using EmailSendingApplication.Data;
using EmailSendingApplication.DTO_s;
using EmailSendingApplicationMVC.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using System.Text.Json;

namespace EmailSendingApplicationMVC.Services
{
    public class MailReportAPIService : IMailReportAPIService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://'...'/api/MailSending";
        private readonly AppDbContext _context;

        public MailReportAPIService(AppDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        public async Task<ActionResult<IEnumerable<RespondSentMailDTO>>> GetSentMailsByEmailAsync(string senderEmail)
        {
            var sentMails = await _context.SentMail
                .Where(mail => mail.SenderMail == senderEmail)
                .ToListAsync();

            var emailReports = sentMails.Select(mail => new RespondSentMailDTO
            {
                Subject = mail.Subject,
                Body = mail.Body,
                SendingDate = mail.SendingDate,
                TransmissionStatus = mail.TransmissionStatus,
                RecipientMails = mail.RecipientMails.ToList()
            }).ToList();

            return emailReports;
        }

        public async Task<List<RespondSentMailDTO>> GetSentMailsAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var sentMails = JsonSerializer.Deserialize<List<RespondSentMailDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return sentMails;
        }

        public List<RespondSentMailDTO> FilterDistinctSenders(List<RespondSentMailDTO> sentMails)
        {
            var distinctSenders = sentMails
                .GroupBy(mail => mail.SenderMail)
                .Select(group => group.First())
                .ToList();

            return distinctSenders;
        }
    }
}
