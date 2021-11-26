﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20211125183710_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DataAccess.Company", b =>
                {
                    b.Property<Guid>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CompanyNumber")
                        .HasColumnType("int");

                    b.Property<string>("CompanyType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Market")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ScheduleId")
                        .HasColumnType("int");

                    b.HasKey("CompanyId");

                    b.HasIndex("ScheduleId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("DataAccess.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<DateTime>("SendingDate")
                        .HasColumnType("datetime2");

                    b.HasKey("NotificationId");

                    b.HasIndex("ScheduleId");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("DataAccess.Schedule", b =>
                {
                    b.Property<int>("ScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("ScheduleId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("DataAccess.Company", b =>
                {
                    b.HasOne("DataAccess.Schedule", "Schedule")
                        .WithMany()
                        .HasForeignKey("ScheduleId");

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("DataAccess.Notification", b =>
                {
                    b.HasOne("DataAccess.Schedule", "Schedule")
                        .WithMany("Notifications")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("DataAccess.Schedule", b =>
                {
                    b.Navigation("Notifications");
                });
#pragma warning restore 612, 618
        }
    }
}
