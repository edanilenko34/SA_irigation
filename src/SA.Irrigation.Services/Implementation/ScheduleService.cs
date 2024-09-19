using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Quartz;
using SA.Irrigation.Common.Models.Dto;
using SA.Irrigation.Common.Models.QuartzJobs;
using SA.Irrigation.Common.Models.Requests;
using SA.Irrigation.Common.QuartzJobs;
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
        private readonly ISchedulerManager _schedulerManager;

        public ScheduleService(IrrigationDbContext context, ILogger<ScheduleService> logger, ISchedulerManager schedulerManager)
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));
            ArgumentNullException.ThrowIfNull(logger, nameof(logger));
            ArgumentNullException.ThrowIfNull(schedulerManager, nameof(schedulerManager));

            _context = context;
            _logger = logger;
            _schedulerManager = schedulerManager;
        }

        public async Task<IActionResult> CreateScheduleAsync(Guid deviceId, CreateOrUpdateScheduleRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var parent = await _context.Devices.Include(d => d.Model).FirstOrDefaultAsync(p => p.Id == deviceId, cancellationToken);
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
                toAdd.StartDate = request.StartDate;
                toAdd.FinishDate = request.FinishDate;
                toAdd.IsDisabled = request.IsDisabled;
                _context.Schedules.Add(toAdd);
                await _context.SaveChangesAsync(cancellationToken);

                if (!toAdd.IsDisabled)
                {
                    if (toAdd.FinishBy == Common.Enums.FinishByType.ByTime)
                    {
                        var keyStart = $"send_{toAdd.Id}_open";
                        var keyFinish = $"send_{toAdd.Id}_close";
                        var parameters = new JobParameters(keyStart, toAdd.StartCron, toAdd.StartDate, toAdd.FinishDate, new SendJobData { Address = toAdd.ParentDevice.Address, Message = parent.Model.OpenCommand! }, null);
                        await _schedulerManager.ScheduleJob<SendCommandJob, JobParameters>(parameters);
                        parameters = new JobParameters(keyFinish, toAdd.FinishCron!, toAdd.StartDate, toAdd .FinishDate, new SendJobData { Address = toAdd.ParentDevice.Address, Message = parent.Model.CloseCommand! }, null);
                        await _schedulerManager.ScheduleJob<SendCommandJob, JobParameters>(parameters);
                    }
                }
                return new OkObjectResult(new ScheduleDto
                {
                    Id = toAdd.Id,
                    FinishDeviceId = toAdd.FinishDevice == null ? null : toAdd.FinishDevice.Id,
                    FinishBy = toAdd.FinishBy,
                    Name = toAdd.Name,
                    Description = toAdd.Description,
                    StartCron = toAdd.StartCron,
                    FinishCron = toAdd.FinishCron,
                    FinishValue = toAdd.FinishValue,
                    StartDate = toAdd.StartDate,
                    FinishDate = toAdd.FinishDate,
                    IsDisabled = toAdd.IsDisabled
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
                var keyStart = $"send_{toDelete.Id}_open";
                var keyFinish = $"send_{toDelete.Id}_close";
                _context.Schedules.Remove(toDelete);
                await _context.SaveChangesAsync(cancellationToken);
                await _schedulerManager.UnscheduleJob(keyStart);
                await _schedulerManager.UnscheduleJob(keyFinish);
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
                    StartCron = res.StartCron,
                    FinishDate = res.FinishDate,
                    StartDate = res.StartDate,
                    IsDisabled = res.IsDisabled
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
                        FinishDeviceId = s.FinishDevice == null ? null : s.FinishDevice.Id,
                        FinishDate = s.FinishDate,
                        StartDate = s.StartDate,
                        IsDisabled = s.IsDisabled
                        
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
                var toUpdate = await _context.Schedules.Include(i=> i.FinishDevice).Include(p=>p.ParentDevice).FirstOrDefaultAsync(i=> i.Id == scheduleId, cancellationToken);
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

                var needToUnschedule = toUpdate.FinishBy == Common.Enums.FinishByType.ByTime && request.FinishBy == Common.Enums.FinishByType.ByValue;


                toUpdate.StartCron = request.StartCron;
                toUpdate.FinishDevice = sensor;
                toUpdate.FinishValue = request.FinishValue;
                toUpdate.FinishBy = request.FinishBy;
                toUpdate.FinishCron = request.FinishCron;
                toUpdate.Description = request.Description;
                toUpdate.Name = request.Name;
                toUpdate.FinishDate = request.FinishDate;
                toUpdate.StartDate = request.StartDate;
                toUpdate.IsDisabled = request.IsDisabled;
                _context.Schedules.Update(toUpdate);
                await _context.SaveChangesAsync(cancellationToken);

                var parent = await _context.Devices.Include(d => d.Model).FirstAsync(p => p.Id == toUpdate.ParentDevice.Id, cancellationToken);
                _context.Entry(parent).Reference(f => f.Model).Load();

                var keyStart = $"send_{toUpdate.Id}_open";
                var keyFinish = $"send_{toUpdate.Id}_close";

                if (!toUpdate.IsDisabled && !needToUnschedule)
                {
                    if (toUpdate.FinishBy == Common.Enums.FinishByType.ByTime)
                    {
                        
                        var parameters = new JobParameters(keyStart, toUpdate.StartCron, toUpdate.StartDate, toUpdate.FinishDate, new SendJobData { Address = toUpdate.ParentDevice.Address, Message = parent.Model.OpenCommand! }, null);
                        await _schedulerManager.ScheduleJob<SendCommandJob, JobParameters>(parameters);
                        parameters = new JobParameters(keyFinish, toUpdate.FinishCron!, toUpdate.StartDate, toUpdate.FinishDate, new SendJobData { Address = toUpdate.ParentDevice.Address, Message = parent.Model.CloseCommand! }, null);
                        await _schedulerManager.ScheduleJob<SendCommandJob, JobParameters>(parameters);
                    }
                }
                else
                {
                    await _schedulerManager.UnscheduleJob(keyStart);
                    await _schedulerManager.UnscheduleJob(keyFinish);
                }

                return new OkObjectResult(new ScheduleDto
                {
                    Id = toUpdate.Id,
                    Name = toUpdate.Name,
                    Description = toUpdate.Description,
                    FinishBy = toUpdate.FinishBy,
                    FinishCron = toUpdate.FinishCron,
                    FinishDeviceId = toUpdate.FinishDevice == null ? null : toUpdate.FinishDevice.Id,
                    FinishValue = toUpdate.FinishValue,
                    StartCron = toUpdate.StartCron,
                    FinishDate = toUpdate.FinishDate,
                    StartDate = toUpdate.StartDate,
                    IsDisabled = toUpdate.IsDisabled
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
