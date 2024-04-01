using EmailSendingApplication.Controllers;
using EmailSendingApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace EmailSendingApplication.DTO_s
{
    public class RespondSentMailDTO
    {
        public int Id { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        [DataType(DataType.Date)]
        public DateTime SendingDate { get; set; }
        public bool TransmissionStatus { get; set; }
        
        public string SenderMail { get; set; }
         public MailSenders MailSender { get; set; }
        public ICollection<string> RecipientMails { get; set; }
    }
}
