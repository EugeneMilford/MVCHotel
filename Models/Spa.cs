using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class Spa
    {
        [Key]
        public int SpaId { get; set; }
        public string SpaCustomerName { get; set; }
        public DateTime BookingDate { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime EndTime { get; set; }
        public string SpaPackage { get; set; }
        public string Service { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
