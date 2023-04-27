using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrdersApp.Server.Servises;
using OrdersApp.Shared.DTO;
using OrdersApp.Shared.Models;

namespace OrdersApp.Server.Controllers
{
    [Route("/api/orders")]
    [Authorize(Roles = "Administrator")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService service;

        public OrdersController(IOrderService service)
        {
            this.service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] OrderRequest request)
        {
            return new OkObjectResult(await service.Create(request));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReadById(Guid id)
        {
            return new OkObjectResult(await service.ReadById(id));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Find([FromQuery] FilterRequest request)
        {
            return new OkObjectResult(await service.Find(request));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateById(Guid id, [FromBody] UpdateRequest request)
        {
            await service.UpdateById(id, request);
            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            await service.DeleteById(id);
            return Ok();
        }

        [HttpPut("status/{id}/{newStasus}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SetStatus(Guid id, Status newStasus)
        {
            await service.SetStatus(id, newStasus);
            return Ok();
        }

        [HttpPost("line/{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddLine(Guid orderId, [FromBody] LineRequest request)
        {
            await service.AddLine(orderId, request);
            return Ok();
        }

        [HttpDelete("line/{lineId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RemoveLine(Guid lineId)
        {
            await service.RemoveLine(lineId);
            return Ok();
        }

        [HttpGet("count")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCount([FromQuery] FilterRequest request)
        {
            return new OkObjectResult(await service.GetCount(request));
        }
    }
}
