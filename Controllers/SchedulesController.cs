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

        // GET: api/Schedules/User
        [HttpGet("User/{id}")]
        public async Task<ActionResult<IEnumerable<ScheduleDto>>> GetSchedulesByUser(int id)
        {
            var schedules = await _context.Schedules.Where(d => d.UserId == id).Include(i => i.User).ToListAsync();
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
        public async Task<IActionResult> PutSchedule(int id, ScheduleDto schedule)
        {
            if (id != schedule.Id)
            {
                return BadRequest();
            }

            var updateItem = await _context.Schedules.FindAsync(schedule.Id);
            if (updateItem == null) {
                return NotFound();
            }

            try
            {
                _mapper.Map(schedule, updateItem);
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
        public async Task<ActionResult<ScheduleDto>> PostSchedule(ScheduleDto schedule)
        {
            var newSchedule = new Schedule();
            _mapper.Map(schedule, newSchedule);

            _context.Schedules.Add(newSchedule);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSchedule), new { id = newSchedule.Id }, newSchedule);
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
