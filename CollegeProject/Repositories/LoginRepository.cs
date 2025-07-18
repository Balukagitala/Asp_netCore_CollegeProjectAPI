using AutoMapper.Configuration.Annotations;
using CollegeProject.DbContexts;
using CollegeProject.Models;
using CollegeProject.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CollegeProject.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly CollegeDbContext _context;
        private readonly IConfiguration _configuration;

        public LoginRepository(CollegeDbContext context,IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<string> LoginAsync(LoginDetailsDto loginDetails)
        {
            if (loginDetails != null)
            {
                var result = _context.Registration.FirstOrDefault(x =>
                            x.UserName == loginDetails.UserName && x.Password == loginDetails.Password);
               
                if(result == null)
                {
                    return null;
                }
                else
                {
                    var existingUserActivity = await _context.UserActivity
                          .Where(x => x.UserId == result.UserId && x.Status == true)
                          .ToListAsync();
                    if (existingUserActivity.Any())
                    {
                        foreach (var activity in existingUserActivity)
                        {
                            activity.Status = false;
                        }
                    }
                    _context.UpdateRange(existingUserActivity);
                    await _context.SaveChangesAsync();
                    var token = await CreateToken(result);
                    if (token != null) {
                        var userActivity = new UserActivity
                        {
                            UserId = result.UserId,
                            RoleId = result.RoleId,
                            Token = token,
                            ExpiryDate = DateTime.Now.AddMinutes(15),
                            CreatedDate = DateTime.Now,
                            Status = true

                        };
                      

                        await _context.UserActivity.AddAsync(userActivity);
                        await _context.SaveChangesAsync();
                    }
                    return ("Login Successfully Completed" +token.ToString());
                }
            }
            return "Please Contact Administrator";
        }
        public async Task<String> CreateToken(Registration registration)
        {
            string role = _context.Roles
                .Where(x=>x.RoleId==registration.RoleId)
                .Select(r=>r.RoleName)
                .FirstOrDefault();
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Email, registration.Email));
            claims.Add(new Claim(ClaimTypes.Role,role));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
