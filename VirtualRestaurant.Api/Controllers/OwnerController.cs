using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualRestaurant.BusinessLogic.CQRS.Commands;

namespace VirtualRestaurant.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class OwnerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OwnerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("login")]
        //Add documentation string: "Use respond url to login via google"
        public IActionResult Login()
        {
            return Ok($"Use this url to sign in via google: https://{Request.Host.Value}/api/owner/google-login");
        }

        //[HttpGet]
        //[AllowAnonymous]
        //[Route("logout")]
        ////Add documentation string: "Use respond url to login via google"
        //public async Task<IActionResult> Logout()
        //{
        //    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    return Ok("Logouted");
        //}

        [HttpGet]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet]
        [AllowAnonymous]
        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return Ok("Successufly login!");
        }

        [HttpPost]
        [Authorize]
        [Route("create-restaurant")]
        public IActionResult CreateRestaurant()
        {



            var a = _mediator.Send(new CreateRestaurant.Command(new Domain.Models.Owner()));
            return Ok();
        }

    }
}
