using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebDoctors.Contracts;

namespace WebDoctors.Services
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _environment;
        public FileUpload(IWebHostEnvironment environment)
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

            var maxContentLength = 512 * 512 * 2; //1 MB
            var allowedFileExtensions = new string[] { ".pdf" };

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
