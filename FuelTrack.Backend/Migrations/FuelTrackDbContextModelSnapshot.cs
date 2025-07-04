﻿// <auto-generated />
using System;
using FuelTrack.Backend.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FuelTrack.Backend.Migrations
{
    [DbContext(typeof(FuelTrackDbContext))]
    partial class FuelTrackDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FuelTrack.Backend.Domain.Dispatch.Entities.Driver", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean");

                    b.Property<string>("LicenseNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("FuelTrack.Backend.Domain.Dispatch.Entities.Tank", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SerialNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TotalCompartments")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Tanks");
                });

            modelBuilder.Entity("FuelTrack.Backend.Domain.Dispatch.Entities.Truck", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FuelType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("boolean");

                    b.Property<string>("Plate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Trucks");
                });

            modelBuilder.Entity("FuelTrack.Backend.Domain.Orders.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("DriverId")
                        .HasColumnType("uuid");

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid?>("TankId")
                        .HasColumnType("uuid");

                    b.Property<string>("TerminalName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("TruckId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("FuelTrack.Backend.Domain.Pricing.Entities.FuelPrice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FuelType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TerminalName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("FuelPrices");
                });

            modelBuilder.Entity("FuelTrack.Backend.Domain.Dispatch.Entities.Tank", b =>
                {
                    b.OwnsMany("FuelTrack.Backend.Domain.Dispatch.ValueObjects.CompartmentAssignment", "CompartmentAssignments", b1 =>
                        {
                            b1.Property<Guid>("TankId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<int>("CompartmentNumber")
                                .HasColumnType("integer");

                            b1.Property<string>("FuelType")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<decimal>("Gallons")
                                .HasColumnType("numeric");

                            b1.HasKey("TankId", "Id");

                            b1.ToTable("CompartmentAssignment");

                            b1.WithOwner()
                                .HasForeignKey("TankId");
                        });

                    b.Navigation("CompartmentAssignments");
                });

            modelBuilder.Entity("FuelTrack.Backend.Domain.Orders.Entities.Order", b =>
                {
                    b.OwnsMany("FuelTrack.Backend.Domain.Orders.ValueObjects.Payment", "Payments", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid");

                            b1.Property<decimal>("Amount")
                                .HasColumnType("numeric");

                            b1.Property<string>("Bank")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<DateTime>("Date")
                                .HasColumnType("timestamp with time zone");

                            b1.Property<string>("OperationCode")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("OrderId", "Id");

                            b1.ToTable("Orders_Payments");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");
                        });

                    b.OwnsMany("FuelTrack.Backend.Domain.Orders.Entities.OrderProduct", "Products", b1 =>
                        {
                            b1.Property<Guid>("OrderId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid");

                            b1.Property<string>("FuelType")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Note")
                                .HasColumnType("text");

                            b1.Property<decimal>("Quantity")
                                .HasColumnType("numeric");

                            b1.Property<string>("Unit")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<decimal>("UnitPrice")
                                .HasColumnType("numeric");

                            b1.HasKey("OrderId", "Id");

                            b1.ToTable("OrderProduct");

                            b1.WithOwner()
                                .HasForeignKey("OrderId");

                            b1.OwnsMany("FuelTrack.Backend.Domain.Orders.ValueObjects.Payment", "Payments", b2 =>
                                {
                                    b2.Property<Guid>("OrderProductOrderId")
                                        .HasColumnType("uuid");

                                    b2.Property<Guid>("OrderProductId")
                                        .HasColumnType("uuid");

                                    b2.Property<Guid>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("uuid");

                                    b2.Property<decimal>("Amount")
                                        .HasColumnType("numeric");

                                    b2.Property<string>("Bank")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.Property<DateTime>("Date")
                                        .HasColumnType("timestamp with time zone");

                                    b2.Property<string>("OperationCode")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.HasKey("OrderProductOrderId", "OrderProductId", "Id");

                                    b2.ToTable("OrderProduct_Payments");

                                    b2.WithOwner()
                                        .HasForeignKey("OrderProductOrderId", "OrderProductId");
                                });

                            b1.Navigation("Payments");
                        });

                    b.Navigation("Payments");

                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
