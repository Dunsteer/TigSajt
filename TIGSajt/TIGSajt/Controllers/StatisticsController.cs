using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TIGSajt.DB;
using TIGSajt.Models;

namespace TIGSajt.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly ILogger _logger;

        public StatisticsController(ILogger<StatisticsController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("info");
            _logger.LogDebug("debug");
            _logger.LogCritical("critical");
            _logger.LogError("error");
            using (TeorijaIgaraContext tic = new TeorijaIgaraContext())
            {
                var list = await tic.GetStatistics();

                return View(list);
            }
            return View();
        }

        public async Task<IActionResult> All()
        {
            using(TeorijaIgaraContext tic = new TeorijaIgaraContext())
            {
                var list = await tic.Statistics.Include(x=>x.HomeStudent).Include(x=>x.GuestStudent).ToListAsync();

#if DEBUG
                list = list.Where(x => x.GuestStudent.Type == (short)eDbEntryType.Test && x.HomeStudent.Type == (short)eDbEntryType.Test).ToList();
#else
                list = list.Where(x => x.GuestStudent.Type != (short)eDbEntryType.Test && x.HomeStudent.Type != (short)eDbEntryType.Test).ToList();
#endif

                var model = new List<StatisticsModel>();
                foreach(var s in list)
                {
                    model.Add(new StatisticsModel()
                    {
                        Home = s.HomeStudent.Name,
                        Guest = s.GuestStudent.Name,
                        HomePoints = s.HomePoints,
                        GuestPoints = s.GuestPoints,
                        HomeScore = s.HomeScore,
                        GuestScore = s.GuestScore
                    });
                }

                return View(model);
            }
        }

        public async Task<IActionResult> GetForUser(long? userId)
        {
            
            if (userId == null) throw new Exception("User does not exist.");

            return View(new GetForUserModel()
            {
                UserId = userId??0
            });
        }

        public async Task<JsonResult> GetForUserAjax(long? userId)
        {

            if (userId == null) throw new Exception("User does not exist.");

            using (TeorijaIgaraContext tic = new TeorijaIgaraContext())
            {
                var userData = await tic.GetPerUser(userId);

                if (userData == null) throw new Exception("User does not exist.");

                return new JsonResult(userData);
            }
        }
    }
}