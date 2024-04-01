using EmailSendingApplication.DTO_s;
using EmailSendingApplication.IServices;
using EmailSendingApplicationMVC.IServices;
using EmailSendingApplicationMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmailSendingApplicationMVC.Controllers
{
    public class MailSenderMVCController : Controller
    {
        private readonly IMailSenderAPIService _mailSenderService;
        public MailSenderMVCController(IMailSenderAPIService mailSenderService)
        {
            _mailSenderService = mailSenderService;
        }
        public async Task<IActionResult> Index()
        {
            var mailSenders = await _mailSenderService.GetMailSendersAsync();
            return View(mailSenders);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RequestMailSenderDTO mailSenderDto)
        {
            if (ModelState.IsValid)
            {
                await _mailSenderService.CreateMailSenderAsync(mailSenderDto);
                return RedirectToAction(nameof(Index));
            }

            return View(mailSenderDto);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var mailSender = await _mailSenderService.GetMailSenderByIdAsync(id);

            if (mailSender == null)
            {
                return NotFound();
            }

            return View(mailSender);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RespondMailSenderDTO mailSenderDto,int id)
        {
            if (ModelState.IsValid)
            {
                await _mailSenderService.UpdateMailSenderAsync(mailSenderDto);
                return RedirectToAction(nameof(Index));
            }

            return View(mailSenderDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
                await _mailSenderService.DeleteMailSenderAsync(id);
                return RedirectToAction(nameof(Index));
        }

    }
}
