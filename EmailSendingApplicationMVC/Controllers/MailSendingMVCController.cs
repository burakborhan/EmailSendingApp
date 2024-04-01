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
        // GET action for displaying the form to send an email
        [HttpGet]
        public IActionResult Index()
        {
            //var res = _mailSendingService.SendEmailAsync(1, new List<int> { 2, 3 }, "Test", "Test");
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

        //[HttpPost]
        //[Route("SendEmail")]
        //public ObjectResult SendEmail([FromBody] EmailViewModel emailViewModel)
        //{
        //    // Handle email sending logic asynchronously
        //    //_mailSendingService.SendEmailAsync(emailViewModel.SenderId, emailViewModel.RecipientIds, emailViewModel.Subject, emailViewModel.Body);

        //    return Ok(new { message = "Email sent successfully" });
        //}


        // POST action to actually send the email
        //[HttpPost]
        //public async Task<IActionResult> SendEmail(RespondSentMailDTO sentMailDto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            await _mailSendingService.SendEmailAsync(sentMailDto, sentMailDto.RecipientIds, sentMailDto.Subject, sentMailDto.Body);
        //            return RedirectToAction("Index", "Home"); // Redirect to a success page
        //        }
        //        catch (Exception ex)
        //        {
        //            ModelState.AddModelError(string.Empty, "An error occurred while sending the email.");
        //        }
        //    }

        //    return View(model);
        //}

    }
}
