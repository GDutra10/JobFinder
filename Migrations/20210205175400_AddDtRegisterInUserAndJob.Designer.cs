﻿// <auto-generated />
using System;
using JobFinder.Models.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JobFinder.Migrations
{
    [DbContext(typeof(JobFinderDbContext))]
    [Migration("20210205175400_AddDtRegisterInUserAndJob")]
    partial class AddDtRegisterInUserAndJob
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("JobFinder.Models.Candidate", b =>
                {
                    b.Property<int>("IdCandidate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DtRegister")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCustomer")
                        .HasColumnType("int")
                        .HasColumnName("IdUser");

                    b.Property<int>("IdJob")
                        .HasColumnType("int")
                        .HasColumnName("IdJob");

                    b.Property<bool>("WasAccept")
                        .HasColumnType("bit");

                    b.Property<bool>("WasReject")
                        .HasColumnType("bit");

                    b.HasKey("IdCandidate");

                    b.HasIndex("IdCustomer");

                    b.HasIndex("IdJob");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("JobFinder.Models.Company", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("DeEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DePassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DtRegister")
                        .HasColumnType("datetime2");

                    b.Property<string>("NmUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NuTelephone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUser");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("JobFinder.Models.Customer", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("DeEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DePassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DtRegister")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdRole")
                        .HasColumnType("int")
                        .HasColumnName("IdRole");

                    b.Property<string>("NmUser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NuTelephone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUser");

                    b.HasIndex("IdRole");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("JobFinder.Models.Job", b =>
                {
                    b.Property<int>("IdJob")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("DeDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DtRegister")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCompany")
                        .HasColumnType("int")
                        .HasColumnName("IdUser");

                    b.Property<int>("IdRole")
                        .HasColumnType("int")
                        .HasColumnName("IdRole");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<float?>("VlSalaryMax")
                        .HasColumnType("real");

                    b.Property<float?>("VlSalaryMin")
                        .HasColumnType("real");

                    b.HasKey("IdJob");

                    b.HasIndex("IdCompany");

                    b.HasIndex("IdRole");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("JobFinder.Models.Role", b =>
                {
                    b.Property<int>("IdRole")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("NmRole")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdRole");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("JobFinder.Models.Candidate", b =>
                {
                    b.HasOne("JobFinder.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("IdCustomer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobFinder.Models.Job", "Job")
                        .WithMany()
                        .HasForeignKey("IdJob")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("JobFinder.Models.Customer", b =>
                {
                    b.HasOne("JobFinder.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("IdRole")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("JobFinder.Models.Job", b =>
                {
                    b.HasOne("JobFinder.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("IdCompany")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("JobFinder.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("IdRole")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
