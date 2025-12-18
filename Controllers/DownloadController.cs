using FileShareApp.Data;
using FileShareApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FileShareApp.Controllers
{
    public class DownloadController : Controller
    {
        private readonly FileShareDbContext _context;
        private readonly IWebHostEnvironment _env;

        public DownloadController(FileShareDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet("d/{token}")]
        public async Task<IActionResult> Index(string token)
        {
            SharedFile? file = await _context.Files
                .Include(f => f.User)
                .FirstOrDefaultAsync(f => f.ShareToken == token);

            if (file == null)
            {
                Response.StatusCode = 404;
                return View("FileNotFound");
            }

            return View(file);
        }
    }
}
