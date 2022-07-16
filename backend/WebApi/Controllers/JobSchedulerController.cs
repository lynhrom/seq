using Application.Services;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebApi.Controllers
{
    public class JobSchedulerController : BaseApiController
    {
        private readonly IJobSchedulerService _jobTestService;

        public JobSchedulerController(IJobSchedulerService jobTestService)
        {
            _jobTestService = jobTestService ?? throw new ArgumentNullException(nameof(jobTestService));
        }

        /// <summary>
        /// minuteInterval must be an interger and minimum value is 5 minutes
        /// </summary>
        /// <param name="minuteInterval"></param>
        /// <returns></returns>
        [HttpGet("/create-a-minutes-job/{minuteInterval:int:min(5)}")]
        public IActionResult CreateReccuringJob(int minuteInterval)
        {
            RecurringJob.AddOrUpdate("JobTestService.SyncJob", () => _jobTestService.SyncData(), Cron.MinuteInterval(minuteInterval));
            return Ok("You have just create a schedule Job with MinuteInterval=" + minuteInterval);
        }
    }
}
