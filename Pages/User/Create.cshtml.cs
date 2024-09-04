using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CRUD_Students.Data;
using CRUD_Students.Model;
using Microsoft.AspNetCore.Hosting;

namespace CRUD_Students.Pages.User
{
    public class CreateModel : PageModel
    {
        private readonly CRUD_StudentsContext _context;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(CRUD_StudentsContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        private bool IsAllowedFileType(string fileType)
        {
            return fileType == ".png" || fileType == ".jpg" || fileType == ".jpeg";
        }

        private bool IsFileSizeAllowed(long fileSize)
        {
            return fileSize <= 10485760; // 10MB
        }

        private string GetUniqueFileName(string fileName)
        {
            return $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
        }

        [BindProperty]
        public Model.User Users { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync(IFormFile UserImage)
        {
            if (UserImage != null && UserImage.Length > 0)
            {
                var fileType = Path.GetExtension(UserImage.FileName).ToLowerInvariant();
                if (IsAllowedFileType(fileType) && IsFileSizeAllowed(UserImage.Length))
                {
                    var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var uniqueFileName = GetUniqueFileName(UserImage.FileName);
                    var fileSavePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(fileSavePath, FileMode.Create))
                    {
                        await UserImage.CopyToAsync(stream);
                    }

                    Users.ImagePath = "/uploads/" + uniqueFileName;
                }
            }

            _context.Users.Add(Users);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
