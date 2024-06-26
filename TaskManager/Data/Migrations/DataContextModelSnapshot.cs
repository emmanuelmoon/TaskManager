﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManager.Data;

#nullable disable

namespace TaskManager.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.31")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TaskManager.Models.Task", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Tasks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2024, 6, 19, 16, 32, 42, 940, DateTimeKind.Local).AddTicks(8547),
                            Description = "Setup Admin Dashboard",
                            DueDate = new DateTime(2024, 6, 25, 16, 32, 42, 940, DateTimeKind.Local).AddTicks(8574),
                            Status = "Completed",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2024, 6, 24, 16, 32, 42, 940, DateTimeKind.Local).AddTicks(8581),
                            Description = "Create Initial Users",
                            DueDate = new DateTime(2024, 6, 29, 16, 32, 42, 940, DateTimeKind.Local).AddTicks(8584),
                            Status = "In Progress",
                            UserId = 1
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2024, 6, 28, 16, 32, 42, 940, DateTimeKind.Local).AddTicks(8587),
                            Description = "Review System Logs",
                            DueDate = new DateTime(2024, 7, 3, 16, 32, 42, 940, DateTimeKind.Local).AddTicks(8589),
                            Status = "Pending",
                            UserId = 1
                        });
                });

            modelBuilder.Entity("TaskManager.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@gmail.com",
                            PasswordHash = new byte[] { 244, 233, 150, 49, 6, 153, 218, 27, 53, 28, 65, 102, 160, 166, 14, 188, 80, 240, 71, 240, 82, 133, 11, 206, 50, 180, 134, 30, 143, 42, 226, 71, 49, 191, 244, 108, 148, 28, 249, 123, 1, 163, 101, 39, 202, 138, 202, 240, 247, 188, 38, 40, 40, 96, 167, 120, 195, 160, 61, 185, 86, 5, 18, 225 },
                            PasswordSalt = new byte[] { 218, 78, 89, 89, 120, 5, 158, 36, 169, 157, 29, 227, 98, 67, 143, 113, 211, 99, 2, 105, 110, 163, 54, 20, 66, 101, 64, 173, 132, 106, 224, 253, 194, 34, 84, 182, 145, 196, 120, 134, 78, 176, 170, 125, 29, 253, 219, 148, 88, 193, 143, 217, 153, 199, 48, 131, 210, 43, 152, 227, 224, 22, 211, 32, 208, 150, 253, 77, 20, 49, 55, 169, 222, 42, 115, 34, 234, 143, 35, 170, 1, 221, 75, 139, 106, 194, 62, 235, 253, 183, 15, 96, 212, 44, 254, 18, 75, 253, 25, 246, 182, 220, 28, 109, 178, 49, 12, 215, 55, 150, 76, 233, 252, 120, 241, 156, 133, 201, 252, 27, 187, 90, 242, 179, 96, 81, 64, 69 },
                            Role = "Admin",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("TaskManager.Models.Task", b =>
                {
                    b.HasOne("TaskManager.Models.User", "User")
                        .WithMany("Tasks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskManager.Models.User", b =>
                {
                    b.Navigation("Tasks");
                });
#pragma warning restore 612, 618
        }
    }
}
