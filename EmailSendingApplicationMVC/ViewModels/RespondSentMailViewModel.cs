using System.ComponentModel.DataAnnotations;

namespace EmailSendingApplicationMVC.ViewModels
{
    public class RespondSentMailViewModel
    {
        public int Id { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public DateTime SendingDate { get; set; }
        public bool TransmissionStatus { get; set; }
        [DataType(DataType.Date)]
        public string SenderMail { get; set; }
        public ICollection<string> RecipientMails { get; set; }
    }
}
