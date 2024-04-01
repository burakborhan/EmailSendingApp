using AutoMapper;
using EmailSendingApplication.Data;
using EmailSendingApplication.DTO_s;
using EmailSendingApplication.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmailSendingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailSendingController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MailSendingController(IEmailService emailService, AppDbContext context, IMapper mapper)
        {
            _emailService = emailService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RespondSentMailDTO>>> GetEmailReports(string senderEmail)
        {
            var sentMails = await _context.SentMail
                .Where(mail => mail.SenderMail == senderEmail)
                .ToListAsync();

            var emailReports = sentMails.Select(mail => new RespondSentMailDTO
            {
                Subject = mail.Subject,
                Body = mail.Body,
                SendingDate = mail.SendingDate,
                TransmissionStatus = mail.TransmissionStatus,
                RecipientMails = mail.RecipientMails.ToList()
            }).ToList();

            return emailReports;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail(/*[FromBody]*/ RequestSentMailDTO request)
        {
            try
            {
                var recipientIds = request.mailRecipients.Select(r => r.Id).ToList();
                await _emailService.SendEmailAsync(request.MailSender.Id, recipientIds, request.Subject, request.Body);
                return Ok("Email sent successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while sending the email.");
            }
        }
        [HttpGet("filter")]
        public ActionResult<IEnumerable<RespondMailRecipientDTO>> GetFilteredRecipients([FromQuery] string? name, [FromQuery] string? surname, [FromQuery] string? gender, [FromQuery] int? minAge, [FromQuery] int? maxAge, [FromQuery] string? email, [FromQuery] string? homePhoneNo, [FromQuery] string? cellPhoneNo, [FromQuery] string? title, [FromQuery] string? workPlace)
        {
            var query = _context.MailRecipient.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(r => r.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(surname))
            {
                query = query.Where(r => r.Surname.Contains(surname));
            }

            if (!string.IsNullOrEmpty(gender))
            {
                query = query.Where(r => r.Gender == gender);
            }

            if (minAge.HasValue && maxAge.HasValue)
            {
                var today = DateTime.Today;
                query = query.Where(r => r.Birthday != null &&
                                         (today.Year - r.Birthday.Year -
                                         (today.DayOfYear < r.Birthday.DayOfYear ? 1 : 0)) >= minAge &&
                                         (today.Year - r.Birthday.Year -
                                         (today.DayOfYear < r.Birthday.DayOfYear ? 1 : 0)) <= maxAge);
            }
            if (minAge.HasValue && !maxAge.HasValue)
            {
                var today = DateTime.Today;
                query = query.Where(r => r.Birthday != null && (today.Year - r.Birthday.Year - (today.DayOfYear < r.Birthday.DayOfYear ? 1 : 0)) >= minAge);
            }
            if (!minAge.HasValue && maxAge.HasValue)
            {
                var today = DateTime.Today;
                query = query.Where(r => r.Birthday != null && (today.Year - r.Birthday.Year - (today.DayOfYear < r.Birthday.DayOfYear ? 1 : 0)) <= maxAge);
            }
            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(r => r.Email.Contains(email));
            }

            if (!string.IsNullOrEmpty(homePhoneNo))
            {
                query = query.Where(r => r.HomePhoneNo.Contains(homePhoneNo));
            }

            if (!string.IsNullOrEmpty(cellPhoneNo))
            {
                query = query.Where(r => r.CellPhoneNo.Contains(cellPhoneNo));
            }

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(r => r.Title.Contains(title));
            }

            if (!string.IsNullOrEmpty(workPlace))
            {
                query = query.Where(r => r.WorkPlace.Contains(workPlace));
            }

            var filteredRecipients = query.Select(r => new RespondMailRecipientDTO
            {
                Name = r.Name,
                Surname = r.Surname,
                Gender = r.Gender,
                Birthday = r.Birthday,
                Email = r.Email,
                HomePhoneNo = r.HomePhoneNo,
                CellPhoneNo = r.CellPhoneNo,
                Title = r.Title,
                WorkPlace = r.WorkPlace
            })
                .ToList();

            return Ok(filteredRecipients);
        }
    }
}
