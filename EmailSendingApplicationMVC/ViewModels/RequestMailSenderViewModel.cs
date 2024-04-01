using System.ComponentModel.DataAnnotations;

namespace EmailSendingApplicationMVC.ViewModels
{
    public class RequestMailSenderViewModel
    {
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
