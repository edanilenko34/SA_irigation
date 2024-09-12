﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SA.Irrigation.Db;

#nullable disable

namespace SA.Irrigation.Db.Migrations
{
    [DbContext(typeof(IrrigationDbContext))]
    [Migration("20240912125818_Added_FinishCron")]
    partial class Added_FinishCron
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SA.Irrigation.Db.Entities.Device", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Address")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<Guid>("ModelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.HasIndex("Address")
                        .IsUnique();

                    b.HasIndex("ModelId");

                    b.ToTable("Devices");

                    b.HasDiscriminator().HasValue("Device");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("SA.Irrigation.Db.Entities.DeviceModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CloseCommand")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Description")
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.Property<string>("GetDataCommand")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("OpenCommand")
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Models");
                });

            modelBuilder.Entity("SA.Irrigation.Db.Entities.Schedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FinishBy")
                        .HasColumnType("int");

                    b.Property<string>("FinishCron")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("FinishDeviceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("FinishValue")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ParentDeviceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StartCron")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FinishDeviceId");

                    b.HasIndex("ParentDeviceId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("SA.Irrigation.Db.Entities.Sensor", b =>
                {
                    b.HasBaseType("SA.Irrigation.Db.Entities.Device");

                    b.HasDiscriminator().HasValue("Sensor");
                });

            modelBuilder.Entity("SA.Irrigation.Db.Entities.Device", b =>
                {
                    b.HasOne("SA.Irrigation.Db.Entities.DeviceModel", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Model");
                });

            modelBuilder.Entity("SA.Irrigation.Db.Entities.Schedule", b =>
                {
                    b.HasOne("SA.Irrigation.Db.Entities.Sensor", "FinishDevice")
                        .WithMany()
                        .HasForeignKey("FinishDeviceId");

                    b.HasOne("SA.Irrigation.Db.Entities.Device", "ParentDevice")
                        .WithMany("Schedules")
                        .HasForeignKey("ParentDeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FinishDevice");

                    b.Navigation("ParentDevice");
                });

            modelBuilder.Entity("SA.Irrigation.Db.Entities.Device", b =>
                {
                    b.Navigation("Schedules");
                });
#pragma warning restore 612, 618
        }
    }
}
