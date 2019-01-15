using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TIGSajt.DB;
using TIGSajt.Models;

namespace TIGSajt.Controllers.API
{
    [Produces("application/json")]
    [Route("api/Statistics")]
    public class StatisticsController : Controller
    {
        [HttpPost]
        [Route("Information")]
        public async Task<ApiReturnModel> Information([FromBody] StatisticsModel model)
        {
            if(model!=null && model.HasValue())
            {
                using(TeorijaIgaraContext tic = new TeorijaIgaraContext())
                {
                    var homeStudent = tic.Student.FirstOrDefault(x => x.Name == model.Home);
                    var guestStudent = tic.Student.FirstOrDefault(x => x.Name == model.Guest);

                    if(homeStudent == null)
                    {
                        homeStudent = new Student()
                        {
                            Name = model.Home,
                            Type = (short)model.Type
                        };
                        tic.Student.Add(homeStudent);
                    }

                    if (guestStudent == null)
                    {
                        guestStudent = new Student()
                        {
                            Name = model.Guest,
                            Type = (short)model.Type
                        };
                        tic.Student.Add(guestStudent);
                    }

                    var stat = new Statistics()
                    {
                        HomeStudent = homeStudent,
                        GuestStudent = guestStudent,
                        HomePoints = model.HomePoints,
                        GuestPoints = model.GuestPoints,
                        HomeScore = model.HomeScore,
                        GuestScore = model.GuestScore,
                        Created = DateTime.UtcNow,
                    };

                    tic.Statistics.Add(stat);
                    await tic.SaveChangesAsync();
                    return new ApiReturnModel()
                    {
                        ErrorCode = null,
                        OK = true,
                        ErrorMessage = ""
                    };
                }
            }
            return new ApiReturnModel()
            {
                ErrorCode = eErrorCode.InvalidData,
                OK = false,
                ErrorMessage = "Invalid object received."
            };
        }
    }
}