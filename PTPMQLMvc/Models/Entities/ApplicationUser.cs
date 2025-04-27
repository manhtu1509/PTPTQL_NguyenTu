using Microsoft.AspNetCore.Identity;
namespace PTPMQLMvc.Models
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string? FullName {get; set;}
    }
}