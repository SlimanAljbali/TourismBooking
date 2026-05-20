using System.Reflection.Metadata.Ecma335;

namespace TourismBooking.Models
{
    public class Restaurants
    {

        public Guid Id { get; set; }


        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Cuisine { get; set; }
        public int cusomerRating { get; set; }



    }
}
