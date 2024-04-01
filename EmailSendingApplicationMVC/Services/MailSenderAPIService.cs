using EmailSendingApplication.DTO_s;
using System.Text.Json;
using System.Text;
using Microsoft.EntityFrameworkCore;
using EmailSendingApplication.IServices;
using EmailSendingApplicationMVC.IServices;

namespace EmailSendingApplicationMVC.Services
{
    public class MailSenderAPIService : IMailSenderAPIService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7183/api/MailSender";

        public MailSenderAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RespondMailSenderDTO>> GetMailSendersAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var mailSenders = JsonSerializer.Deserialize<List<RespondMailSenderDTO>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return mailSenders;
        }

        public async Task<RespondMailSenderDTO> GetMailSenderByIdAsync(int id)
        {
            var url = $"{BaseUrl}/{id}";
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var mailSender = JsonSerializer.Deserialize<RespondMailSenderDTO>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return mailSender;
        }

        public async Task CreateMailSenderAsync(RequestMailSenderDTO mailSenderDto)
        {
            var json = JsonSerializer.Serialize(mailSenderDto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(BaseUrl, data);
            response.EnsureSuccessStatusCode();
        }
        public async Task UpdateMailSenderAsync(RespondMailSenderDTO mailSenderDto)
        {
            var json = JsonSerializer.Serialize(mailSenderDto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var url = $"{BaseUrl}/{mailSenderDto.Id}";
            var response = await _httpClient.PutAsync(url, data);
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> DeleteMailSenderAsync(int id)
        {
            var url = $"{BaseUrl}/{id}";
            var response = await _httpClient.DeleteAsync(url);
            return response.IsSuccessStatusCode;
        }

    }
}
