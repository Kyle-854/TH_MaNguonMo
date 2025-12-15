using FileShareApp.Data;
using FileShareApp.Filters;
using FileShareApp.Helpers;
using FileShareApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace FileShareApp.Controllers
{
    public class FileController : Controller
    {
        private readonly FileShareDbContext _context;
        private readonly IWebHostEnvironment _env;
        private static readonly Microsoft.AspNetCore.Http.Features.FormOptions _defaultFormOptions = new Microsoft.AspNetCore.Http.Features.FormOptions();

        public FileController(FileShareDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> UploadStream()
        {
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                return BadRequest("Request không phải là Multipart.");
            }

            try
            {
                //string? userIdStr = User.FindFirst("UserId")?.Value;
                //if (userIdStr == null) return Unauthorized();
                //int userId = int.Parse(userIdStr);

                int userId = 1; // For testing purposes only. Replace with actual user ID retrieval logic.

                string? boundary = MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(Request.ContentType), _defaultFormOptions.MultipartBoundaryLengthLimit);
                MultipartReader? reader = new MultipartReader(boundary, Request.Body);
                MultipartSection? section = await reader.ReadNextSectionAsync();

                while (section != null)
                {
                    bool hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);

                    if (hasContentDispositionHeader)
                    {
                        if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                        {
                            string? trustedFileNameForDisplay = System.Net.WebUtility.HtmlEncode(contentDisposition.FileName.Value);

                            string? extension = Path.GetExtension(contentDisposition.FileName.Value).ToLower();
                            string[]? blockedExtensions = new[] { ".exe", ".sh", ".php", ".asp", ".aspx" };
                            if (blockedExtensions.Contains(extension))
                            {
                                return Json(new { success = false, message = "File không được phép." });
                            }

                            string storedFileName = Guid.NewGuid().ToString() + extension;
                            string savePath = Path.Combine(_env.WebRootPath, "uploads", storedFileName);

                            using (FileStream? targetStream = System.IO.File.Create(savePath))
                            {
                                await section.Body.CopyToAsync(targetStream);
                            }

                            FileInfo? fileInfo = new FileInfo(savePath);
                            SharedFile? fileEntity = new SharedFile
                            {
                                UserId = userId,
                                OriginalName = contentDisposition.FileName.Value,
                                StoredFileName = storedFileName,
                                ContentType = section.ContentType ?? "application/octet-stream",
                                FileSize = fileInfo.Length,
                                FileExtension = extension,
                                ShareToken = Guid.NewGuid().ToString("N"),
                                UploadDate = DateTime.Now
                            };

                            _context.Files.Add(fileEntity);
                            await _context.SaveChangesAsync();
                        }
                    }

                    section = await reader.ReadNextSectionAsync();
                }

                return Json(new { success = true, message = "Upload Streaming thành công!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        public async Task<IActionResult> Index()
        {
            //string? userIdStr = User.FindFirst("UserId")?.Value;

            //if (userIdStr == null)
            //{
            //    return RedirectToAction("Login", "Account");
            //}

            //int userId = int.Parse(userIdStr);

            int userId = 1; // For testing purposes only. Replace with actual user ID retrieval logic.

            List<SharedFile>? files = await _context.Files
                .Where(f => f.UserId == userId)
                .OrderByDescending(f => f.UploadDate)
                .ToListAsync();

            return View(files);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            //string? userIdStr = User.FindFirst("UserId")?.Value;
            //int userId = int.Parse(userIdStr);

            int userId = 1; // For testing purposes only. Replace with actual user ID retrieval logic.

            SharedFile? file = await _context.Files.FirstOrDefaultAsync(f => f.FileId == id && f.UserId == userId);

            if (file != null)
            {
                string filePath = Path.Combine(_env.WebRootPath, "uploads", file.StoredFileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                _context.Files.Remove(file);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
