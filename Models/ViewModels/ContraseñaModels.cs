using System.ComponentModel.DataAnnotations;
using BakeryAdmin.Models.ViewModels;

namespace BakeryAdmin.Models.ViewModels {
    public class ContraseñaModels {
        public string UserId { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string Confirmar { get; set; } = string.Empty;
    }
}