using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProductManagementAPI.AccountsService;
using ProductManagementAPI.DTOs;
using ProductManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementAPI.AccountsService
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppSettings _appSettings;
        

        public AccountService(
            UserManager<ApplicationUser> userManager,
            IOptions<AppSettings> appSettings
            )
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
            
        }

        public async Task<ApplicationUser> CreateAsync(RegisterModelDto model, string role = "admin")
        {
            if (model is null) throw new ArgumentNullException(message: "Invalid Details Provided", null);

            ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                await _userManager.AddToRoleAsync(user, role);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(message: "Account creation not succeeded");
                }
            }

            return user;
        }

        //public async Task<string> LoginAsync(LoginModelDto model)
        //{
        //    try
        //    {
        //        using (var userManager = new UserManager<ApplicationUser>())
        //        {
        //            var user = await userManager.FindByEmailAsync(model.Email);
        //            var role = await userManager.GetRolesAsync(user);
        //            var result = await userManager.CheckPasswordAsync(user, model.Password);

        //            if (result)
        //            {
        //                //if user is found
        //                var tokenHandler = new JwtSecurityTokenHandler();
        //                var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
        //                var tokenDescriptor = new SecurityTokenDescriptor
        //                {
        //                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
        //                    new Claim(ClaimTypes.Email, model.Email),
        //                    new Claim(ClaimTypes.Role, role[1]),
        //                    new Claim(ClaimTypes.Version, "V1.0"),
        //                }),
        //                    Expires = DateTime.UtcNow.AddDays(7),
        //                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //                };
        //                var token = tokenHandler.CreateToken(tokenDescriptor);

        //                return token.ToString();
        //            }
        //            else
        //            {
        //                return null;
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }
        //}
    }
}
