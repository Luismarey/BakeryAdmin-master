namespace BakeryAdmin.Models.ViewModels {
    public class RolesModels
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public List<RolCheckbox> Roles { get; set; }
    }

    public class RolCheckbox
    {
        public string NombreRol { get; set; }
        public bool IsSelected { get; set; }
    }
}