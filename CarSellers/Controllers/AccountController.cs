﻿using CarSellers.DTO;
using CarSellers.Interface;
using CarSellers.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarSellers.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager) {
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AccountRegisterDTO registerDTO) {
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
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded) {
                        return Ok(
                            new AccountReturnDTO {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
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
                Token = await _tokenService.CreateToken(user)
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
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDTO updateUserDto)
        {
            if (updateUserDto == null)
            {
                return BadRequest("Invalid user data.");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Assuming user ID is in claims
            var user = await _userManager.FindByIdAsync(userId);

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
           var updatedUser =await  _userManager.FindByNameAsync(user.UserName);
            var userReturnDTO = new UserUpdateReturnDTO
            {
                Username = updatedUser.UserName,
                Email = updatedUser.Email,
            };
            return Ok(userReturnDTO);
        }
    }
}
