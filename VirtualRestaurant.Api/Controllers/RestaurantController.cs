using MediatR;
using Microsoft.AspNetCore.Mvc;
using VirtualRestaurant.Api.DTO.RequestDto;
using VirtualRestaurant.Api.DTO.ResponseDto;
using VirtualRestaurant.BusinessLogic.CQRS.Commands;
using VirtualRestaurant.BusinessLogic.CQRS.Queries;
using VirtualRestaurant.Domain.Models;

namespace VirtualRestaurant.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class RestaurantController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RestaurantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Use this endpoint to get all available restaurants
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllRestaurants()
        {
            var result = await _mediator.Send(new GetRestaurants.Query());
            if (result.Value.Count == 0)
            {
                return Ok("No restaurants");
            }
            return Ok(result.Value.Select(x => new GetRestaurantsDto()
            {
                Id = x.Id,
                Name = x.Name,
                FreeTablesCount = x.FreeTablesCount
            }).ToList());
        }

        /// <summary>
        /// Use this endpoint to get restaurant by id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurantId([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetRestaurantById.Query(id));
            if (result.Value == null)
            {
                return BadRequest("Wrong restaurant id");
            }
            return Ok(new GetRestaurantDto() 
            {
                Id = result.Value.Id,
                Name = result.Value.Name,
                FreeTablesCount = result.Value.FreeTablesCount,
                TotalTablesCount = result.Value.TotalTablesCount
            });
        }

        /// <summary>
        /// Use this endpoint to reserve restaurant(enter correct restaurantId, email which contains "@", reservationDate must be future date, visitorsCount must be > 0)
        /// </summary>
        [HttpPost("reserve")]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDto dto)
        {
            if (dto.VisitorsCount <= 0 || !dto.VisitorEmail.Contains("@") || dto.ReservationDate < DateTime.UtcNow)
            {
                return BadRequest("Wrong entred info");
            }
            var result = await _mediator.Send(new CreateReservation.Command(new Reservation()
            {
                ReservationDate = dto.ReservationDate,
                VisitorEmail = dto.VisitorEmail,
                RestaurantId = dto.RestaurantId,
                VisitorsCount = dto.VisitorsCount
            }));
            if (!result.Successful)
            {
                return BadRequest(result.Error);
            }
            return Ok();
        }
        /// <summary>
        /// Use this endpoint to create restaurant(You have to be authorized by google login), enter all required info
        /// </summary>
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestarauntDto restaraunt)
        {
            var claims = HttpContext.User.Identities.First().Claims.ToList();
            if (!claims.Any(x => x.Type.Contains("emailaddress")) || (await _mediator.Send(new GetOwner.Query(claims.First(x => x.Type.Contains("emailaddress")).Value))).Value == null)
            {
                return Unauthorized();
            }
            var owner = (await _mediator.Send(new GetOwner.Query(claims.First(x => x.Type.Contains("emailaddress")).Value))).Value;

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

        /// <summary>
        /// Use this endpoint to setup restaurant(You have to be authorized by google login),
        /// enter all required info, count of tables, which you post from body must match tables count,
        /// that you entered in createRestaurant request
        /// </summary>
        [HttpPost]
        [Route("setup")]
        public async Task<IActionResult> SetupRestaurant([FromBody] SetupRestaurantDto dto)
        {
            var claims = HttpContext.User.Identities.First().Claims.ToList();
            if (!claims.Any(x => x.Type.Contains("emailaddress")) || (await _mediator.Send(new GetOwner.Query(claims.First(x => x.Type.Contains("emailaddress")).Value))).Value == null)
            {
                return Unauthorized();
            }
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
                    NumberOfSits = dto.Tables[i].NumberOfSits,
                    Location = dto.Tables[i].Location
                });
            }
            var result = await _mediator.Send(new SetupRestaurant.Command(updatedTables, dto.RestaurantId));
            if (!result.Successful)
            {
                return BadRequest(result.Error);
            }
            return Ok();
        }
    }
}
