using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using astute.Models;
using Microsoft.Extensions.Configuration;

namespace astute.Extention
{
    public class Auth
    {
        private readonly IConfiguration _configuration;
        public Auth(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private static string key;
        //public static string Authentication(Employee_Master employee, UserModel userModel)
        //{
        //    if (!(employee.User_Name.Equals(userModel.UserName) || employee.Password.Equals(userModel.Password)))
        //    {
        //        return null;
        //    }

        //    // 1. Create Security Token Handler
        //    var tokenHandler = new JwtSecurityTokenHandler();

        //    // 2. Create Private Key to Encrypted
        //    var tokenKey = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);

        //    //3. Create JETdescriptor
        //    var tokenDescriptor = new SecurityTokenDescriptor()
        //    {
        //        Subject = new ClaimsIdentity(
        //            new Claim[]
        //            {
        //                new Claim(ClaimTypes.Name, userModel.UserName)
        //            }),
        //        Expires = DateTime.UtcNow.AddHours(1),
        //        SigningCredentials = new SigningCredentials(
        //            new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    //4. Create Token
        //    var token = tokenHandler.CreateToken(tokenDescriptor);

        //    // 5. Return Token from method
        //    return tokenHandler.WriteToken(token);
        //}
    }
}
