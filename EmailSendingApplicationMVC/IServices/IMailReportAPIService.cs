using EmailSendingApplication.DTO_s;
using Microsoft.AspNetCore.Mvc;

namespace EmailSendingApplicationMVC.IServices
{
    public interface IMailReportAPIService
    {
        Task<ActionResult<IEnumerable<RespondSentMailDTO>>> GetSentMailsByEmailAsync(string senderEmail);
        Task<List<RespondSentMailDTO>> GetSentMailsAsync();
        List<RespondSentMailDTO> FilterDistinctSenders(List<RespondSentMailDTO> sentMails);
    }
}
