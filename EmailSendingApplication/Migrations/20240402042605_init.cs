using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EmailSendingApplication.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MailSender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    EnableSSL = table.Column<bool>(type: "boolean", nullable: false),
                    MailServerAddress = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    MailPassword = table.Column<string>(type: "text", nullable: false),
                    Port = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailSender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SentMail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    SendingDate = table.Column<DateTime>(type: "date", nullable: false),
                    TransmissionStatus = table.Column<bool>(type: "boolean", nullable: false),
                    SenderMail = table.Column<string>(type: "text", nullable: false),
                    RecipientMails = table.Column<List<string>>(type: "text[]", nullable: false),
                    MailSendersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SentMail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SentMail_MailSender_MailSendersId",
                        column: x => x.MailSendersId,
                        principalTable: "MailSender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MailRecipient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    Birthday = table.Column<DateTime>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    HomePhoneNo = table.Column<string>(type: "text", nullable: false),
                    CellPhoneNo = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    WorkPlace = table.Column<string>(type: "text", nullable: false),
                    SentMailId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailRecipient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MailRecipient_SentMail_SentMailId",
                        column: x => x.SentMailId,
                        principalTable: "SentMail",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MailRecipient_SentMailId",
                table: "MailRecipient",
                column: "SentMailId");

            migrationBuilder.CreateIndex(
                name: "IX_SentMail_MailSendersId",
                table: "SentMail",
                column: "MailSendersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MailRecipient");

            migrationBuilder.DropTable(
                name: "SentMail");

            migrationBuilder.DropTable(
                name: "MailSender");
        }
    }
}
