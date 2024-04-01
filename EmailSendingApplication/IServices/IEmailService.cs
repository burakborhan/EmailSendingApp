using EmailSendingApplication.DTO_s;
using EmailSendingApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace EmailSendingApplication.IServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(int senderId, List<int> recipientIds, string subject, string body);
        Task<List<MailRecipient>> GetMailRecipientsAsync();
        Task<List<MailRecipient>> GetMailRecipientsFilterAsync(FilterDTO filter);
        Task<List<MailSenders>> GetMailSendersAsync();
    }
}
