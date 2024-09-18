using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class Contact
    {
        public int ContactId{get;set;}
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(1000)]
        public string Message { get; set; }
    }
}
