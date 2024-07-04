﻿// <auto-generated />
using System;
using Bank_Api.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bank_Api.Migrations
{
    [DbContext(typeof(BankDbContext))]
    [Migration("20240704144015_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bank_Api.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<long>("AccountNumber")
                        .HasColumnType("bigint");

                    b.Property<double>("Money")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserInfoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserInfoId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("Bank_Api.Models.Creditcard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<long>("CardNo")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Creditcard");
                });

            modelBuilder.Entity("Bank_Api.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int?>("CreditcardId")
                        .HasColumnType("int");

                    b.Property<int?>("FromAccountId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Sender")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ToAccountId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreditcardId");

                    b.HasIndex("FromAccountId");

                    b.HasIndex("ToAccountId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("Bank_Api.Models.UserAuthentication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserAuthentication");
                });

            modelBuilder.Entity("Bank_Api.Models.UserInfo", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Firstname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserInfo");
                });

            modelBuilder.Entity("Bank_Api.Models.Account", b =>
                {
                    b.HasOne("Bank_Api.Models.UserInfo", "UserInfo")
                        .WithMany("Accounts")
                        .HasForeignKey("UserInfoId");

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("Bank_Api.Models.Creditcard", b =>
                {
                    b.HasOne("Bank_Api.Models.Account", "Account")
                        .WithMany("CreditCards")
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Bank_Api.Models.Transaction", b =>
                {
                    b.HasOne("Bank_Api.Models.Creditcard", "Creditcard")
                        .WithMany()
                        .HasForeignKey("CreditcardId");

                    b.HasOne("Bank_Api.Models.Account", "FromAccount")
                        .WithMany()
                        .HasForeignKey("FromAccountId");

                    b.HasOne("Bank_Api.Models.Account", "ToAccount")
                        .WithMany()
                        .HasForeignKey("ToAccountId");

                    b.Navigation("Creditcard");

                    b.Navigation("FromAccount");

                    b.Navigation("ToAccount");
                });

            modelBuilder.Entity("Bank_Api.Models.UserInfo", b =>
                {
                    b.HasOne("Bank_Api.Models.UserAuthentication", "UserAuthentication")
                        .WithOne("UserInfo")
                        .HasForeignKey("Bank_Api.Models.UserInfo", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserAuthentication");
                });

            modelBuilder.Entity("Bank_Api.Models.Account", b =>
                {
                    b.Navigation("CreditCards");
                });

            modelBuilder.Entity("Bank_Api.Models.UserAuthentication", b =>
                {
                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("Bank_Api.Models.UserInfo", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
