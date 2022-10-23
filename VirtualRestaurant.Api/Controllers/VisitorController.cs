using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualRestaurant.Api.DTO.RequestDto;
using VirtualRestaurant.Api.DTO.ResponseDto;
using VirtualRestaurant.BusinessLogic.CQRS.Queries;

namespace VirtualRestaurant.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class VisitorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VisitorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllRestaurants() 
        {
            var result = await _mediator.Send(new GetRestaurants.Query());
            return Ok(result.Value.Select(x => new GetRestaurantDto()
            {
                Id = x.Id,
                Name = x.Name,
                FreeTablesCount = x.FreeTablesCount,
                TotalTablesCount = x.TotalTablesCount
            }).ToList());
        }

        [HttpGet("get-{id}")]
        public async Task<IActionResult> GetRestaurantId([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetRestaurantById.Query(id));
            return Ok(new GetRestaurantDto() 
            {
                Id = result.Value.Id,
                Name = result.Value.Name,
                FreeTablesCount = result.Value.FreeTablesCount,
                TotalTablesCount = result.Value.TotalTablesCount
            });
        }

        [HttpPost("reserve")]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDto dto)
        {
            await _mediator.Send(new )
            return Ok();
        }
    }
}
