using EmailSendingApplication.DTO_s;

namespace EmailSendingApplicationMVC.IServices
{
    public interface IMailSenderAPIService
    {
        Task<List<RespondMailSenderDTO>> GetMailSendersAsync();
        Task<RespondMailSenderDTO> GetMailSenderByIdAsync(int id);
        Task CreateMailSenderAsync(RequestMailSenderDTO mailSenderDto);
        Task UpdateMailSenderAsync(RespondMailSenderDTO mailSenderDto);
        Task<bool> DeleteMailSenderAsync(int id);
    }
}
