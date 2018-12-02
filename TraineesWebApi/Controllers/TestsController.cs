using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Trainees.Models;

namespace TraineesWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/tests")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly TraineesContext _context;

        public TestsController(TraineesContext context)
        {
            _context = context;
        }

        // GET: api/Tests
        [HttpGet]
        public IEnumerable<Test> GetTest()
        {
            return _context.Test;

            //select* from TestSubject
            //inner join Test on TestSubject.TestId = Test.TestId
            //inner join[Subject] on TestSubject.SubjectCode = [Subject].SubjectCode
            //inner join TraineeTest on Test.TestId = TraineeTest.TestId
            //inner join Trainee on TraineeTest.TraineeId = Trainee.TraineeId

            //Get RouteSchedule records

            //var testsContext = from _context.

                //var schedulesContext = from scheduleRec in stopsContext.BusRouteCodeNavigation.RouteSchedule
                //    .OrderByDescending(r => r.IsWeekDay)
                //    .ThenBy(r => r.StartTime)

                               //                       select new RouteScheduleEx
                               //                       {
                               //                           RouteScheduleId = scheduleRec.RouteScheduleId,
                               //                           BusRouteCode = scheduleRec.BusRouteCode,
                               //                           StartTime = scheduleRec.StartTime,
                               //                           IsWeekDay = scheduleRec.IsWeekDay,
                               //                           Comments = scheduleRec.Comments,

                               //                           //Add the offset minutes to each schedule’s start time 
                               //                           //to get the arrival times for the stop.
                               //                           ArrivalTime = scheduleRec.StartTime.Add(TimeSpan.FromMinutes(offsetMin))
                               //                       };

                               //return View(schedulesContext);



        }

        // GET: api/Tests/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var test = await _context.Test.FindAsync(id);

            if (test == null)
            {
                return NotFound();
            }

            return Ok(test);
        }

        // PUT: api/Tests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTest([FromRoute] int id, [FromBody] Test test)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != test.TestId)
            {
                return BadRequest();
            }

            _context.Entry(test).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tests
        [HttpPost]
        public async Task<IActionResult> PostTest([FromBody] Test test)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Test.Add(test);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTest", new { id = test.TestId }, test);
        }

        // DELETE: api/Tests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var test = await _context.Test.FindAsync(id);
            if (test == null)
            {
                return NotFound();
            }

            _context.Test.Remove(test);
            await _context.SaveChangesAsync();

            return Ok(test);
        }

        private bool TestExists(int id)
        {
            return _context.Test.Any(e => e.TestId == id);
        }
    }
}