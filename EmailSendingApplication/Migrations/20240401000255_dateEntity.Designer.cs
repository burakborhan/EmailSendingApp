﻿// <auto-generated />
using System;
using System.Collections.Generic;
using EmailSendingApplication.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EmailSendingApplication.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240401000255_dateEntity")]
    partial class dateEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EmailSendingApplication.Models.MailRecipient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("date");

                    b.Property<string>("CellPhoneNo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HomePhoneNo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("SentMailId")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WorkPlace")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SentMailId");

                    b.ToTable("MailRecipient");
                });

            modelBuilder.Entity("EmailSendingApplication.Models.MailSenders", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("EnableSSL")
                        .HasColumnType("boolean");

                    b.Property<string>("MailPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MailServerAddress")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Port")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("MailSender");
                });

            modelBuilder.Entity("EmailSendingApplication.Models.SentMail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MailSendersId")
                        .HasColumnType("integer");

                    b.Property<List<string>>("RecipientMails")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<string>("SenderMail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("SendingDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("TransmissionStatus")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("MailSendersId");

                    b.ToTable("SentMail");
                });

            modelBuilder.Entity("EmailSendingApplication.Models.MailRecipient", b =>
                {
                    b.HasOne("EmailSendingApplication.Models.SentMail", null)
                        .WithMany("MailRecipients")
                        .HasForeignKey("SentMailId");
                });

            modelBuilder.Entity("EmailSendingApplication.Models.SentMail", b =>
                {
                    b.HasOne("EmailSendingApplication.Models.MailSenders", "MailSenders")
                        .WithMany()
                        .HasForeignKey("MailSendersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MailSenders");
                });

            modelBuilder.Entity("EmailSendingApplication.Models.SentMail", b =>
                {
                    b.Navigation("MailRecipients");
                });
#pragma warning restore 612, 618
        }
    }
}
