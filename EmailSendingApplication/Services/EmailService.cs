using EmailSendingApplication.IServices;
using System.Net.Mail;
using System.Net;
using EmailSendingApplication.Data;
using EmailSendingApplication.Models;
using AutoMapper;
using EmailSendingApplication.DTO_s;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Xml.Linq;
using System.Globalization;
using Microsoft.CodeAnalysis;

namespace EmailSendingApplication.Services
{
    public class EmailService : IEmailService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EmailService> _logger;
        private readonly IMapper _mapper;

        public EmailService(AppDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MailRecipient>> GetMailRecipientsAsync()
        {
            return await _context.MailRecipient.ToListAsync();
        }

        public async Task<List<MailRecipient>> GetMailRecipientsFilterAsync(FilterDTO filter)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("tr-TR");

            var query =  _context.MailRecipient.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Name))
            {
                query = query.Where(r => r.Name.ToUpper().Contains(filter.Name.ToUpper()));
            }
            if (!string.IsNullOrEmpty(filter.Surname))
            {
                query = query.Where(r => r.Surname.ToUpper().Contains(filter.Surname.ToUpper()));
            }

            if (!string.IsNullOrEmpty(filter.Gender))
            {
                query = query.Where(r => r.Gender.ToUpper().Contains(filter.Gender.ToUpper()));
            }

            if (filter.MinAge.HasValue && filter.MaxAge.HasValue)
            {
                var today = DateTime.Today;
                query = query.Where(r => r.Birthday != null &&
                                         (today.Year - r.Birthday.Year -
                                         (today.DayOfYear < r.Birthday.DayOfYear ? 1 : 0)) >= filter.MinAge &&
                                         (today.Year - r.Birthday.Year -
                                         (today.DayOfYear < r.Birthday.DayOfYear ? 1 : 0)) <= filter.MaxAge);
            }
            if (filter.MinAge.HasValue && !filter.MaxAge.HasValue)
            {
                var today = DateTime.Today;
                query = query.Where(r => r.Birthday != null && (today.Year - r.Birthday.Year - (today.DayOfYear < r.Birthday.DayOfYear ? 1 : 0)) >= filter.MinAge);
            }
            if (!filter.MinAge.HasValue && filter.MaxAge.HasValue)
            {
                var today = DateTime.Today;
                query = query.Where(r => r.Birthday != null && (today.Year - r.Birthday.Year - (today.DayOfYear < r.Birthday.DayOfYear ? 1 : 0)) <= filter.MaxAge);
            }
            if (!string.IsNullOrEmpty(filter.Email))
            {
                query = query.Where(r => r.Email.ToUpper().Contains(filter.Email.ToUpper()));
            }

            if (!string.IsNullOrEmpty(filter.HomePhoneNo))
            {
                query = query.Where(r => r.HomePhoneNo.ToUpper().Contains(filter.HomePhoneNo.ToUpper()));
            }

            if (!string.IsNullOrEmpty(filter.CellPhoneNo))
            {
                query = query.Where(r => r.CellPhoneNo.ToUpper().Contains(filter.CellPhoneNo.ToUpper()));
            }

            if (!string.IsNullOrEmpty(filter.Title))
            {
                query = query.Where(r => r.Title.ToUpper().Contains(filter.Title.ToUpper()));
            }

            if (!string.IsNullOrEmpty(filter.WorkPlace))
            {
                query = query = query.Where(r => r.WorkPlace.ToUpper().Contains(filter.WorkPlace.ToUpper()));
            }

            return await query.ToListAsync();
        }

        public async Task<List<MailSenders>> GetMailSendersAsync()
        {
            return await _context.MailSender.ToListAsync();
        }

        public async Task SendEmailAsync(int senderId, List<int> recipientIds, string subject, string body)
        {
            // Find the specified MailSender.
            var mailSender = await _context.MailSender.FindAsync(senderId);
            var mailSenderDto =_mapper.Map<RequestMailSenderDTO>(_context.MailSender.Find(senderId));
            if (mailSender == null)
            {
                throw new InvalidOperationException("The specified MailSender was not found.");
            }

            // Find the specified MailRecipients.
            var recipients =  _context.MailRecipient
                                           .Where(r => recipientIds.Contains(r.Id))
                                           .ToList();

            if (!recipients.Any())
            {
                throw new InvalidOperationException("The specified MailRecipients were not found.");
            }

            //Mail sending process.
            foreach (var recipient in recipients)
            {
                var emailContent = body.Replace("@AD_SOYAD@", $"{recipient.Name} {recipient.Surname}");

                using (var smtpClient = new SmtpClient(mailSender.MailServerAddress))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential(mailSender.Email, mailSender.MailPassword);
                    smtpClient.EnableSsl = mailSender.EnableSSL;
                    smtpClient.Port = mailSender.Port;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(mailSender.Email),
                        Subject = subject,
                        Body = emailContent,
                        IsBodyHtml = true
                    };
                    mailMessage.To.Add(new MailAddress(recipient.Email));

                    try
                    {
                        await smtpClient.SendMailAsync(mailMessage);
                    }
                    catch (Exception ex)
                    {
                        //Logging or error management.
                        _logger.LogError(ex, "Email could not be sent. Recipient: {RecipientEmail}", recipient.Email);
                    }
                }
            }

            // Save sent emails.
            var recipientEmails = recipients.Select(r => r.Email).ToList();

            var sentMail = new SentMail
            {
                Body = body,
                Subject = subject,
                SendingDate = DateTime.UtcNow,
                TransmissionStatus = true,
                SenderMail = mailSender.Email,
                RecipientMails = recipientEmails,
            };

            _context.SentMail.Add(sentMail);

            sentMail.MailSenders = mailSender;
            sentMail.MailRecipients = recipients;

            _context.SaveChanges();
        }
    }

}
