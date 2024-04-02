using EmailSendingApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace EmailSendingApplicationMVC.ViewModels
{
    public class RequestSentMailViewModel
    {
        public string? Subject { get; set; }
        public string? Body { get; set; }
        [DataType(DataType.Date)]
        public DateTime SendingDate { get; set; }
        public bool TransmissionStatus { get; set; }
        public MailSenders MailSender { get; set; }
        public IEnumerable<MailSenders> MailSenderList { get; set; }
        public List<MailRecipient> mailRecipients { get; set; }
    }
}
