using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class Spa
    {
        [Key]
        public int SpaId { get; set; }
        [Display(Name = "Name")]
        public string SpaCustomerName { get; set; }
        [Display(Name = "Date")]
        public DateTime BookingDate { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Time)]
        [Display(Name = "End Time")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime EndTime { get; set; }
        [Display(Name = "Package")]
        public string SpaPackage { get; set; }
        public string Service { get; set; }
        [Display(Name = "Confirmed")]
        public bool IsConfirmed { get; set; }
    }
}
