using CarSellers.DTO;
using CarSellers.Interface;
using CarSellers.Model;
using CarSellers.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace CarSellers.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IFileService _fileService;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IFileService fileService, IEmailSender emailSender)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;
            _roleManager = roleManager;
            _emailSender = emailSender;
            this._fileService = fileService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] AccountRegisterDTO registerDTO) {
            try {
                if (!ModelState.IsValid) {
                    return BadRequest(ModelState);
                }
                var userAlreadyExists = await _userManager.FindByEmailAsync(registerDTO.Email);
                if (userAlreadyExists != null) {
                    return BadRequest("User already exists.");
                }
                var appUser = new AppUser {
                    UserName = registerDTO.UserName,
                    Email = registerDTO.Email,
                };
                var createdUser = await _userManager.CreateAsync(appUser, registerDTO.Password);
                if (createdUser.Succeeded) {
                    // Save profile picture if it exists
                    if (registerDTO.ProfileImage != null)
                    {
                        string[] allowedFileExtensions = { ".jpg", ".jpeg", ".png" }; 
                        try
                        {
                            var createdImageName = await _fileService.SaveFileAsync(registerDTO.ProfileImage, allowedFileExtensions);
                            appUser.ProfilePicture = createdImageName;
                        }
                        catch (Exception ex)
                        {
                            return BadRequest($"Error saving profile picture: {ex.Message}");
                        }

                        await _userManager.UpdateAsync(appUser); // Update user with the profile picture filename
                    }
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded) {
                        return Ok(
                            new AccountReturnDTO {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                ProfileImage = appUser.ProfilePicture,
                                Token = await _tokenService.CreateToken(appUser)
                            }
                        );
                    }
                    else {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e) {
                return StatusCode(500, e);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AccountLoginDTO loginDTO) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginDTO.UserName);
            if (user == null) {
                return Unauthorized("Invalid username!");
            }
            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
            if (!result.Succeeded) {
                return Unauthorized("Invalid username or password");
            }
            var userToReturn = new AccountReturnDTO {
                UserName = user.UserName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user),
                ProfileImage = user?.ProfilePicture,
            };
            return Ok(userToReturn);
        }

        [HttpPost("assignRole")]
        [Authorize]
        public async Task<IActionResult> AssignRole([FromBody] RoleAssignDTO model) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) {
                return NotFound("User not found");
            }
            var roleExists = await _roleManager.RoleExistsAsync(model.Role);
            if (!roleExists) {
                return BadRequest("Role does not exist");
            }

            var result = await _userManager.AddToRoleAsync(user, model.Role);
            if (!result.Succeeded) {
                return BadRequest("Failed to assign role");
            }

            return Ok("Role assigned successfully");
        }

        [HttpPatch("update")]
        [Authorize]
        public async Task<IActionResult> UpdateUser([FromForm] UserUpdateDTO updateUserDto)
        {
            if (updateUserDto == null)
            {
                return BadRequest("Invalid user data.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Assuming user ID is in claims
            var user = await _userManager.FindByIdAsync(userId);
            var oldImage = user?.ProfilePicture;

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Update email
            if (!string.IsNullOrEmpty(updateUserDto.Email) && user.Email != updateUserDto.Email)
            {
                var emailChangeResult = await _userManager.SetEmailAsync(user, updateUserDto.Email);
                if (!emailChangeResult.Succeeded)
                {
                    return BadRequest("Failed to update email.");
                }
            }

            // Update username
            if (!string.IsNullOrEmpty(updateUserDto.Username) && user.UserName != updateUserDto.Username)
            {
                var usernameChangeResult = await _userManager.SetUserNameAsync(user, updateUserDto.Username);
                if (!usernameChangeResult.Succeeded)
                {
                    return BadRequest("Failed to update username.");
                }
            }

            // Update password if provided
            if (!string.IsNullOrEmpty(updateUserDto.CurrentPassword) && !string.IsNullOrEmpty(updateUserDto.NewPassword))
            {
                var passwordChangeResult = await _userManager.ChangePasswordAsync(user, updateUserDto.CurrentPassword, updateUserDto.NewPassword);
                if (!passwordChangeResult.Succeeded)
                {
                    return BadRequest(passwordChangeResult.Errors);
                }
            }

            // Update profile image if provided
            if (updateUserDto.ProfileImage != null)
            {
                if (updateUserDto.ProfileImage?.Length > 1 * 1024 * 1024)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 1 MB");
                }
                string[] allowedFileExtentions = { ".jpg", ".jpeg", ".png" };
                string createdImageName = await _fileService.SaveFileAsync(updateUserDto.ProfileImage, allowedFileExtentions);
                user.ProfilePicture = createdImageName;
                var updatedUser1 = await _userManager.UpdateAsync(user);
                if (!updatedUser1.Succeeded)
                {
                    return BadRequest(updatedUser1.Errors);
                }
                if(oldImage != null) _fileService.DeleteFile(oldImage);
            }
            var updatedUser =await _userManager.FindByNameAsync(user.UserName);
            var userReturnDTO = new UserUpdateReturnDTO
            {
                Username = updatedUser.UserName,
                Email = updatedUser.Email,
                ProfileImage = updatedUser.ProfilePicture,
            };
            return Ok(userReturnDTO);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDTO request)
        {
            try { 
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    return BadRequest("User with this email does not exist.");
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                var resetLink = $"localhost:3000/reset-password?token={encodedToken}&email={request.Email}";

                // Send the reset link via email (this is just a placeholder for email logic)
                await _emailSender.SendEmailAsync(request.Email, "<p> Password Reset", $"Reset your password <a href={resetLink} target=\"_blank\"> HERE </a> </p>");

                return Ok("Password reset link has been sent to your email.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDTO request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return BadRequest("Invalid email.");
            }
            var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var resetPassResult = await _userManager.ResetPasswordAsync(user, decodedToken, request.NewPassword);
            if (!resetPassResult.Succeeded)
            {
                return BadRequest(resetPassResult.Errors);
            }

            return Ok("Password has been reset successfully.");
        }
    }
}
