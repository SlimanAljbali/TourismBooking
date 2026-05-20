using Microsoft.EntityFrameworkCore;

namespace TourismBooking.Data
{
    public class TourismDbContext : DbContext
    {

        public TourismDbContext(DbContextOptions<TourismDbContext> options) : base(options)
        {
        }
        public DbSet<Models.Hotels> Hotels { get; set; }
        public DbSet<Models.Restaurants> Restaurants { get; set; }
        public DbSet<Models.Entertainment> Entertainment { get; set; }
            public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Booking> Bookings { get; set; }



    }
}
