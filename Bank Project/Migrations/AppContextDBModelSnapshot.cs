﻿// <auto-generated />
using System;
using DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankProject.Migrations
{
    [DbContext(typeof(AppContextDB))]
    partial class AppContextDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BankAccount", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(9,2)");

                    b.Property<int>("Percent")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("BankAccountVIP", b =>
                {
                    b.Property<int>("VIP_UserId")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(9,2)");

                    b.Property<int>("Percent")
                        .HasColumnType("int");

                    b.HasKey("VIP_UserId");

                    b.ToTable("VIPAccounts");
                });

            modelBuilder.Entity("BankAmountHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(9,2)");

                    b.Property<string>("AmountType")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("amountHistory");
                });

            modelBuilder.Entity("BankAmountHistoryVIP", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(9,2)");

                    b.Property<string>("AmountType")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime");

                    b.Property<int>("VIP_UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VIP_UserId");

                    b.ToTable("amountHistoryVIPs");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Email" }, "Email_Index")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("VIP_User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("VIP_Number")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "VIP_Number" }, "VIP_Index")
                        .IsUnique();

                    b.ToTable("UsersVIP");
                });

            modelBuilder.Entity("BankAccount", b =>
                {
                    b.HasOne("User", "User")
                        .WithOne("BankAccount")
                        .HasForeignKey("BankAccount", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BankAccountVIP", b =>
                {
                    b.HasOne("VIP_User", "VIP_User")
                        .WithOne("AccountVIP")
                        .HasForeignKey("BankAccountVIP", "VIP_UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VIP_User");
                });

            modelBuilder.Entity("BankAmountHistory", b =>
                {
                    b.HasOne("User", "User")
                        .WithMany("amountHistory")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BankAmountHistoryVIP", b =>
                {
                    b.HasOne("VIP_User", "User")
                        .WithMany("amountHistoryVIP")
                        .HasForeignKey("VIP_UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("User", b =>
                {
                    b.Navigation("BankAccount")
                        .IsRequired();

                    b.Navigation("amountHistory");
                });

            modelBuilder.Entity("VIP_User", b =>
                {
                    b.Navigation("AccountVIP")
                        .IsRequired();

                    b.Navigation("amountHistoryVIP");
                });
#pragma warning restore 612, 618
        }
    }
}
