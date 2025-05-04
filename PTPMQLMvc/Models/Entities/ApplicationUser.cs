using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PTPMQLMvc.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string? FullName { get; set; }
    }
}
