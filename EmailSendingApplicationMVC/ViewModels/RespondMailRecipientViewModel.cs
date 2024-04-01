using System.ComponentModel.DataAnnotations;

namespace EmailSendingApplicationMVC.ViewModels
{
    public class RespondMailRecipientViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Gender { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string? HomePhoneNo { get; set; }
        [Required]
        public string CellPhoneNo { get; set; }
        public string? Title { get; set; }
        public string? WorkPlace { get; set; }
    }
}
