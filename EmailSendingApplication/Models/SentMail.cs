using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace EmailSendingApplication.Models
{
    public class SentMail
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        [DataType(DataType.Date)]
        public DateTime SendingDate { get; set; }
        public bool TransmissionStatus { get; set; }
        public string SenderMail { get; set; }
        public List<string> RecipientMails { get; set; }
        public virtual MailSenders MailSenders { get; set; }
        public virtual IEnumerable<MailRecipient> MailRecipients { get; set; }
    }
}
