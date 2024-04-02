using EmailSendingApplication.DTO_s;
using EmailSendingApplication.IServices;
using EmailSendingApplicationMVC.IServices;
using EmailSendingApplicationMVC.Services;
using EmailSendingApplicationMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmailSendingApplicationMVC.Controllers
{
    public class MailSendingMVCController : Controller
    {
        private readonly IMailSendingAPIService _mailSendingService;

        public MailSendingMVCController(IMailSendingAPIService mailSendingService)
        {
            _mailSendingService = mailSendingService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] EmailViewModel emailViewModel)
        {
           await _mailSendingService.SendEmailAsync(emailViewModel.SenderId, emailViewModel.RecipientIds, emailViewModel.Subject, emailViewModel.Body);

            return Ok(new { message = "Email sent successfully" });
        }

        [HttpGet]
        public async Task<List<ResponseSelectBoxViewModel>> GetRecipientsFromFilter([FromQuery] FilterDTO filter)
        {
            var mailRecipients = await _mailSendingService.GetMailRecipientsFilterAsync(filter);
            var res = new List<ResponseSelectBoxViewModel>();
            foreach (var item in mailRecipients)
            {
                res.Add(new ResponseSelectBoxViewModel()
                {
                    Text = item.Email,
                    Value = item.Id
                });
            }
            return res;
        }

        [HttpGet]
        public async Task<List<ResponseSelectBoxViewModel>> GetRecipients()
        {
            var mailRecipients = await _mailSendingService.GetMailRecipientsAsync();
            var res = new List<ResponseSelectBoxViewModel>();
            foreach (var item in mailRecipients)
            {
                res.Add(new ResponseSelectBoxViewModel()
                {
                    Text = item.Email,
                    Value = item.Id
                });
            }
            return res;
        }

        [HttpGet]
        public async Task<List<ResponseSelectBoxViewModel>> GetSenders()
        {
            var mailRecipients = await _mailSendingService.GetMailSendersAsync();
            var res = new List<ResponseSelectBoxViewModel>();
            foreach (var item in mailRecipients)
            {
                res.Add(new ResponseSelectBoxViewModel()
                {
                    Text = item.Email,
                    Value = item.Id
                });
            }
            return res;
        }
    }
}
