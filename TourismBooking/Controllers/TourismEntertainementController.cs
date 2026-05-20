using Microsoft.AspNetCore.Mvc;
using TourismBooking.Data;

namespace TourismBooking.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TourismEntertainementController : ControllerBase
    {

        private readonly TourismDbContext _context;
        public TourismEntertainementController(TourismDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Get()
        {
            if(_context.Entertainment == null)
            {
                return NotFound();
            }

            return Ok(_context.Entertainment.ToList());

        }
    }
}
