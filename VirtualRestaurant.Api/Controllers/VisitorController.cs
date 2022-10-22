using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> GetRestaurants() 
        {
            var result = await _mediator.Send(new GetRestaurants.Query());
            return Ok(result.Value.Select(x => new GetRestaurantsDto()
            {
                Name = x.Name,
                FreeTablesCount = x.FreeTablesCount,
                TotalTablesCount = x.TotalTablesCount
            }).ToList());
        }
    }
}
