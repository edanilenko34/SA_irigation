using Microsoft.AspNetCore.Mvc;
using SA.Irrigation.Common.Models.Dto;
using SA.Irrigation.Common.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SA.Irrigation.Common.Services
{
    public interface IDeviceModelService
    {
        Task<IActionResult> CreateDeviceModelAsync(CreateDeviceModelRequest createDeviceModelRequest, CancellationToken cancellationToken = default);
        Task<IActionResult> ReadAllAsync(CancellationToken cancellationToken = default);
        Task<IActionResult> UpdateDeviceModelAsync(Guid deviceModelId, CreateDeviceModelRequest request, CancellationToken cancellationToken = default);
        Task<IActionResult> DeleteDeviceModelAsync(Guid deviceModelId, CancellationToken cancellationToken = default);
        Task<IActionResult> ReadByIdAsync(Guid deviceModelId, CancellationToken cancellationToken = default);

    }
}
