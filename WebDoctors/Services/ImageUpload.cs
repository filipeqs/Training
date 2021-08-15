using System;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using WebDoctors.Contracts;

namespace WebDoctors.Services
{
    public class ImageUpload : IImageUpload
    {
        private readonly IWebHostEnvironment _environment;
        public ImageUpload(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public string GetFilePath(IFormFile file)
        {
            var fileName = Path.GetFileName(file.FileName);
            return $"/uploads/{fileName}";
        }

        public bool SaveFile(IFormFile file)
        {
            if (file == null)
                return false;

            var maxContentLength = 512 * 512 * 1; //0.5 MB
            var allowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".jpeg" };

            if (!allowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.')).ToLower()))
                return false;

            if (file.Length > maxContentLength)
                return false;

            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(_environment.WebRootPath, "uploads");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            using (var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                file.CopyTo(stream);

            return true;
        }
    }
}
