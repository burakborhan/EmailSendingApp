using EmailSendingApplication.DTO_s;
using EmailSendingApplication.Models;
using EmailSendingApplicationMVC.ViewModels;

namespace EmailSendingApplicationMVC.IServices
{
    public interface IMailSendingAPIService
    {
        Task<string> SendEmailAsync(int senderId, List<int> recipientIds, string subject, string body);
        Task<List<MailSenders>> GetMailSendersAsync();
        Task<List<MailRecipient>> GetMailRecipientsAsync();
        Task<List<MailRecipient>> GetMailRecipientsFilterAsync(FilterDTO filter);
    }
}
