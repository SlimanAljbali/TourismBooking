using Microsoft.AspNetCore.Mvc;
using TourismBooking.Data;

namespace TourismBooking.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TourismRestaurantsController : ControllerBase
    {

        private readonly TourismDbContext _context;
        public TourismRestaurantsController(TourismDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Get()
        {
            if(_context.Restaurants == null)
            {
                return NotFound();
            }

            return Ok(_context.Restaurants.ToList());

        }
    }
}
