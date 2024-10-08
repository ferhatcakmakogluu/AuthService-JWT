﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MiniApp1.API.Controllers
{
    [Authorize(Policy = "AgePolicy")]
    [Authorize(Roles ="admin", Policy = "ZonguldakPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStock()
        {
            var userName = HttpContext.User.Identity.Name;

            //username degilde id almak istesek
            //service kısmında token olusturuken, id yi ClaimTypes.NameIdentifier 'e yazdık
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            var userEmail = User.Claims.FirstOrDefault(x=>x.Type == ClaimTypes.Email);

            return Ok($"Stock -> UserName: {userName} - UserId: {userIdClaim.Value} - UserEmail: {userEmail.Value}");

        }
    }
}
