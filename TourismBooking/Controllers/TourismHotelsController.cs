using Microsoft.AspNetCore.Mvc;
using TourismBooking.Data;

namespace TourismBooking.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TourismHotelsController : ControllerBase
    {

        private readonly TourismDbContext _context;
        public TourismHotelsController(TourismDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Get()
        {
            if(_context.Hotels == null)
            {
                return NotFound();
            }

            return Ok(_context.Hotels.ToList());

        }
    }
}
