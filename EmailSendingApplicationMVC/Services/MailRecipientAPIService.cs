using EmailSendingApplication.DTO_s;
using EmailSendingApplicationMVC.IServices;
using System.Text;
using System.Text.Json;

namespace EmailSendingApplicationMVC.Services
{
    public class MailRecipientAPIService : IMailRecipientAPIService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://'...'/api/MailRecipient";

        public MailRecipientAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RespondMailRecipientDTO>> GetMailRecipientsAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var mailRecipients = JsonSerializer.Deserialize<List<RespondMailRecipientDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return mailRecipients;
        }

        public async Task<RespondMailRecipientDTO> GetMailRecipientByIdAsync(int id)
        {
            var url = $"{BaseUrl}/{id}";
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var mailRecipient = JsonSerializer.Deserialize<RespondMailRecipientDTO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return mailRecipient;
        }

        public async Task CreateMailRecipientAsync(RequestMailRecipientDTO mailRecipientDto)
        {
            var json = JsonSerializer.Serialize(mailRecipientDto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(BaseUrl, data);
            response.EnsureSuccessStatusCode();
        }
        public async Task UpdateMailRecipientAsync(RespondMailRecipientDTO mailRecipientDto)
        {
            var json = JsonSerializer.Serialize(mailRecipientDto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"{BaseUrl}/{mailRecipientDto.Id}";
            var response = await _httpClient.PutAsync(url, data);
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> DeleteMailRecipientAsync(int id)
        {
            var url = $"{BaseUrl}/{id}";
            var response = await _httpClient.DeleteAsync(url);
            return response.IsSuccessStatusCode;
        }
    }
}
