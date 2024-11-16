using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Models;
using Microsoft.AspNetCore.Identity;

namespace HotelManagement.Areas.Identity.Data;

// Add profile data for application users by adding properties to the HotelUser class
public class HotelUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserRole { get; set; }

}

