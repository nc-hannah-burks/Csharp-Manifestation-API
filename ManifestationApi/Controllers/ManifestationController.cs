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
    public class ManifestationController : ControllerBase
    {
        private readonly ManifestationContext _context;

        public ManifestationController(ManifestationContext context)
        {
            _context = context;
        }

        // GET: api/Manifestation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Manifestation>> GetManifestation(Guid id)
        {
            var manifestation = await _context.Manifestations.FindAsync(id);

            if (manifestation == null)
            {
                return NotFound();
            }

            return manifestation;
        }


        // GET: api/Manifestation/users/{id}
        [HttpGet("users/{id}")]
        public async Task<ActionResult<IEnumerable<Manifestation>>> GetUserManifestation(Guid id)
        {
            var manifestations = await _context.Manifestations.Where(m => m.UserId == id).ToListAsync();

            if (manifestations == null)
            {
                return NotFound();
            }

            return manifestations;
        }
        // PUT: api/Manifestation/{id}/ManifestationImg
        [HttpPut("{id}/ManifestationImg")]
        public async Task<IActionResult> PutManifestationImg(Guid id, ManifestationImgUpdate updatedImgUrl)
        {

            var manifestation = await _context.Manifestations.FindAsync(id);
            if (manifestation == null)
            {
                return NotFound();
            }



            manifestation.ManifestationImg = updatedImgUrl.ManifestationImg;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ManifestationExists(id))
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/Manifestation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Manifestation>> PostManifestation(CreateManifestation newManifestation)
        {
            try
            {
                // Ensure UserId is provided and valid
                if (string.IsNullOrEmpty(newManifestation.UserId))
                {
                    return BadRequest("User ID must be provided.");
                }

                // Turn UserId string into a Guid
                if (!Guid.TryParse(newManifestation.UserId, out Guid userId))
                {
                    return BadRequest($"Invalid User ID format: {newManifestation.UserId}");
                }

                // Find the user in the database
                var user = await _context.ManifestationUsers
                    .FirstOrDefaultAsync(r => r.Id == userId);

                // If the user not found, return a NotFound response
                if (user == null)
                {
                    return NotFound($"User with ID {newManifestation.UserId} not found.");
                }

                System.Diagnostics.Debug.WriteLine($"Found user with ID {userId}");

                // Create a new ManifestationReminder
                var addedManifestation = new Manifestation
                {
                    Id = Guid.NewGuid(),
                    Affirmation = newManifestation.Affirmation,
                    ManifestationImg = newManifestation.ManifestationImg,
                    UserId = userId
                };

                // Add  reminder 
                _context.Manifestations.Add(addedManifestation);
                await _context.SaveChangesAsync();

                // Return a response with the created resource
                return CreatedAtAction(nameof(GetManifestation), new { id = addedManifestation.Id }, addedManifestation);
            }
            catch (Exception ex)
            {
                // Log exception 
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // DELETE: api/Manifestation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManifestation(Guid id)
        {
            var manifestation = await _context.Manifestations.FindAsync(id);
            if (manifestation == null)
            {
                return NotFound();
            }

            _context.Manifestations.Remove(manifestation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ManifestationExists(Guid id)
        {
            return _context.Manifestations.Any(e => e.Id == id);
        }
    }
}
