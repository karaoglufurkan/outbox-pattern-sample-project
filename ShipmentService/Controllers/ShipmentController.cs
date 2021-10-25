using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShipmentService.Data;

namespace ShipmentService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShipmentController : ControllerBase
    {
        private readonly ShipmentServiceDbContext _context;

        public ShipmentController(ShipmentServiceDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetShipments()
        {
            return Ok(await _context.Shipments.ToListAsync());
        }
    }
}