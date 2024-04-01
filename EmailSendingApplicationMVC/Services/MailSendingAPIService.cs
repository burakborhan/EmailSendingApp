using EmailSendingApplication.DTO_s;
using EmailSendingApplication.IServices;
using EmailSendingApplication.Models;
using EmailSendingApplication.Services;
using EmailSendingApplicationMVC.IServices;
using EmailSendingApplicationMVC.ViewModels;

namespace EmailSendingApplicationMVC.Services
{
    public class MailSendingAPIService : IMailSendingAPIService
    {
        private readonly IEmailService _emailService;

        public MailSendingAPIService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<List<MailRecipient>> GetMailRecipientsAsync()
        {
            return await _emailService.GetMailRecipientsAsync();
        }

        public async Task<List<MailRecipient>> GetMailRecipientsFilterAsync(FilterDTO filter)
        {
            return await _emailService.GetMailRecipientsFilterAsync(filter);
        }

        public async Task<List<MailSenders>> GetMailSendersAsync()
        {
            return await _emailService.GetMailSendersAsync();
        }

        //public async Task SendEmailAsync(int senderId, List<int> recipientIds, string subject, string body)
        //{
        //    await _emailService.SendEmailAsync(senderId, recipientIds, subject, body);
        //}

        public async Task<string> SendEmailAsync(int senderId, List<int> recipientIds, string subject, string body)
        {
            await _emailService.SendEmailAsync(senderId, recipientIds, subject, body);
            return "Başarılı";
        }
    }
}
