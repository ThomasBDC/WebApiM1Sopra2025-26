﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiM1Sopra2025_26.Models;

namespace WebApiM1Sopra2025_26.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Login(UserLogin userLogin)
        {
            var isOk = Authenticate(userLogin);

            if (isOk)
            {
                //générer jeton JWT
                string token = Generate(userLogin);
                return Ok(token);
            }
            else
            {
                return Unauthorized("Bad credentials");
            }
        }

        private string Generate(UserLogin userLogin)
        {
            var secret = _configuration["Jwt:Key"];

            var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(security, SecurityAlgorithms.HmacSha256);

            //Choisir les informations à mettre dans le token (claims)
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userLogin.UserName),
                new Claim("Coucou", "value")
            };

            //Générer le token 
            var token = new JwtSecurityToken(
                      claims: claims,
                      expires: DateTime.Now.AddMinutes(15),
                      signingCredentials: credentials
                );

            var tokenString =  new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;
        }

        private bool Authenticate(UserLogin userLogin)
        {
            var allUsers = UserConstants.Users;

            var userExist = from user in allUsers
                            where user.Username.ToLower() == userLogin.UserName.ToLower()
                                && user.Password == userLogin.Password
                            select user;

            if (userExist.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
