using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManifestationApi.Models;
using Microsoft.CodeAnalysis.Scripting.Hosting;

namespace ManifestationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManifestionUsersController : ControllerBase
    {
        private readonly ManifestationContext _context;

        public ManifestionUsersController(ManifestationContext context)
        {
            _context = context;
        }

        // GET: api/ManifestionUser
        [HttpGet]

        public async Task<ActionResult<IEnumerable<ManifestationUser>>> GetManifestationUsers()
        {
            return await _context.ManifestationUsers.ToListAsync();
        }

        // GET: api/ManifestionUser/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ManifestationUser>> GetManifestationUser(int id)
        {
            var manifestationUser = await _context.ManifestationUsers.FindAsync(id);

            if (manifestationUser == null)
            {
                return NotFound();
            }

            return manifestationUser;
        }

        // PUT: api/ManifestionUser/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManifestationUser(int id, ManifestationUser manifestationUser)
        {

            if (id != manifestationUser.UserId)
            {
                return BadRequest();
            }

            _context.Entry(manifestationUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManifestationUserExists(id))
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

        // POST: api/ManifestionUser
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ManifestationUser>> PostManifestationUser(ManifestationUser manifestationUser)
        {
            _context.ManifestationUsers.Add(manifestationUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetManifestationUser), new { id = manifestationUser.UserId }, manifestationUser);
        }

        // DELETE: api/ManifestionUser/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManifestationUser(int id)
        {
            var manifestationUser = await _context.ManifestationUsers.FindAsync(id);
            if (manifestationUser == null)
            {
                return NotFound();
            }

            _context.ManifestationUsers.Remove(manifestationUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ManifestationUserExists(int id)
        {
            return _context.ManifestationUsers.Any(e => e.UserId == id);
        }
    }
}
