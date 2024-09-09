using Microsoft.AspNetCore.Mvc;
using SA.Irrigation.Common.Models.Requests;

namespace SA.Irrigation.Common.Services
{
    public interface IScheduleService
    {
        Task<IActionResult> CreateScheduleAsync(Guid deviceId, CreateOrUpdateScheduleRequest request, CancellationToken cancellationToken);
        Task<IActionResult> UpdateScheduleAsync(Guid scheduleId, CreateOrUpdateScheduleRequest request, CancellationToken cancellationToken);
        Task<IActionResult> DeleteScheduleAsync(Guid scheduleId, CancellationToken cancellationToken);
        Task<IActionResult> ReadByIdAsync(Guid scheduleId, CancellationToken cancellationToken);
        Task<IActionResult> ReadByParentAsync(Guid parentId, CancellationToken cancellationToken);
    }
}
