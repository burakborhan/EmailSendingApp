using AutoMapper;
using EmailSendingApplication.Data;
using EmailSendingApplication.DTO_s;
using EmailSendingApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmailSendingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailRecipientController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MailRecipientController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RespondMailRecipientDTO>>> GetMailRecipients()
        {
            var mailRecipient = await _context.MailRecipient.ToListAsync();
            var mailRecipientDtos = _mapper.Map<List<RespondMailRecipientDTO>>(mailRecipient);
            return mailRecipientDtos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RespondMailRecipientDTO>> GetMailRecipientById(int id)
        {
            var mailRecipient = await _context.MailRecipient.FindAsync(id);

            if (mailRecipient == null)
            {
                return NotFound();
            }

            var mailRecipientDto = _mapper.Map<RespondMailRecipientDTO>(mailRecipient);
            return mailRecipientDto;
        }

        [HttpPost]
        public async Task<ActionResult<MailRecipient>> PostMailRecipient(RequestMailRecipientDTO mailRecipientDto)
        {
            if (!IsEmailUnique(mailRecipientDto.Email))
            {
                return BadRequest("The same email address is already registered!");
            }
            else
            {
                DateTime.SpecifyKind(mailRecipientDto.Birthday, DateTimeKind.Utc);
                var mailRecipient = _mapper.Map<MailRecipient>(mailRecipientDto);
                _context.MailRecipient.Add(mailRecipient);
                _context.SaveChanges();

                return CreatedAtAction("GetMailRecipients", new { id = mailRecipient.Id }, mailRecipientDto);
            }
        }

        

        [HttpPut("{id}")]
        public async Task<IActionResult> EditMailRecipient(int id, RespondMailRecipientDTO mailRecipientDto)
        {
            var mailRecipient = await _context.MailRecipient.FindAsync(id);
            if (mailRecipient == null)
            {
                return NotFound();
            }
            _mapper.Map(mailRecipientDto, mailRecipient);
            _context.Entry(mailRecipient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MailRecipientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMailRecipient(int id)
        {
            var mailRecipient = await _context.MailRecipient.FindAsync(id);
            if (mailRecipient == null)
            {
                return NotFound();
            }

            _context.MailRecipient.Remove(mailRecipient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MailRecipientExists(int id)
        {
            return _context.MailRecipient.Any(e => e.Id == id);
        }

        private bool IsEmailUnique(string email)
        {
            return !_context.MailSender.Any(m => m.Email == email);
        }
    }
}
