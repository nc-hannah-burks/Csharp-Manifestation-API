using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManifestationApi.Models;
using Microsoft.CodeAnalysis.Scripting.Hosting;

namespace ManifestationApiUsers.Controllers
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

        // GET: api/ManifestionUsers
        [HttpGet]

        public async Task<ActionResult<IEnumerable<ManifestationUser>>> GetManifestationUsers()
        {
            return await _context.ManifestationUsers.ToListAsync();
        }


    }
}
