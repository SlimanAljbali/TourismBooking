namespace TourismBooking.Models
{
    public class Booking
    {

        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public Guid HotelId { get; set; }
        public Guid RestaurantId { get; set; }
        public Guid EntertainmentId { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime CreationTime { get; set; }
        public bool HasCustomerAttend { get; set; }
        public string CustomerReview { get; set; }
        public int CustomerRating { get; set; }


    }
}
