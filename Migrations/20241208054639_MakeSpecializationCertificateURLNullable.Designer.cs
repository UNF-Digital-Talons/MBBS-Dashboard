﻿// <auto-generated />
using System;
using MBBS.Dashboard.web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MBBS.Dashboard.web.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241208054639_MakeSpecializationCertificateURLNullable")]
    partial class MakeSpecializationCertificateURLNullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MBBS.Dashboard.web.Controllers.ActivityLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("Action")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ActivityLogs");
                });

            modelBuilder.Entity("MBBS.Dashboard.web.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LegalName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("MBBS.Dashboard.web.Models.ExcelDataCourseraSpecialization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Completed")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CompletedCourses")
                        .HasColumnType("int");

                    b.Property<int?>("CoursesInSpecialization")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EnrollmentSource")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EnrollmentTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExternalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastSpecializationActivityTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LocationCity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocationCountry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LocationRegion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProgramName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProgramSlug")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RemovedFromProgram")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Specialization")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpecializationCertificateURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("SpecializationCompletionTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("SpecializationSlug")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("University")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ExcelDataCourseraSpecialization");
                });
#pragma warning restore 612, 618
        }
    }
}
