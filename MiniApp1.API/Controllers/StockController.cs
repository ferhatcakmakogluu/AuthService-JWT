using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MiniApp1.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        public IActionResult GetStock()
        {
            var userName = HttpContext.User.Identity.Name;

            //username degilde id almak istesek
            //service kısmında token olusturuken, id yi ClaimTypes.NameIdentifier 'e yazdık
            var userId = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

            var userEmail = User.Claims.FirstOrDefault(x=>x.Type == JwtRegisteredClaimNames.Email);

            return Ok($"Stock -> UserName: {userName} - UserId: {userId} - UserEmail: {userEmail}");

        }
    }
}
