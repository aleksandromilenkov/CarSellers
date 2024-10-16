namespace CarSellers.DTO
{
    public class UserUpdateReturnDTO
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string? ProfileImage { get; set; }
    }
}
