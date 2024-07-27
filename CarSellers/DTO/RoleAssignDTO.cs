using System.ComponentModel.DataAnnotations;

namespace CarSellers.DTO {
    public class RoleAssignDTO {
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
