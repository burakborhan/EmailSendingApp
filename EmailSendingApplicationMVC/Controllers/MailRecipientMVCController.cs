using EmailSendingApplication.DTO_s;
using EmailSendingApplicationMVC.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EmailSendingApplicationMVC.Controllers
{
    public class MailRecipientMVCController : Controller
    {
        private readonly IMailRecipientAPIService _mailRecipientService ;
        public MailRecipientMVCController(IMailRecipientAPIService mailRecipientService)
        {
            _mailRecipientService = mailRecipientService;
        }
        public async Task<IActionResult> Index()
        {
            var mailRecipients = await _mailRecipientService.GetMailRecipientsAsync();
            return View(mailRecipients);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RequestMailRecipientDTO mailRecipientDto)
        {
            if (ModelState.IsValid)
            {
                await _mailRecipientService.CreateMailRecipientAsync(mailRecipientDto);
                return RedirectToAction(nameof(Index));
            }

            return View(mailRecipientDto);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var mailRecipient = await _mailRecipientService.GetMailRecipientByIdAsync(id);

            if (mailRecipient == null)
            {
                return NotFound();
            }

            return View(mailRecipient);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RespondMailRecipientDTO mailRecipientDto, int id)
        {
            if (ModelState.IsValid)
            {
                await _mailRecipientService.UpdateMailRecipientAsync(mailRecipientDto);
                return RedirectToAction(nameof(Index));
            }

            return View(mailRecipientDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _mailRecipientService.DeleteMailRecipientAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
