namespace CarSellers.DTO
{
    public class UserUpdateDTO
    {
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? CurrentPassword { get; set; }
        public string? NewPassword { get; set; }
        public IFormFile? ProfileImage { get; set; }
    }
}
