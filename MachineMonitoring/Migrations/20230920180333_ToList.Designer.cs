﻿// <auto-generated />
using System;
using MachineMonitoring.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MachineMonitoring.Migrations
{
    [DbContext(typeof(MachineMonitoringDbContext))]
    [Migration("20230920180333_ToList")]
    partial class ToList
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MachineMonitoring.Data.Models.Machine", b =>
                {
                    b.Property<int>("MachineId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MachineId"), 1L, 1);

                    b.Property<int?>("CurrentMachineState")
                        .HasColumnType("int");

                    b.Property<string>("OrderCurrent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkcenterId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MachineId");

                    b.ToTable("Machines");
                });

            modelBuilder.Entity("MachineMonitoring.Data.Models.MachineDeviceState", b =>
                {
                    b.Property<int>("MachineDeviceStateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MachineDeviceStateId"), 1L, 1);

                    b.Property<int>("DeviceState")
                        .HasColumnType("int");

                    b.Property<DateTime>("DeviceStateChangedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("MachineId")
                        .HasColumnType("int");

                    b.Property<string>("WorkcenterId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MachineDeviceStateId");

                    b.HasIndex("MachineId");

                    b.ToTable("MachineDeviceState");
                });

            modelBuilder.Entity("MachineMonitoring.Data.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<int?>("MachineId")
                        .HasColumnType("int");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderId");

                    b.HasIndex("MachineId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("MachineMonitoring.Data.Models.MachineDeviceState", b =>
                {
                    b.HasOne("MachineMonitoring.Data.Models.Machine", "Machine")
                        .WithMany("HistoricalDeviceStates")
                        .HasForeignKey("MachineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("MachineMonitoring.Data.Models.Order", b =>
                {
                    b.HasOne("MachineMonitoring.Data.Models.Machine", null)
                        .WithMany("OrderBacklog")
                        .HasForeignKey("MachineId");
                });

            modelBuilder.Entity("MachineMonitoring.Data.Models.Machine", b =>
                {
                    b.Navigation("HistoricalDeviceStates");

                    b.Navigation("OrderBacklog");
                });
#pragma warning restore 612, 618
        }
    }
}
