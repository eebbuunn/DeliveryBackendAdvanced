﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using delivery_backend_advanced.Models;

#nullable disable

namespace DeliveryApi.DAL.Migrations
{
    [DbContext(typeof(BackendDbContext))]
    [Migration("20230403083921_AverageRatingInDishEntity")]
    partial class AverageRatingInDishEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("delivery_backend_advanced.Models.Entities.DishBasketEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<Guid>("DishId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsInOrder")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("OrderEntityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RestaurantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DishId");

                    b.HasIndex("OrderEntityId");

                    b.HasIndex("RestaurantId");

                    b.ToTable("DishesInBasket");
                });

            modelBuilder.Entity("delivery_backend_advanced.Models.Entities.DishEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double?>("AverageRating")
                        .HasColumnType("double precision");

                    b.Property<int>("Category")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsVegetarian")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("MenuEntityId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte[]>("Photo")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("MenuEntityId");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("delivery_backend_advanced.Models.Entities.MenuEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsMain")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RestaurantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("delivery_backend_advanced.Models.Entities.OrderEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("DeliveryTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("OrderTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<Guid>("RestaurantId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RestaurantId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("delivery_backend_advanced.Models.Entities.RatingEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("DishId")
                        .HasColumnType("uuid");

                    b.Property<int>("Value")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DishId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("delivery_backend_advanced.Models.Entities.RestaurantEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Restaurants");
                });

            modelBuilder.Entity("delivery_backend_advanced.Models.Entities.DishBasketEntity", b =>
                {
                    b.HasOne("delivery_backend_advanced.Models.Entities.DishEntity", "Dish")
                        .WithMany()
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("delivery_backend_advanced.Models.Entities.OrderEntity", null)
                        .WithMany("Dishes")
                        .HasForeignKey("OrderEntityId");

                    b.HasOne("delivery_backend_advanced.Models.Entities.RestaurantEntity", "Restaurant")
                        .WithMany()
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dish");

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("delivery_backend_advanced.Models.Entities.DishEntity", b =>
                {
                    b.HasOne("delivery_backend_advanced.Models.Entities.MenuEntity", null)
                        .WithMany("Dishes")
                        .HasForeignKey("MenuEntityId");
                });

            modelBuilder.Entity("delivery_backend_advanced.Models.Entities.MenuEntity", b =>
                {
                    b.HasOne("delivery_backend_advanced.Models.Entities.RestaurantEntity", "Restaurant")
                        .WithMany("Menus")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("delivery_backend_advanced.Models.Entities.OrderEntity", b =>
                {
                    b.HasOne("delivery_backend_advanced.Models.Entities.RestaurantEntity", "Restaurant")
                        .WithMany("Orders")
                        .HasForeignKey("RestaurantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Restaurant");
                });

            modelBuilder.Entity("delivery_backend_advanced.Models.Entities.RatingEntity", b =>
                {
                    b.HasOne("delivery_backend_advanced.Models.Entities.DishEntity", "Dish")
                        .WithMany("Ratings")
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dish");
                });

            modelBuilder.Entity("delivery_backend_advanced.Models.Entities.DishEntity", b =>
                {
                    b.Navigation("Ratings");
                });

            modelBuilder.Entity("delivery_backend_advanced.Models.Entities.MenuEntity", b =>
                {
                    b.Navigation("Dishes");
                });

            modelBuilder.Entity("delivery_backend_advanced.Models.Entities.OrderEntity", b =>
                {
                    b.Navigation("Dishes");
                });

            modelBuilder.Entity("delivery_backend_advanced.Models.Entities.RestaurantEntity", b =>
                {
                    b.Navigation("Menus");

                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
