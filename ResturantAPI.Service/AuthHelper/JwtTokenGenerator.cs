using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ResturantAPI.Domain.Entities;

namespace ResturantAPI.Services.AuthHelper
{
    public static class JwtTokenExtensions
    {
        public static IConfiguration config;
        public static async Task<string> GenerateTokenAsync(
            this UserManager<ApplicationUser> userManager,
            ApplicationUser user
           )
        {
            var roles = await userManager.GetRolesAsync(user);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
        };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(config.GetValue<int>("Authentication:TokenExpirationInMinutes")),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    
    
         public static async Task<int> GenerateOTPAsync(this UserManager<ApplicationUser> userManager,string userId)
         {
            ApplicationUser? user = await userManager.FindByIdAsync(userId);
            if(user == null)
            {
                throw new Exception("User not found");
            }
            //GeneratOtp
            var otp = new Random().Next(100000, 999999);
            user.OTP = otp;
            user.OTPExpiration = DateTime.UtcNow.AddMinutes(5); // Set expiration time for OTP
            await userManager.UpdateAsync(user);

            // Send OTP to user's email or phone number
            // await SendOtpToUser(user.Email, otp);
            return otp;
        }
   
        
        public static async Task<bool> ConfirmEmailOtp(this UserManager<ApplicationUser> userManager, string userId, int otp)
        {
            ApplicationUser? user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if (user.OTP != otp || user.OTPExpiration < DateTime.UtcNow)
            {
                return false;
            }
 
            user.OTP = 0;
            user.OTPExpiration = DateTime.MinValue;
            user.EmailConfirmed = true; 
            IdentityResult result = await userManager.UpdateAsync(user);
             if (!result.Succeeded)
            {
                throw new Exception("Failed to Confirm Email");
            }

            return true; 
        }


        public static async Task<bool> RevokeToken(this UserManager<ApplicationUser> userManager, string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var userId = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
            ApplicationUser user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            
            await userManager.UpdateAsync(user);
            return true;
        }

      public static async Task<string?> RefreshToken(this UserManager<ApplicationUser> userManager, string token, string refreshToken)
      {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                var userId = jwtToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
    
                ApplicationUser? user = await userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return null;
                }

                if (user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime < DateTime.UtcNow)
                {
                    return null;
                }

                // Generate new access token
                var newToken = await userManager.GenerateTokenAsync(user);

               // Best practice: Update refresh token too
                user.RefreshToken = await userManager.GenerateRefreshTokenAsync(user); // Create a new refresh token
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                await userManager.UpdateAsync(user);

            return newToken;
      }

         public static async Task<string> GenerateRefreshTokenAsync(this UserManager<ApplicationUser> userManager,ApplicationUser user) {
            // GenerateRefreshToken
           
            string refreshToken =  Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);  
            IdentityResult result =    await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception("Failed to generate refresh token");
            }
            return refreshToken;

        }
    }

}

