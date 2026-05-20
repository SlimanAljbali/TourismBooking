using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;

namespace TourismBooking.Models
{

    [Table("User", Schema = "Security") ]
    public class User
    {

        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateOnly BOD { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string ZipCode { get; set; }


       
    }
}
