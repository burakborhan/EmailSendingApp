using EmailSendingApplication.Controllers;
using EmailSendingApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace EmailSendingApplication.DTO_s
{
    public class RequestSentMailDTO
    {
        
        public string? Subject { get; set; }
        public string? Body { get; set; }
        [DataType(DataType.Date)]
        public DateTime SendingDate { get; set; }
        public bool TransmissionStatus { get; set; }
        public MailSenders MailSender { get; set; }
        public List<MailRecipient> mailRecipients { get; set; }
    }
}
