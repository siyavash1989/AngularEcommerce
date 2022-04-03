using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class BuggyController : ApiBaseController
    {
        private readonly StoreContext _context;
        public BuggyController(StoreContext context)
        {
            _context = context;

        }

        [Authorize]
        [HttpGet("TestAuth")]
        public IActionResult TestAuth()
        {
            return Ok("secret key");
        }

        [HttpGet("NotFound")]
        public IActionResult NotFoundTest()
        {
            return NotFound(new ApiResponse(404));
        }

        [HttpGet("ServerError/{id}")]
        public async Task<IActionResult> ServerError(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p=>p.Id == id);
            var response = product.ToString();

            return Ok();
        }
    }
}