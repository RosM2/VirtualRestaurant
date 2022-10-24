using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualRestaurant.BusinessLogic.CQRS.Queries;
using VirtualRestaurant.BusinessLogic.Handlers.Commands;
using VirtualRestaurant.Domain.Models;

namespace VirtualRestaurant.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthorizationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthorizationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// This endpoint returns url, which is used for google authentication and authorization
        /// (Before testing api, enter a command in Nugget Package Manager Console: Update-Database, and be sure, that MSSQL Server installed)
        /// </summary>
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
            var claims = HttpContext.User.Identities.First().Claims.ToList();

            var owner = (await _mediator.Send(new GetOwner.Query(claims.First(x => x.Type.Contains("emailaddress")).Value))).Value;

            if (owner == null)
            {
                owner = new Owner()
                {
                    Email = claims.First(x => x.Type.Contains("emailaddress")).Value,
                    FirstName = claims.First(x => x.Type.Contains("givenname")).Value,
                    LastName = claims.First(x => x.Type.Contains("surname")).Value
                };
                await _mediator.Send(new CreateOwner.Command(owner));
            }             
            return Ok("Successfully loged in");
        }
    }
}
