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
    [Route("api/[controller]")]
    [ApiController]
    public class TraineeTestsController : ControllerBase
    {
        private readonly TraineesContext _context;

        public TraineeTestsController(TraineesContext context)
        {
            _context = context;
        }

        // GET: api/TraineeTests
        [HttpGet]
        public IActionResult GetTraineeTest()
        {            
            //select* from TraineeTest
            //inner join Test on Test.TestId = TraineeTest.TestId
            //inner join Trainee on TraineeTest.TraineeId = Trainee.TraineeId
            //inner join TestSubject on TestSubject.TestId = Test.TestId
            //inner join[Subject] on TestSubject.SubjectCode = [Subject].SubjectCode

            var data = from val in _context.TraineeTest
                .Include(t => t.TraineeNavigation)
                .Include(t => t.TestNavigation)
                .ThenInclude(t => t.TestSubject)                   
                       
                select new
                {                          
                    data = val.TestNavigation.TestSubject.Select(item => 
                    new {
                        traineeName = val.TraineeNavigation.TraineeName,
                        testName = val.TestNavigation.Name,
                        subjectName = item.SubjectCodeNavigation.Name
                    })
                };

            return Ok(data);            
        }

        // GET: api/TraineeTests/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTraineeTest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var traineeTest = await _context.TraineeTest.FindAsync(id);

            if (traineeTest == null)
            {
                return NotFound();
            }

            return Ok(traineeTest);
        }

        // PUT: api/TraineeTests/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTraineeTest([FromRoute] int id, [FromBody] TraineeTest traineeTest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != traineeTest.TraineeTestId)
            {
                return BadRequest();
            }

            _context.Entry(traineeTest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TraineeTestExists(id))
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

        // POST: api/TraineeTests
        [HttpPost]
        public async Task<IActionResult> PostTraineeTest([FromBody] TraineeTest traineeTest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TraineeTest.Add(traineeTest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTraineeTest", new { id = traineeTest.TraineeTestId }, traineeTest);
        }

        // DELETE: api/TraineeTests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTraineeTest([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var traineeTest = await _context.TraineeTest.FindAsync(id);
            if (traineeTest == null)
            {
                return NotFound();
            }

            _context.TraineeTest.Remove(traineeTest);
            await _context.SaveChangesAsync();

            return Ok(traineeTest);
        }

        private bool TraineeTestExists(int id)
        {
            return _context.TraineeTest.Any(e => e.TraineeTestId == id);
        }
    }
}