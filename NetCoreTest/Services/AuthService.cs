using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetCoreTest.Data;
using NetCoreTest.Entities;
using NetCoreTest.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;
using BCryptor = BCrypt.Net.BCrypt;

namespace NetCoreTest.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;

        public AuthService(ApplicationDbContext context,
                           IOptions<AppSettings> appSettingOption)
        {
            _context = context;
            _appSettings = appSettingOption.Value;
        }

        public async Task<User> RegisterAsync(User user, string password)
        {
            // validation
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new BadRequestException("Password is required");
            }

            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Username.ToLower() == user.Username.ToLower()
                                                                          || x.Email.ToLower() == user.Email.ToLower());
            if (existingUser != null)
            {
                if (existingUser.Username.ToLower() == user.Username.ToLower())
                {
                    throw new BadRequestException($"Username {user.Username} is already taken");
                }

                if (existingUser.Email.ToLower() == user.Email.ToLower())
                {
                    throw new BadRequestException($"Email {user.Email} is already taken");
                }
            }

            user.PasswordHash = BCryptor.HashPassword(password);
            user.VerifyEmailToken = GenerateValidateToken(user.Id);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task VerifyAccountAsync(string token)
        {
            var account = await _context.Users.FirstOrDefaultAsync(u => u.VerifyEmailToken == token);
            if (account == null)
            {
                throw new BadRequestException("Invalid token");
            }
            account.IsVerified = true;
            await _context.SaveChangesAsync();
        }

        public string GenerateValidateToken(string accountId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.VerifyAccountSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", accountId) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}