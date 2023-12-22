﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestTask.DAL;

#nullable disable

namespace TestTask.DAL.Migrations
{
    [DbContext(typeof(TestTaskDbContext))]
    partial class TestTaskDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TestTask.Domain.Entities.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("157f1ade-c626-4bb6-a0f6-dfec9af03acd"),
                            Title = "Admin"
                        },
                        new
                        {
                            Id = new Guid("be47cc53-f76f-4007-92cb-1ba3ea8f5922"),
                            Title = "User"
                        },
                        new
                        {
                            Id = new Guid("1e0940e1-6e7b-4786-9f73-0b1fce52f8ee"),
                            Title = "SuperAdmin"
                        },
                        new
                        {
                            Id = new Guid("9c6f35a3-6be9-4e0b-a3a0-6c7c50db6464"),
                            Title = "Support"
                        });
                });

            modelBuilder.Entity("TestTask.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("cd9b8a1d-0897-418f-8981-9ad728ee35c3"),
                            Age = 25,
                            Email = "penis@gmail.com",
                            FullName = "Mike Vazovskiy",
                            PasswordHash = "$2a$11$8exyJAoj3b1oqKn4CBdd4upnwp5FCVDBTM/BpY9XickzyPXh8u61u"
                        },
                        new
                        {
                            Id = new Guid("c6f7040b-c383-49cd-9336-11118eaf384d"),
                            Age = 28,
                            Email = "jopka@gmail.com",
                            FullName = "Sally Vazovskiy",
                            PasswordHash = "$2a$11$S.qmXp9l/sfcE3uF4XlWlO74uIbZuSVlColXBwD9peuAydSR40UYC"
                        },
                        new
                        {
                            Id = new Guid("2324250c-1258-40bd-9cc2-429b749a9a2a"),
                            Age = 34,
                            Email = "govno@gmail.com",
                            FullName = "Ivan Vazovskiy",
                            PasswordHash = "$2a$11$YYjEojDUW.dbGb9R/QSE4O6pmTsW7ZtzwYSy.7DGt0VfMHrFgLMWC"
                        });
                });

            modelBuilder.Entity("TestTask.Domain.Entities.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UsersRoles");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("cd9b8a1d-0897-418f-8981-9ad728ee35c3"),
                            RoleId = new Guid("157f1ade-c626-4bb6-a0f6-dfec9af03acd")
                        },
                        new
                        {
                            UserId = new Guid("cd9b8a1d-0897-418f-8981-9ad728ee35c3"),
                            RoleId = new Guid("be47cc53-f76f-4007-92cb-1ba3ea8f5922")
                        },
                        new
                        {
                            UserId = new Guid("cd9b8a1d-0897-418f-8981-9ad728ee35c3"),
                            RoleId = new Guid("1e0940e1-6e7b-4786-9f73-0b1fce52f8ee")
                        },
                        new
                        {
                            UserId = new Guid("c6f7040b-c383-49cd-9336-11118eaf384d"),
                            RoleId = new Guid("157f1ade-c626-4bb6-a0f6-dfec9af03acd")
                        },
                        new
                        {
                            UserId = new Guid("c6f7040b-c383-49cd-9336-11118eaf384d"),
                            RoleId = new Guid("be47cc53-f76f-4007-92cb-1ba3ea8f5922")
                        },
                        new
                        {
                            UserId = new Guid("2324250c-1258-40bd-9cc2-429b749a9a2a"),
                            RoleId = new Guid("be47cc53-f76f-4007-92cb-1ba3ea8f5922")
                        },
                        new
                        {
                            UserId = new Guid("2324250c-1258-40bd-9cc2-429b749a9a2a"),
                            RoleId = new Guid("9c6f35a3-6be9-4e0b-a3a0-6c7c50db6464")
                        });
                });

            modelBuilder.Entity("TestTask.Domain.Entities.UserRole", b =>
                {
                    b.HasOne("TestTask.Domain.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestTask.Domain.Entities.User", "User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TestTask.Domain.Entities.User", b =>
                {
                    b.Navigation("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}