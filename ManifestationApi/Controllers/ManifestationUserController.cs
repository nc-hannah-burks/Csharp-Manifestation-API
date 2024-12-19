using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManifestationApi.Models;

namespace ManifestationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManifestationUserController : ControllerBase
    {
        private readonly ManifestationContext _context;

        public ManifestationUserController(ManifestationContext context)
        {
            _context = context;
        }

        // GET: api/ManifestationUser/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ManifestationUser>> GetManifestationUser(Guid id)
        {
            var manifestationUser = await _context.ManifestationUsers.FindAsync(id);

            if (manifestationUser == null)
            {
                return NotFound();
            }

            return manifestationUser;
        }

        // // PUT: api/ManifestationUser/{id}
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutManifestationUser(Guid id, ManifestationUser updatedManifestationUser)
        // {
        //     if (id != updatedManifestationUser.id)
        //     {
        //         return BadRequest("User ID in URL does not match User ID in the payload.");
        //     }

        //     _context.Entry(updatedManifestationUser).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!ManifestationUserExists(id))
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

        // POST: api/ManifestationUser
        [HttpPost]
        public async Task<ActionResult<ManifestationUser>> PostManifestationUser(CreateManifestationUser newManifestationUser)
        {
            var newUser = new ManifestationUser
            {
                Id = Guid.NewGuid(),
                Forename = newManifestationUser.Forename,
                Surname = newManifestationUser.Surname,
                Password = newManifestationUser.Password,
                Email = newManifestationUser.Email
            };

            _context.ManifestationUsers.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetManifestationUser), new { id = newUser.Id }, newUser);
        }

        // // DELETE: api/ManifestationUser/{id}
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteManifestationUser(Guid id)
        // {
        //     var manifestationUser = await _context.ManifestationUser.FindAsync(id);

        //     if (manifestationUser == null)
        //     {
        //         return NotFound();
        //     }

        //     _context.ManifestationUser.Remove(manifestationUser);
        //     await _context.SaveChangesAsync();

        //     return NoContent();
        // }

        //     private bool ManifestationUserExists(Guid id)
        //     {
        //         return _context.ManifestationUser.Any(e => e.Id == id);
        //     }
        // }
    }
};
