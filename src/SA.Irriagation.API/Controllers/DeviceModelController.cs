using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SA.Irrigation.Common.Models.Dto;
using SA.Irrigation.Common.Models.Requests;
using SA.Irrigation.Common.Services;
using System.Net;

namespace SA.Irrigation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceModelController : ControllerBase
    {
        private readonly IDeviceModelService _deviceModelService;

        public DeviceModelController(IDeviceModelService deviceModelService)
        {
            ArgumentNullException.ThrowIfNull(deviceModelService, nameof(deviceModelService));
            _deviceModelService = deviceModelService;
        }

        /// <summary>
        /// Add new device model 
        /// </summary>
        [HttpPost()]
        [ProducesResponseType(typeof(DeviceModelDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> CreateDeviceModel(CreateDeviceModelRequest request, CancellationToken cancellationToken)
        {
            return await _deviceModelService.CreateDeviceModelAsync(request, cancellationToken);
        }

        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<DeviceModelDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return await _deviceModelService.ReadAllAsync(cancellationToken);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            return await _deviceModelService.DeleteDeviceModelAsync(id, cancellationToken);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(DeviceModelDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CreateDeviceModelRequest request, CancellationToken cancellationToken)
        {
            return await _deviceModelService.UpdateDeviceModelAsync(id, request, cancellationToken);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(DeviceModelDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            return await _deviceModelService.ReadByIdAsync(id, cancellationToken);
        }
    }
}
