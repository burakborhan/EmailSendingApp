using EmailSendingApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EmailSendingApplication.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<MailSenders> MailSender { get; set; }
        public DbSet<MailRecipient> MailRecipient { get; set; }
        public DbSet<SentMail> SentMail { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=EmailSendingApp;Username=postgres;Password=admin");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<MailRecipient>()
                    .Property(e => e.Birthday)
                    .HasColumnType("date");

            modelBuilder.Entity<SentMail>()
                    .Property(e => e.SendingDate)
                    .HasColumnType("date");

        }



    }
}
