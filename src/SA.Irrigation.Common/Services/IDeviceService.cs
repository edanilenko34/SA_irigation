using Microsoft.AspNetCore.Mvc;
using SA.Irrigation.Common.Enums;
using SA.Irrigation.Common.Models.Requests;

namespace SA.Irrigation.Common.Services
{
    public interface IDeviceService
    {
        Task<IActionResult> CreateDeviceAsync(CreateOrUpdateDeviceRequest request, CancellationToken cancellationToken);
        Task<IActionResult> UpdateDeviceAsync(Guid deviceId, CreateOrUpdateDeviceRequest request, CancellationToken cancellationToken);
        Task<IActionResult> DeleteDeviceAsync(Guid deviceId, CancellationToken cancellationToken);
        Task<IActionResult> ReadAllAsync(CancellationToken cancellationToken);
        Task<IActionResult> ReadByAIdAsync(Guid deviceId, CancellationToken cancellationToken);
        Task<IActionResult> ReadByModelTypeAsync(DeviceType deviceType, CancellationToken cancellationToken);

    }
}
