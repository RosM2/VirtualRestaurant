using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualRestaurant.Api.DTO.RequestDto;
using VirtualRestaurant.BusinessLogic.CQRS.Commands;
using VirtualRestaurant.BusinessLogic.CQRS.Queries;
using VirtualRestaurant.Domain.Models;

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
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestarauntDto restaraunt)
        {
            var claims = HttpContext.User.Identities.First().Claims.ToList();

            var owner = (await _mediator.Send(new GetOwner.Query(claims.First(x => x.Type.Contains("emailaddress")).Value))).Value;

            if (owner == null)
            {
                owner = new Owner();
                owner.Email = claims.First(x => x.Type.Contains("emailaddress")).Value;
                owner.FirstName = claims.First(x => x.Type.Contains("givenname")).Value;
                owner.LastName = claims.First(x => x.Type.Contains("surname")).Value;
            }
            var result = await _mediator.Send(new CreateRestaurant.Command(new Restaurant() 
            { 
                Name = restaraunt.Name,
                TotalTablesCount = restaraunt.TotalTablesCount,
                FreeTablesCount = restaraunt.TotalTablesCount,
            }, owner));

            if (!result.Successful)
            {
                return BadRequest(result.Error);
            }
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("setup-restaurant")]
        public async Task<IActionResult> SetupRestaurant([FromBody] SetupRestaurantDto dto)
        {
            var claims = HttpContext.User.Identities.First().Claims.ToList();

            var owner = (await _mediator.Send(new GetOwner.Query(claims.First(x => x.Type.Contains("emailaddress")).Value))).Value;

            var isOwner = (await _mediator.Send(new CheckRestaurantOwner.Query(claims.First(x => x.Type.Contains("emailaddress")).Value, dto.RestaurantId))).Value;
            if (!isOwner)
            {
                return BadRequest("You have no permissions");
            }

            var updatedTables = new List<Table>();
            for (int i = 0; i < dto.Tables.Count; i++)
            {
                updatedTables.Add(new Table()
                {
                    IsBooked = dto.Tables[i].IsBooked,
                    NumberOfSits = dto.Tables[i].NumberOfSits,
                    Location = dto.Tables[i].Location
                });
            }
            await _mediator.Send(new SetupRestaurant.Command(updatedTables, dto.RestaurantId));
            return Ok();
        }
    }
}
