using Microsoft.AspNetCore.Identity;

namespace BakeryAdmin.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? NombreCompleto { get; set; }
        public string? IsActive { get; set; } 
        public bool MustChangePassword { get; set; }  
    }
}
