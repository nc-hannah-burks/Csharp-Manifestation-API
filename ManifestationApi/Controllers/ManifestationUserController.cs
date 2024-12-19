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

        // PUT: api/ManifestationUser/{id}/email
        [HttpPut("{id}/email")]
        public async Task<IActionResult> PutManifestationUserEmail(Guid id, ManifestationUserEmailUpdate updatedEmail)
        {

            var manifestationUser = await _context.ManifestationUsers.FindAsync(id);
            if (manifestationUser == null)
            {
                return NotFound();
            }

            manifestationUser.Email = updatedEmail.Email;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ManifestationUserExists(id))
            {
                return NotFound();
            }
            return NoContent();
        }

        // PUT: api/ManifestationUser/{id}/name
        [HttpPut("{id}/name")]
        public async Task<IActionResult> PutManifestationUserName(Guid id, ManifestationUserNameUpdate usernameUpdate)
        {

            var manifestationUser = await _context.ManifestationUsers.FindAsync(id);
            if (manifestationUser == null)
            {
                return NotFound();
            }

            manifestationUser.Forename = usernameUpdate.Forename;
            manifestationUser.Forename = usernameUpdate.Surname;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ManifestationUserExists(id))
            {
                return NotFound();
            }
            return NoContent();
        }

        // PUT: api/ManifestationUser/{id}/password
        [HttpPut("{id}/password")]
        public async Task<IActionResult> PutManifestationPassword(Guid id, ManifestationUserPasswordUpdate passwordUpdate)
        {

            var manifestationUser = await _context.ManifestationUsers.FindAsync(id);
            if (manifestationUser == null)
            {
                return NotFound();
            }

            if (passwordUpdate.MemorableAnswer != manifestationUser.MemorableAnswer)
            {
                return Unauthorized("Your Answer is incorrect - It is not possible to update your password at this time");
            }


            manifestationUser.Password = passwordUpdate.Password;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ManifestationUserExists(id))
            {
                return NotFound();
            }
            return NoContent();
        }

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
                Email = newManifestationUser.Email,
                MemorableQuestion = newManifestationUser.MemorableQuestion,
                MemorableAnswer = newManifestationUser.MemorableAnswer
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

        private bool ManifestationUserExists(Guid id)
        {
            return _context.ManifestationUsers.Any(e => e.Id == id);
        }
        // }
    }
};
