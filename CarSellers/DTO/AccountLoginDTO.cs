using System.ComponentModel.DataAnnotations;

namespace CarSellers.DTO {
    public class AccountLoginDTO {
        [Required]

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
