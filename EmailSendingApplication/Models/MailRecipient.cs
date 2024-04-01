using System.ComponentModel.DataAnnotations;

namespace EmailSendingApplication.Models
{
    public class MailRecipient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string HomePhoneNo { get; set; }
        public string CellPhoneNo { get; set; }
        public string Title { get; set; }
        public string WorkPlace { get; set; }
    }
}
