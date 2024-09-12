using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SA.Irrigation.Common.Enums;
using SA.Irrigation.Common.Models.Dto;
using SA.Irrigation.Common.Models.Requests;
using SA.Irrigation.Common.Services;
using System.Net;

namespace SA.Irrigation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DeviceDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return await _deviceService.ReadAllAsync(cancellationToken);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DeviceDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            return await _deviceService.ReadByAIdAsync(id, cancellationToken);
        }

        [HttpGet("bytype/{deviceType}")]
        [ProducesResponseType(typeof(IEnumerable<DeviceDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetByType([FromRoute] DeviceType deviceType, CancellationToken cancellationToken)
        {
            return await _deviceService.ReadByModelTypeAsync(deviceType, cancellationToken);
        }

        [HttpPost]
        [ProducesResponseType(typeof(DeviceDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateDevice([FromBody] CreateOrUpdateDeviceRequest request, CancellationToken cancellationToken)
        {
            return await _deviceService.CreateDeviceAsync(request, cancellationToken);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(DeviceDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CreateOrUpdateDeviceRequest request, CancellationToken cancellationToken)
        {
            return await _deviceService.UpdateDeviceAsync(id, request, cancellationToken);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            return await _deviceService.DeleteDeviceAsync(id, cancellationToken);
        }
    }
}
