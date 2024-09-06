using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SA.Irrigation.Common.Enums;
using SA.Irrigation.Common.Models.Dto;
using SA.Irrigation.Common.Models.Requests;
using SA.Irrigation.Common.Services;
using SA.Irrigation.Db;
using SA.Irrigation.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SA.Irrigation.Services.Implementation
{
    public class DeviceService : IDeviceService
    {
        private readonly IrrigationDbContext _context;
        private readonly ILogger<DeviceService> _logger;

        public DeviceService(IrrigationDbContext context, ILogger<DeviceService> logger)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> CreateDeviceAsync(CreateOrUpdateDeviceRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(request, nameof(request));

                var model = await _context.Models.FindAsync(request.ModelId, cancellationToken);
                if (model == null)
                {
                    return new NotFoundObjectResult($"There is no model whith id:{request.ModelId}");
                }
                if (request.Address<0 || request.Address>=65534)
                {
                    return new BadRequestObjectResult($"Address must be btween 0 and 65533");
                }

                var toAdd = new Device
                {
                    Address = request.Address,
                    Description = request.Description,
                    Name = request.Name,
                    Model = model
                };
                _context.Devices.Add(toAdd);
                await _context.SaveChangesAsync();
                return new OkObjectResult(new DeviceDto 
                {
                    Id = toAdd.Id,
                    Address = toAdd.Address,
                    Description = toAdd.Description,
                    Name = toAdd.Name,
                    Model = new DeviceModelDto
                    {
                        Id = model.Id,
                        CloseCommand = model.CloseCommand,
                        GetDataCommand = model.GetDataCommand,
                        OpenCommand = model.OpenCommand,
                        Name = model.Name,
                        Description = model.Description,
                        Type = model.Type
                    }
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception thrown when adding a Device");
                return new ObjectResult(e.Message) { StatusCode = (int)HttpStatusCode.InternalServerError };
            }
        }

        public async Task<IActionResult> DeleteDeviceAsync(Guid deviceId, CancellationToken cancellationToken = default)
        {
            try
            {
                var toDelete = _context.Devices.Include(i => i.Schedules).Where(s=> s.Id == deviceId).FirstOrDefault();
                if (toDelete == null)
                {
                    return new NotFoundObjectResult($"device not found");
                }
                if (toDelete.Schedules!.Any())
                {
                    _context.Schedules.RemoveRange(toDelete.Schedules!.ToArray());
                }
                _context.Devices.Remove(toDelete);
                await _context.SaveChangesAsync();
                return new OkResult();

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception thrown when adding a Device");
                return new ObjectResult(e.Message) { StatusCode = (int)HttpStatusCode.InternalServerError };
            }
        }

        public async Task<IActionResult> ReadAllAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await  _context.Devices.Include(i=> i.Model)
                    .Select(s=> new DeviceDto { 
                        Id = s.Id, 
                        Address = s.Address,
                        Name = s.Name,
                        Description = s.Description,
                        Model = new DeviceModelDto
                        {
                            Id = s.Model.Id,
                            Name = s.Model.Name,
                            Description = s.Model.Description,
                            OpenCommand = s.Model.OpenCommand,
                            CloseCommand = s.Model.CloseCommand,
                            Type = s.Model.Type,
                            GetDataCommand = s.Model.GetDataCommand
                        }
                    }).ToListAsync(cancellationToken);

                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception thrown when get all Devices");
                return new ObjectResult(e.Message) { StatusCode = (int)HttpStatusCode.InternalServerError };
            }
        }

        public async Task<IActionResult> ReadByAIdAsync(Guid deviceId, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _context.Devices.Include(i => i.Model)
                    .Where(i=> i.Id == deviceId)
                    .Select(s => new DeviceDto
                    {
                        Id = s.Id,
                        Address = s.Address,
                        Name = s.Name,
                        Description = s.Description,
                        Model = new DeviceModelDto
                        {
                            Id = s.Model.Id,
                            Name = s.Model.Name,
                            Description = s.Model.Description,
                            OpenCommand = s.Model.OpenCommand,
                            CloseCommand = s.Model.CloseCommand,
                            Type = s.Model.Type,
                            GetDataCommand = s.Model.GetDataCommand
                        }
                    }).FirstOrDefaultAsync(cancellationToken);
                if (result == null)
                {
                    return new NotFoundObjectResult($"Device with id {deviceId} not found");
                }
                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception thrown when get Device with id: {deviceId}", deviceId);
                return new ObjectResult(e.Message) { StatusCode = (int)HttpStatusCode.InternalServerError };
                throw;
            }
        }

        public async Task<IActionResult> ReadByModelTypeAsync(DeviceType deviceType, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _context.Devices
                    .Include(i => i.Model)
                    .Where(d=> d.Model.Type == deviceType)
                    .Select(s => new DeviceDto
                    {
                        Id = s.Id,
                        Address = s.Address,
                        Name = s.Name,
                        Description = s.Description,
                        Model = new DeviceModelDto
                        {
                            Id = s.Model.Id,
                            Name = s.Model.Name,
                            Description = s.Model.Description,
                            OpenCommand = s.Model.OpenCommand,
                            CloseCommand = s.Model.CloseCommand,
                            Type = s.Model.Type,
                            GetDataCommand = s.Model.GetDataCommand
                        }
                    }).ToListAsync(cancellationToken);

                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception thrown when get Devices for type");
                return new ObjectResult(e.Message) { StatusCode = (int)HttpStatusCode.InternalServerError };
            }
        }

        public async Task<IActionResult> UpdateDeviceAsync(Guid deviceId, CreateOrUpdateDeviceRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                if (request.Address < 0 || request.Address >= 65534)
                {
                    return new BadRequestObjectResult($"Address must be between 0 and 65533");
                }
                var toUpdate = await _context.Devices
                    .Include(i=> i.Model)
                    .Where(w=> w.Id == deviceId)
                    .FirstOrDefaultAsync(cancellationToken);
                if (toUpdate == null)
                {
                    return new NotFoundObjectResult($"Device with id {deviceId} not found");
                }
                var updatedModel = await _context.Models.FindAsync(request.ModelId, cancellationToken);
                if (updatedModel == null) {
                    return new NotFoundObjectResult($"DeviceModel with id {request.ModelId} not found");
                }
                toUpdate.Address = request.Address;
                toUpdate.Name = request.Name;
                toUpdate.Description = request.Description;
                toUpdate.Model = updatedModel;
                await _context.SaveChangesAsync(cancellationToken);
                return new OkObjectResult(new DeviceDto
                {
                    Id = toUpdate.Id,
                    Address = toUpdate.Address,
                    Description = toUpdate.Description,
                    Name = toUpdate.Name,
                    Model = new DeviceModelDto
                    {
                        Id = toUpdate.Model.Id,
                        CloseCommand = toUpdate.Model.CloseCommand,
                        GetDataCommand = toUpdate.Model.GetDataCommand,
                        OpenCommand = toUpdate.Model.OpenCommand,
                        Name = toUpdate.Model.Name,
                        Description = toUpdate.Model.Description,
                        Type = toUpdate.Model.Type
                    }
                });

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception thrown when update Devices with id: {deviceId}", deviceId);
                return new ObjectResult(e.Message) { StatusCode = (int)HttpStatusCode.InternalServerError };
            }
        }
    }
}
