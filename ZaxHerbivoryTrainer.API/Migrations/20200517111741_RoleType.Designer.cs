﻿// <auto-generated />
using System;
using API.ZaxHerbivoryTrainer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ZaxHerbivoryTrainer.API.Migrations
{
    [DbContext(typeof(ZaxHerbivoryTrainerContext))]
    [Migration("20200517111741_RoleType")]
    partial class RoleType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("API.ZaxHerbivoryTrainer.Models.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AddedUtc")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("CloudinaryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("DecayRate")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("Delete")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DeletedUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ImageId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("API.ZaxHerbivoryTrainer.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedUtc")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FinishedUtc")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("FinishingPercent")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("HashUser")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("PictureCycled")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("API.ZaxHerbivoryTrainer.Models.UsersGuess", b =>
                {
                    b.Property<int>("UsersGuessId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("GuessPercentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ImageId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UsersGuessId");

                    b.HasIndex("ImageId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersGuess");
                });

            modelBuilder.Entity("ZaxHerbivoryTrainer.API.Models.AuthUsers", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("RoleType")
                        .HasColumnType("tinyint");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("AuthUsers");
                });

            modelBuilder.Entity("ZaxHerbivoryTrainer.API.Models.Token", b =>
                {
                    b.Property<int>("TokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiresTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("TokenString")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TokenId");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("API.ZaxHerbivoryTrainer.Models.UsersGuess", b =>
                {
                    b.HasOne("API.ZaxHerbivoryTrainer.Models.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.ZaxHerbivoryTrainer.Models.User", "User")
                        .WithMany("Guesses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
