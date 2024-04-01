using System.ComponentModel.DataAnnotations;

namespace EmailSendingApplicationMVC.ViewModels
{
    public class RespondMailSenderViewModel
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string? Username { get; set; }
        [Required]
        public bool EnableSSL { get; set; }
        [Required]
        public string MailPassword { get; set; }
        [Required]
        public string MailServerAddress { get; set; }
        [Required]
        public int Port { get; set; }
    }
}
