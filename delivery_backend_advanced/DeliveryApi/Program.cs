using Common.Configurations;
using delivery_backend_advanced.Models;
using delivery_backend_advanced.Services.ExceptionHandler;
using DeliveryApi.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDeliveryApiServices();

builder.ConfigureSwagger();

builder.ConfigureJwt();

builder.ConfigureDeliveryApiDAL();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureDeliveryApiDAL();

app.UseLoggerMiddleware();

app.UseExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

//delete
app.UseAuthentication();

app.MapControllers();

app.Run();