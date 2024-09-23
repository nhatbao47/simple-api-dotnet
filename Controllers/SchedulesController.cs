using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleApi.DTOs;
using SimpleApi.Models;

namespace SimpleApi.Controllers
{
    public class SchedulesController(SimpleApiContext context, IMapper mapper) : MyControllerBase(context, mapper)
    {
        // GET: api/Schedules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetSchedules()
        {
            var schedules = await _context.Schedules.Include(i => i.User).ToListAsync();
            var scheduleDtos = _mapper.Map<IEnumerable<ScheduleDto>>(schedules);
            return Ok(scheduleDtos);
        }

        // GET: api/Schedules/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScheduleDto>> GetSchedule(int id)
        {
            var schedule = await _context.Schedules.Include(i => i.User).FirstOrDefaultAsync(d => d.Id == id);

            if (schedule == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ScheduleDto>(schedule));
        }

        // PUT: api/Schedules/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSchedule(int id, Schedule schedule)
        {
            if (id != schedule.Id)
            {
                return BadRequest();
            }

            _context.Entry(schedule).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScheduleExists(id))
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

        // POST: api/Schedules
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Schedule>> PostSchedule(Schedule schedule)
        {
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSchedule), new { id = schedule.Id }, schedule);
        }

        // DELETE: api/Schedules/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }

            _context.Schedules.Remove(schedule);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedules.Any(e => e.Id == id);
        }
    }
}
