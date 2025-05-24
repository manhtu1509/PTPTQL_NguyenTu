namespace PTPMQLMvc.Models.ViewModel
{
    public class AssignRolesVM
    {
        public string UserId { get; set; }
        public IList<string> SelectionRoles { get; set; }
        public IList<RoleVM>? ALLRoles { get; set; }

    }
    public class RoleVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    
    
}