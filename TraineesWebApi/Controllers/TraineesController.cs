﻿using System;
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
    [Route("api/trainees")]
    [ApiController]
    public class TraineesController : ControllerBase
    {
        private readonly TraineesContext _context;

        public TraineesController(TraineesContext context)
        {
            _context = context;
        }

        // GET: api/Trainees
        [HttpGet]
        public IEnumerable<Trainee> GetTrainee()
        {
            return _context.Trainee;
        }

        // GET: api/Trainees/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainee([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainee = await _context.Trainee.FindAsync(id);

            if (trainee == null)
            {
                return NotFound();
            }

            return Ok(trainee);
        }

        // PUT: api/Trainees/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainee([FromRoute] int id, [FromBody] Trainee trainee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trainee.TraineeId)
            {
                return BadRequest();
            }

            _context.Entry(trainee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TraineeExists(id))
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

        // POST: api/Trainees
        [HttpPost]
        public async Task<IActionResult> PostTrainee([FromBody] Trainee trainee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Trainee.Add(trainee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainee", new { id = trainee.TraineeId }, trainee);
        }

        // DELETE: api/Trainees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainee([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainee = await _context.Trainee.FindAsync(id);
            if (trainee == null)
            {
                return NotFound();
            }

            _context.Trainee.Remove(trainee);
            await _context.SaveChangesAsync();

            return Ok(trainee);
        }

        private bool TraineeExists(int id)
        {
            return _context.Trainee.Any(e => e.TraineeId == id);
        }
    }
}