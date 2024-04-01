using System.ComponentModel.DataAnnotations;

namespace EmailSendingApplication.DTO_s
{
    public class RespondMailSenderDTO
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public bool EnableSSL { get; set; }
        public string? Username { get; set; }
        [Required]
        public string MailPassword { get; set; }
        [Required]
        public string MailServerAddress { get; set; }
        [Required]
        public int Port { get; set; }
    }
}
