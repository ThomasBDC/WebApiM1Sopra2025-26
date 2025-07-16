﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApiM1Sopra2025_26.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            var username = GetCurrentUserName();
            return Ok(username);
        }

        private string GetCurrentUserName()
        {
            var identity = HttpContext.User.Claims;

            if (identity != null)
            {
                var claims = identity.ToList();

                var userName = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                return userName;

            }

            return null;
        }
    }
}
