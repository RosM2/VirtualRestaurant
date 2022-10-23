using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VirtualRestaurant.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthorizationController : ControllerBase
    {
        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return Ok($"Use this url to sign in via google: https://{Request.Host.Value}/api/authorization/google-login");
        }

        [HttpGet] 
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("google-response")]
        public async Task<IActionResult> GoogleResponse()
            {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok("Successfully loged in");
        }
    }
}
