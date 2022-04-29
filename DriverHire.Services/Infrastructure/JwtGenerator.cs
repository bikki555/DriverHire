using DriverHire.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Services.Infrastructure
{
    public class JwtGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly DriverHireContext _context;

        public JwtGenerator(IConfiguration configuration,
                            DriverHireContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public async Task<(string token,string role)> GenerateJwtTokenAsync(IdentityUser identityUser)
        {
            var roleName = (await (from user in _context.Users
                                  join userRole in _context.UserRoles
                                  on user.Id equals userRole.UserId
                                  join role in _context.Roles
                                  on userRole.RoleId equals role.Id
                                  where
                                  user.Id == identityUser.Id
                                  select role.Name).ToListAsync()).FirstOrDefault();

            var applicationUser = await _context.ApplicationUser.FirstOrDefaultAsync(x => x.UserId == identityUser.Id);
            var authClaims = new List<Claim>
                {
                    new Claim("ApplicationUserId", $"{applicationUser?.Id??0}"),
                    new Claim("IsCustomer",$"{applicationUser?.IsCustomer}"),
                    new Claim(ClaimTypes.NameIdentifier, identityUser.Id),
                    new Claim("LoginName", identityUser.UserName),
                    new Claim("UserRole",roleName??""),
                };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                claims: authClaims,
                expires: DateTime.Now.AddMinutes(int.Parse(_configuration["DriverHireSettings:Token.ExpiryTimeInMinutes"])),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            var issuedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return (issuedToken,roleName);
        }
    }
}
