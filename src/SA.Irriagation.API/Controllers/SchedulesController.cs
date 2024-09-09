using Microsoft.AspNetCore.Mvc;
using SA.Irrigation.Common.Models.Dto;
using SA.Irrigation.Common.Models.Requests;
using SA.Irrigation.Common.Services;
using System.Net;

namespace SA.Irrigation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public SchedulesController(IScheduleService scheduleService)
        {
            ArgumentNullException.ThrowIfNull(scheduleService, nameof(scheduleService));   
            _scheduleService = scheduleService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ScheduleDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CreateSchedule([FromQuery] Guid deviceId, [FromBody] CreateOrUpdateScheduleRequest request,
            CancellationToken cancellationToken)
        {
            return await _scheduleService.CreateScheduleAsync(deviceId, request, cancellationToken);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ScheduleDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateSchedule([FromRoute] Guid id, [FromBody] CreateOrUpdateScheduleRequest request, CancellationToken cancellationToken)
        {
            return await _scheduleService.UpdateScheduleAsync(id, request, cancellationToken);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteSchedule([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            return await _scheduleService.DeleteScheduleAsync(id, cancellationToken);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ScheduleDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetScheduleById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            return await _scheduleService.ReadByIdAsync(id, cancellationToken);
        }

        [HttpGet("parent/{parentid}")]
        [ProducesResponseType(typeof(IEnumerable<ScheduleDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetSchedulesByParent([FromRoute] Guid parentId, CancellationToken cancellationToken)
        {
            return await _scheduleService.ReadByParentAsync(parentId, cancellationToken);
        }
    }
}
