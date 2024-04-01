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
    public class MailSenderController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MailSenderController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RespondMailSenderDTO>>> GetMailSenders()
        {

            var mailSenders = await _context.MailSender.ToListAsync();
            var mailSenderDtos = _mapper.Map<List<RespondMailSenderDTO>>(mailSenders);
            return mailSenderDtos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RespondMailSenderDTO>> GetMailSenderById(int id)
        {
            var mailSender = await _context.MailSender.FindAsync(id);

            if (mailSender == null)
            {
                return NotFound();
            }

            var mailSenderDto = _mapper.Map<RespondMailSenderDTO>(mailSender);
            return mailSenderDto;
        }

        [HttpPost]
        public async Task<ActionResult<MailSenders>> PostMailSender(RequestMailSenderDTO mailSenderDto)
        {
            if (!IsEmailUnique(mailSenderDto.Email))
            {
                return BadRequest("The same email address is already registered!");
            }
            else
            {
                var mailSender = _mapper.Map<MailSenders>(mailSenderDto);
                _context.MailSender.Add(mailSender);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetMailSenders", new { id = mailSender.Id }, mailSenderDto);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditMailSender(int id, RespondMailSenderDTO mailSenderDto)
        {
            var mailSender = await _context.MailSender.FindAsync(id);
            if (mailSender == null)
            {
                return NotFound();
            }
            _mapper.Map(mailSenderDto, mailSender);
            _context.Entry(mailSender).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MailSenderExists(id))
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
        public async Task<IActionResult> DeleteMailSender(int id)
        {
            var mailSenders = await _context.MailSender.FindAsync(id);
            if (mailSenders == null)
            {
                return NotFound();
            }

            _context.MailSender.Remove(mailSenders);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MailSenderExists(int id)
        {
            return _context.MailSender.Any(e => e.Id == id);
        }

        private bool IsEmailUnique(string email)
        {
            return !_context.MailSender.Any(m => m.Email == email);
        }
    }
}
