namespace PTPMQLMvc.Models.ViewModel
{
    public class UserWithRoleVM
    {
        public ApplicationUser User { get; set; }
        public IList<string> Roles { get; set; }
    }
}