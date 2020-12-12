using System.Threading.Tasks;
using API.Database;
using AutoMapper;
using Data.Models.Classes;
using Data.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace API.Service
{
	public class UserService
	{
		private readonly UserDbRepository _userDbRepository;
		private readonly IMapper _userMapper;
        private readonly IConfiguration _appSettings;

		public UserService(DevHiveContext context, IMapper mapper, IConfiguration appSettings)
		{
			this._userDbRepository = new UserDbRepository(context);
			this._userMapper = mapper;
			this._appSettings = appSettings;
		}

		public async Task<IActionResult> LoginUser(LoginDTO loginDTO)
		{
			User user = this._userDbRepository.FindByUsername(loginDTO.UserName);

			if (user == null)
				return new NotFoundObjectResult("User does not exist!");

			// Get key from appsettings.json
			var key = Encoding.ASCII.GetBytes(_appSettings.GetSection("Secret").Value);  

			if (user.PasswordHash != GeneratePasswordHash(loginDTO.Password))
				return new BadRequestObjectResult("Incorrect password!");

			// Create Jwt Token configuration
			var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Role, user.Role) // Authorize user by role
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

			// Create Jwt Token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

			return new OkObjectResult(tokenString);
		}

		public async Task<IActionResult> RegisterUser(RegisterDTO registerDTO)
		{

			if (this._userDbRepository.DoesUsernameExist(registerDTO.UserName))
				return new BadRequestObjectResult("Username already exists!");

			User user = this._userMapper.Map<User>(registerDTO);

			user.Role = UserRoles.User;
			user.PasswordHash = GeneratePasswordHash(registerDTO.Password);

			await this._userDbRepository.AddAsync(user);

			return new CreatedResult("CreateUser", user);
		}

		private string GeneratePasswordHash(string password)
		{
			return password; // TEMPORARY!
		}

		public async Task<IActionResult> GetUserById(int id) 
		{
			User user = await this._userDbRepository.FindByIdAsync(id);

			if (user == null)
				return new NotFoundObjectResult("User does not exist!");

			return new OkObjectResult(user);
		}

		public async Task<IActionResult> UpdateUser(int id, UserDTO userDTO)
		{
			if (!this._userDbRepository.DoesUserExist(id))
				return new NotFoundObjectResult("User does not exist!");

			if (!this._userDbRepository.HasThisUsername(id, userDTO.UserName)
					&& this._userDbRepository.DoesUsernameExist(userDTO.UserName))
				return new BadRequestObjectResult("Username already exists!");

			User user = this._userMapper.Map<User>(userDTO);
			await this._userDbRepository.EditAsync(id, user);

			return new AcceptedResult("UpdateUser", user);
		}

		public async Task<IActionResult> DeleteUser(int id)
		{
			if (!this._userDbRepository.DoesUserExist(id))
				return new NotFoundObjectResult("User does not exist!");

			await this._userDbRepository.DeleteAsync(id);
			
			return new OkResult();
		}
	}
}
