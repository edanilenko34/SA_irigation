using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SA.Irrigation.Common.Models.Dto;
using SA.Irrigation.Common.Models.Requests;
using SA.Irrigation.Common.Services;
using SA.Irrigation.Db;
using SA.Irrigation.Db.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SA.Irrigation.Services.Implementation
{
    public class DeviceModelService : IDeviceModelService
    {
        private readonly IrrigationDbContext _context;
        private readonly ILogger<DeviceModelService> _logger;

        public DeviceModelService(IrrigationDbContext context, ILogger<DeviceModelService> logger)
        {

            ArgumentNullException.ThrowIfNull(context, nameof(context));
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> CreateDeviceModelAsync(CreateDeviceModelRequest createDeviceModelRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var deviceModel = new DeviceModel
                {
                    CloseCommand = createDeviceModelRequest.CloseCommand,
                    Description = createDeviceModelRequest.Description,
                    OpenCommand = createDeviceModelRequest.OpenCommand,
                    GetDataCommand = createDeviceModelRequest.GetDataCommand,
                    Type = createDeviceModelRequest.Type,
                    Name = createDeviceModelRequest.Name
                };
                await _context.Models.AddAsync(deviceModel);
                await _context.SaveChangesAsync();
                return new OkObjectResult(new DeviceModelDto
                {
                    Id = deviceModel.Id,
                    Name = deviceModel.Name,
                    Description = deviceModel.Description,
                    OpenCommand = deviceModel.OpenCommand,
                    GetDataCommand = deviceModel.GetDataCommand,
                    Type = deviceModel.Type,
                    CloseCommand = deviceModel.CloseCommand
                });

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error when device model adding");
                return new ObjectResult(e.Message) { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> DeleteDeviceModelAsync(Guid deviceModelId, CancellationToken cancellationToken = default)
        {
            try
            {
                var toDelete = await _context.Models.FindAsync(deviceModelId, cancellationToken);
                if ( toDelete == null)
                {
                    return new NotFoundResult();
                }

                _context.Models.Remove(toDelete);
                await _context.SaveChangesAsync(cancellationToken);
                return new OkResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error when device model deleting");
                return new ObjectResult(e.Message) { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> ReadAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return new OkObjectResult(await _context.Models.Select(s => new DeviceModelDto()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    OpenCommand = s.OpenCommand,
                    CloseCommand = s.CloseCommand,
                    GetDataCommand  = s.GetDataCommand, 
                    Type = s.Type
                }).ToListAsync(cancellationToken));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error when device models reading");
                return new ObjectResult(e.Message) { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> ReadByIdAsync(Guid deviceModelId, CancellationToken cancellationToken = default)
        {
            try
            {
                var toReturn = await _context.Models.FindAsync(deviceModelId, cancellationToken);
                if (toReturn == null)
                    return new NotFoundResult();
                return new OkObjectResult(new DeviceModelDto
                {
                    Id = toReturn.Id,
                    Name = toReturn.Name,
                    Description = toReturn.Description,
                    OpenCommand= toReturn.OpenCommand,
                    GetDataCommand = toReturn.GetDataCommand,
                    Type = toReturn.Type,
                    CloseCommand = toReturn.CloseCommand
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error when device models reading");
                return new ObjectResult(e.Message) { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> UpdateDeviceModelAsync(Guid deviceModelId, CreateDeviceModelRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var toUpdate = await _context.Models.FindAsync(deviceModelId, cancellationToken);
                if (toUpdate == null)
                {
                    return new NotFoundResult();
                }

                toUpdate.Type = request.Type;
                toUpdate.Name = request.Name;
                toUpdate.Description = request.Description;
                toUpdate.GetDataCommand = request.GetDataCommand;
                toUpdate.Type = request.Type;
                toUpdate.OpenCommand = request.OpenCommand;
                toUpdate.CloseCommand = request.CloseCommand;

                _context.Models.Update(toUpdate);
                await _context.SaveChangesAsync(cancellationToken);

                return new OkObjectResult(new DeviceModelDto
                {
                    Id = toUpdate.Id,
                    Name = toUpdate.Name,
                    Description = toUpdate.Description,
                    OpenCommand = toUpdate.OpenCommand,
                    GetDataCommand = toUpdate.GetDataCommand,
                    Type = toUpdate.Type,
                    CloseCommand = toUpdate.CloseCommand
                });

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error when device models reading");
                return new ObjectResult(e.Message) { StatusCode = 500 };
            }
        }
    }
}
