using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using SA.Irrigation.Common.Models.Dto;
using SA.Irrigation.Common.Models.Requests;
using SA.Irrigation.Common.Services;
using SA.Irrigation.Db;
using SA.Irrigation.Db.Entities;
using System.Net;

namespace SA.Irrigation.Services.Implementation
{
    public class ScheduleService : IScheduleService
    {
        private readonly IrrigationDbContext _context;
        private readonly ILogger<ScheduleService> _logger;

        public ScheduleService(IrrigationDbContext context, ILogger<ScheduleService> logger)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));

            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> CreateScheduleAsync(Guid deviceId, CreateOrUpdateScheduleRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var parent = await _context.Devices.FindAsync(deviceId, cancellationToken);
                if (parent == null)
                {
                    return new NotFoundObjectResult($"There is no device with id {deviceId}");
                }
                Sensor? sensor = null;

                if (request.FinishBy == Common.Enums.FinishByType.ByValue)
                {
                    sensor = _context.Sensors.FirstOrDefault(r => r.Id == request.FinishDeviceId.Value);
                    if (sensor == null)
                    {
                        return new NotFoundObjectResult($"There is no sensor whith id {request.FinishDeviceId} ");
                    }
                }

                var toAdd = new Schedule();
                toAdd.FinishBy = request.FinishBy;
                toAdd.Name = request.Name;
                toAdd.Description = request.Description;
                toAdd.ParentDevice = parent;
                toAdd.StartCron = request.StartCron;
                toAdd.FinishDevice = sensor;
                toAdd.FinishValue = request.FinishValue;
                toAdd.FinishCron = request.FinishCron;
                toAdd.Name = request.Name;
                _context.Schedules.Add(toAdd);
                await _context.SaveChangesAsync(cancellationToken);
                return new OkObjectResult(new ScheduleDto
                {
                    Id = toAdd.Id,
                    FinishDeviceId = toAdd.FinishDevice == null ? null : toAdd.FinishDevice.Id,
                    FinishBy = toAdd.FinishBy,
                    Name = toAdd.Name,
                    Description = toAdd.Description,
                    StartCron = toAdd.StartCron,
                    FinishCron = toAdd.FinishCron,
                    FinishValue = toAdd.FinishValue
                });

                
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception thrown when create Schedule");
                return new ObjectResult(e.Message) { StatusCode = (int)HttpStatusCode.InternalServerError };
            }
        }

        public async Task<IActionResult> DeleteScheduleAsync(Guid scheduleId, CancellationToken cancellationToken)
        {
            try
            {
                var toDelete = await _context.Schedules.FindAsync(scheduleId, cancellationToken);
                if (toDelete == null)
                {
                    return new NotFoundObjectResult($"There is no schedule with id {scheduleId}");
                }

                _context.Schedules.Remove(toDelete);
                await _context.SaveChangesAsync(cancellationToken);
                return new OkResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception thrown when delete Schedule with id {id}", scheduleId);
                return new ObjectResult(e.Message) { StatusCode = (int)HttpStatusCode.InternalServerError };
            }
        }

        public async Task<IActionResult> ReadByIdAsync(Guid scheduleId, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _context.Schedules.Include(i=> i.FinishDevice).FirstOrDefaultAsync(f=> f.Id == scheduleId);
                if (res == null)
                {
                    return new NotFoundObjectResult($"There is no schedule with id {scheduleId}");
                }
                return new OkObjectResult(new ScheduleDto
                {
                    Id = res.Id,
                    Name = res.Name,
                    Description = res.Description,
                    FinishBy = res.FinishBy,
                    FinishCron = res.FinishCron,
                    FinishDeviceId = res.FinishDevice == null ? null : res.FinishDevice.Id,
                    FinishValue = res.FinishValue,
                    StartCron = res.StartCron
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception thrown when read Schedule with id {id}", scheduleId);
                return new ObjectResult(e.Message) { StatusCode = (int)HttpStatusCode.InternalServerError };
            }
        }

        public async Task<IActionResult> ReadByParentAsync(Guid parentId, CancellationToken cancellationToken)
        {
            try
            {
                if (!_context.Devices.Any(a=>  a.Id == parentId))
                    return new NotFoundObjectResult($"There is no device with id {parentId}");

                var res = await _context.Schedules.Include(i=> i.ParentDevice).Include(i=> i.FinishDevice)
                    .Where(w=> w.ParentDevice.Id == parentId)
                    .Select(s=> new ScheduleDto
                    {
                        Id=s.Id,
                        Name=s.Name,
                        Description=s.Description,
                        FinishBy = s.FinishBy,
                        FinishCron = s.FinishCron,
                        FinishValue=s.FinishValue,
                        StartCron = s.StartCron,
                        FinishDeviceId = s.FinishDevice == null ? null : s.FinishDevice.Id
                        
                    }).ToListAsync(cancellationToken);
                return new OkObjectResult(res);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception thrown when read Schedules for Parent with id {id}", parentId);
                return new ObjectResult(e.Message) { StatusCode = (int)HttpStatusCode.InternalServerError };
            }
        }

        public async Task<IActionResult> UpdateScheduleAsync(Guid scheduleId, CreateOrUpdateScheduleRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var toUpdate = await _context.Schedules.Include(i=> i.FinishDevice).FirstOrDefaultAsync(i=> i.Id == scheduleId, cancellationToken);
                if (toUpdate == null)
                    return new NotFoundObjectResult($"There is no schedule with id {scheduleId}");
                Sensor? sensor = null;

                if (request.FinishBy == Common.Enums.FinishByType.ByValue)
                {
                    sensor = _context.Sensors.FirstOrDefault(r => r.Id == request.FinishDeviceId.Value);
                    if (sensor == null)
                    {
                        return new NotFoundObjectResult($"There is no sensor whith id {request.FinishDeviceId} ");
                    }
                }

                toUpdate.StartCron = request.StartCron;
                toUpdate.FinishDevice = sensor;
                toUpdate.FinishValue = request.FinishValue;
                toUpdate.FinishBy = request.FinishBy;
                toUpdate.FinishCron = request.FinishCron;
                toUpdate.Description = request.Description;
                toUpdate.Name = request.Name;
                _context.Schedules.Update(toUpdate);
                await _context.SaveChangesAsync(cancellationToken);
                return new OkObjectResult(new ScheduleDto
                {
                    Id = toUpdate.Id,
                    Name = toUpdate.Name,
                    Description = toUpdate.Description,
                    FinishBy = toUpdate.FinishBy,
                    FinishCron = toUpdate.FinishCron,
                    FinishDeviceId = toUpdate.FinishDevice == null ? null : toUpdate.FinishDevice.Id,
                    FinishValue = toUpdate.FinishValue,
                    StartCron = toUpdate.StartCron
                });

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception thrown when read Schedule with id {id}", scheduleId);
                return new ObjectResult(e.Message) { StatusCode = (int)HttpStatusCode.InternalServerError };
            }
        }
    }
}
