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

        [HttpGet("d/{token}/download")]
        public async Task<IActionResult> DownloadFile(string token)
        {
            SharedFile? file = await _context.Files.FirstOrDefaultAsync(f => f.ShareToken == token);

            if (file == null)
            {
                Response.StatusCode = 404;
                return View("FileNotFound");
            }

            string filePath = Path.Combine(_env.WebRootPath, "uploads", file.StoredFileName);

            if (!System.IO.File.Exists(filePath))
            {
                Response.StatusCode = 404;
                return View("FileNotFound");
            }

            return PhysicalFile(filePath, "application/octet-stream", file.OriginalName);
        }
    }
}
