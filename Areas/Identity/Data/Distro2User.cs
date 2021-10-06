using System;
using Microsoft.AspNetCore.Identity;

namespace Distro2.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the Distro2User class
    public class Distro2User : IdentityUser
    {
        public DateTime LastLoginDate { get; set; }
        public int NrOfLogins { get; set; }
        public int NrOfDeletions { get; set; }
    }
}
