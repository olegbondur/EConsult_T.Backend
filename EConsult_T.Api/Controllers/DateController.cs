using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using EConsult_T.Api.Models;
using EConsult_T.DAL.EF;
using EConsult_T.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EConsult_T.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/date")]
    public class DateController : Controller
    {
        private EConsultDbContext db;
        public DateController(EConsultDbContext context)
        {
            db = context;
        }

        /// <summary>Get all dates</summary>
        /// <returns>an IEnumerable</returns>
        /// <remarks>Get all dates interval from database</remarks>
        /// <response code="200">Get all dates successful</response>
        /// <response code="401">Unauthorized user</response> 
        /// <response code="500">Internal Server Error</response> 
        [Authorize]
        [HttpGet("all-dates")]
        public async Task<IEnumerable> GetAllDates()
        {
            return db.DateRanges.ToList(); ;
        }

        /// <summary>Get interval</summary>
        /// <returns>an IEnumerable</returns>
        /// <remarks>Get dates intersect interval</remarks>
        /// <response code="200">Set interval successful</response>
        /// <response code="401">Unauthorized user</response> 
        /// <response code="500">Internal Server Error</response> 
        [Authorize]
        [HttpPost("intersection")]
        public async Task<IEnumerable> DatesIntersection([FromBody] DateRangeDto dateDto)
        {
            var dateRanges = db.DateRanges
                .Where(p => p.StartDate > dateDto.StartDate && p.StartDate < dateDto.EndDate
                || (p.EndDate > dateDto.StartDate && p.EndDate < dateDto.EndDate)).ToList();
            return dateRanges;
        }

        /// <summary>Set interval</summary>
        /// <returns>an IActionResult</returns>
        /// <remarks>Save dates interval to database</remarks>
        /// <response code="200">Set interval successful</response>
        /// <response code="401">Unauthorized user</response> 
        /// <response code="500">Internal Server Error</response> 
        [Authorize]
        [HttpPost("interval")]
        public async Task<IActionResult> CreateDatesInterval([FromBody] DateRangeDto dateDto)
        {
            var date = new DateRange
            {
                StartDate = dateDto.StartDate,
                EndDate = dateDto.EndDate
            };

            db.DateRanges.Add(date);
            await db.SaveChangesAsync();
            return Ok();
        }
    }
}