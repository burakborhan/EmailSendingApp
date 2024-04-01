namespace EmailSendingApplicationMVC.ViewModels
{
    public class EmailViewModel
    {
        public int SenderId { get; set; }
        public List<int> RecipientIds { get; set; }
        public string Body { get; set; }
        public string Subject { get; set; }
    }
}
