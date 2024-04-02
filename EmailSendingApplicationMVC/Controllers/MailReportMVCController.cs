using EmailSendingApplicationMVC.Services;
using EmailSendingApplicationMVC.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmailSendingApplicationMVC.Controllers
{
    public class MailReportController : Controller
    {
        private readonly IMailReportAPIService _mailReportAPIService;

        public MailReportController(IMailReportAPIService mailReportAPIService)
        {
            _mailReportAPIService = mailReportAPIService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var mailReports = await _mailReportAPIService.GetSentMailsAsync();

            var distinctEmails = mailReports.Select(r => r.SenderMail).Distinct().ToList();

            ViewBag.EmailList = distinctEmails;

            return View(mailReports);
        }

        [HttpGet]
        public async Task<IActionResult> GetMailReportsAsync(string selectedEmail)
        {
            var mailReports = await _mailReportAPIService.GetSentMailsAsync();
            var selectedEmailReports = mailReports.Where(r => r.MailSender.Email == selectedEmail).ToList();

            return View("Index", selectedEmailReports);
        }
    }
}