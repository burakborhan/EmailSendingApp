namespace EmailSendingApplication.Models
{
    public class MailSenders
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool EnableSSL { get; set; }
        public string MailServerAddress { get; set; }
        public string Username { get; set; }
        public string MailPassword { get; set; }
        public int Port {  get; set; }
    }
}
