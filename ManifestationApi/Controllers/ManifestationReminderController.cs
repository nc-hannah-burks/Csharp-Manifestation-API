using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManifestationApi.Models;

namespace ManifestationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManifestationReminderController : ControllerBase
    {
        private readonly ManifestationContext _context;

        public ManifestationReminderController(ManifestationContext context)
        {
            _context = context;
        }

        // GET: api/ManifestationReminder/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ManifestationReminder>> GetManifestationReminder(Guid id)
        {
            var manifestationReminder = await _context.ManifestationReminders.FindAsync(id);

            if (manifestationReminder == null)
            {
                return NotFound();
            }

            return manifestationReminder;
        }

        // GET: api/ManifestationReminder/{id}
        [HttpGet("users/{id}")]
        public async Task<ActionResult<IEnumerable<ManifestationReminder>>> GetUserManifestationReminders(Guid id)
        {
            var manifestationReminders = await _context.ManifestationReminders.Where(m => m.UserId == id).ToListAsync();

            if (manifestationReminders == null)
            {
                return NotFound();
            }

            return manifestationReminders;
        }
        // // PUT: api/ManifestationReminder/5
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutManifestationReminder(Guid id, ManifestationReminder manifestationReminder)
        // {
        //     if (id != manifestationReminder.Id)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(manifestationReminder).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!ManifestationReminderExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // POST: api/ManifestationReminder
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ManifestationReminder>> PostManifestationReminder(CreateManifestationReminder newManifestationReminder)
        {
            try
            {
                // Ensure UserId is provided and valid
                if (string.IsNullOrEmpty(newManifestationReminder.UserId))
                {
                    return BadRequest("User ID must be provided.");
                }

                // Turn UserId string into a Guid
                if (!Guid.TryParse(newManifestationReminder.UserId, out Guid userId))
                {
                    return BadRequest($"Invalid User ID format: {newManifestationReminder.UserId}");
                }

                // Find the user in the database
                var user = await _context.ManifestationUsers
                    .FirstOrDefaultAsync(r => r.Id == userId);

                // If the user not found, return a NotFound response
                if (user == null)
                {
                    return NotFound($"User with ID {newManifestationReminder.UserId} not found.");
                }

                System.Diagnostics.Debug.WriteLine($"Found user with ID {userId}");

                // Create a new ManifestationReminder
                var newReminder = new ManifestationReminder
                {
                    Id = Guid.NewGuid(),
                    ReminderTime = newManifestationReminder.ReminderTime,
                    UserId = userId
                };

                // Add  reminder 
                _context.ManifestationReminders.Add(newReminder);
                await _context.SaveChangesAsync();

                // Return a response with the created resource
                return CreatedAtAction(nameof(GetManifestationReminder), new { id = newReminder.Id }, newReminder);
            }
            catch (Exception ex)
            {
                // Log exception 
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        // DELETE: api/ManifestationReminder/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManifestationReminder(Guid id)
        {
            var manifestationReminder = await _context.ManifestationReminders.FindAsync(id);
            if (manifestationReminder == null)
            {
                return NotFound();
            }

            _context.ManifestationReminders.Remove(manifestationReminder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ManifestationReminderExists(Guid id)
        {
            return _context.ManifestationReminders.Any(e => e.Id == id);
        }
    }
}
