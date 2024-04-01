using EmailSendingApplication.DTO_s;

namespace EmailSendingApplicationMVC.IServices
{
    public interface IMailRecipientAPIService
    {
        Task<List<RespondMailRecipientDTO>> GetMailRecipientsAsync();
        Task<RespondMailRecipientDTO> GetMailRecipientByIdAsync(int id);
        Task CreateMailRecipientAsync(RequestMailRecipientDTO mailRecipientDto);
        Task UpdateMailRecipientAsync(RespondMailRecipientDTO mailRecipientDto);
        Task<bool> DeleteMailRecipientAsync(int id);
    }
}
