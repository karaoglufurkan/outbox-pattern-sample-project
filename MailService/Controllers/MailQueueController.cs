using System.Threading.Tasks;
using MailService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MailService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailQueueController : ControllerBase
    {
        private readonly MailServiceDbContext _context;

        public MailQueueController(MailServiceDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMailQueue()
        {
            return Ok(await _context.MailQueue.ToListAsync());
        }
    }
}