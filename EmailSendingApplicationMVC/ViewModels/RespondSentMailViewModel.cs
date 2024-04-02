using EmailSendingApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace EmailSendingApplicationMVC.ViewModels
{
    public class RespondSentMailViewModel
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
        public List<MailRecipient> mailRecipients { get; set; }
    }
}
