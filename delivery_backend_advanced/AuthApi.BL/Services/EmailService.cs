﻿using System.Net.Mail;
using AuthApi.Common.ConfigClasses;
using AuthApi.Common.Dtos;
using AuthApi.Common.Interfaces;
using AuthApi.DAL;
using AuthApi.DAL.Entities;
using Common.Configurations;
using delivery_backend_advanced.Exceptions;
using MailKit.Security;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace AuthApi.BL.Services;

public class EmailService : IEmailService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;

    public EmailService(UserManager<AppUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task SendEmailAsync(SendEmailDto emailDto)
    {
        var emailMessage = new MimeMessage();
 
        emailMessage.From.Add(new MailboxAddress("Администрация сайта", "admin@metanit.com"));
        emailMessage.To.Add(new MailboxAddress("", emailDto.email));
        emailMessage.Subject = emailDto.subject;
        emailMessage.Body = new TextPart(TextFormat.Html) { Text = emailDto.message };

        var emailConfig = _configuration.GetSection("EmailConfig").Get<EmailConfig>();
        using var client = new SmtpClient();
        await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(emailConfig.CorpEmail, emailConfig.CorpPassword);
        await client.SendAsync(emailMessage);
 
        await client.DisconnectAsync(true);
    }

    public async Task ConfirmEmail(string email, string code)
    {
        var user = await _userManager.FindByEmailAsync(email) ??
                   throw new NotFoundException($"Cant find user with email {email}");
        
        var result = await _userManager.ConfirmEmailAsync(user, code);
        if (!result.Succeeded)
        {
            throw new Exception("Error");
        }
    }
    
    public async Task SendConfirmationEmail(HttpRequest request, IUrlHelper urlHelper, SendEmailDto emailDto)
    {
        var user = await _userManager.FindByEmailAsync(emailDto.email) ??
                   throw new NotFoundException($"Cant find user with email {emailDto.email}");

        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        
        var callbackUrl = urlHelper.Action(
            "ConfirmEmail",
            "Email",
            new { email = user.Email, code = code },
            request.Scheme);
        emailDto.message += $"<a href='{callbackUrl}'>link</a>";
        
        await SendEmailAsync(emailDto);
    }

    public async Task SendConfirmationPasswordEmail(HttpRequest request, IUrlHelper urlHelper, string newPassword, SendEmailDto emailDto)
    {
        var user = await _userManager.FindByEmailAsync(emailDto.email) ??
                   throw new NotFoundException($"Cant find user with email {emailDto.email}");

        var callbackUrl = urlHelper.Action(
            "ChangeForgotPassword",
            "Profile",
            new { email = user.Email, password = newPassword },
            request.Scheme);
        emailDto.message += $"<a href='{callbackUrl}'>link</a>";
        
        await SendEmailAsync(emailDto);
    }
}