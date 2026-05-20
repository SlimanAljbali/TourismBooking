using Microsoft.AspNetCore.Mvc;
using TourismBooking.Data;

namespace TourismBooking.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TourismUsersController : ControllerBase
    {

        private readonly TourismDbContext _context;
        public TourismUsersController(TourismDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Get()
        {
            if(_context.Bookings == null)
            {
                return NotFound();
            }

            return Ok(_context.Users.ToList());

        }
    }
}
