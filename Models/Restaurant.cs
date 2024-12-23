﻿using HotelManagement.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations;

namespace HotelManagement.Models
{
    public class Restaurant
    {
        public int RestaurantID { get; set; }
        [Display(Name = "Name")]
        public string CustomerName { get; set; }
        [Display(Name = "Date")]
        public DateTime BookingTime { get; set; }
        [Display(Name = "Number of Guests")]
        public int NumberOfGuests { get; set; }
        public bool Confirmed { get; set; }
        public string UserId { get; set; }
        public HotelUser User { get; set; } // Link to HotelUser
    }
}
