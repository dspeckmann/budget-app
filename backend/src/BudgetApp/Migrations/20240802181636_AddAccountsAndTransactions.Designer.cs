﻿// <auto-generated />
using System;
using BudgetApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BudgetApp.Migrations
{
    [DbContext(typeof(BudgetAppDbContext))]
    [Migration("20240802181636_AddAccountsAndTransactions")]
    partial class AddAccountsAndTransactions
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BudgetApp.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("BudgetApp.RecurringTransaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("RecurringTransactions");
                });

            modelBuilder.Entity("BudgetApp.RecurringTransactionDetails", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateOnly?>("EndDate")
                        .HasColumnType("date");

                    b.Property<int>("Frequency")
                        .HasColumnType("integer");

                    b.Property<Guid?>("ReceivingAccountId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("RecurringTransactionId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SendingAccountId")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.HasKey("Id");

                    b.HasIndex("ReceivingAccountId");

                    b.HasIndex("RecurringTransactionId");

                    b.HasIndex("SendingAccountId");

                    b.ToTable("RecurringTransactionDetails");
                });

            modelBuilder.Entity("BudgetApp.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("ReceivingAccountId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("SendingAccountId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ReceivingAccountId");

                    b.HasIndex("SendingAccountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BudgetApp.TransactionCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TransactionCategories");
                });

            modelBuilder.Entity("BudgetApp.RecurringTransaction", b =>
                {
                    b.HasOne("BudgetApp.TransactionCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("BudgetApp.RecurringTransactionDetails", b =>
                {
                    b.HasOne("BudgetApp.Account", "ReceivingAccount")
                        .WithMany()
                        .HasForeignKey("ReceivingAccountId");

                    b.HasOne("BudgetApp.RecurringTransaction", null)
                        .WithMany("Details")
                        .HasForeignKey("RecurringTransactionId");

                    b.HasOne("BudgetApp.Account", "SendingAccount")
                        .WithMany()
                        .HasForeignKey("SendingAccountId");

                    b.Navigation("ReceivingAccount");

                    b.Navigation("SendingAccount");
                });

            modelBuilder.Entity("BudgetApp.Transaction", b =>
                {
                    b.HasOne("BudgetApp.TransactionCategory", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BudgetApp.Account", "ReceivingAccount")
                        .WithMany()
                        .HasForeignKey("ReceivingAccountId");

                    b.HasOne("BudgetApp.Account", "SendingAccount")
                        .WithMany()
                        .HasForeignKey("SendingAccountId");

                    b.Navigation("Category");

                    b.Navigation("ReceivingAccount");

                    b.Navigation("SendingAccount");
                });

            modelBuilder.Entity("BudgetApp.RecurringTransaction", b =>
                {
                    b.Navigation("Details");
                });
#pragma warning restore 612, 618
        }
    }
}
