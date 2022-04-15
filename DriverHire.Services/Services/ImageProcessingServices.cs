using ImageMagick;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverHire.Services.Services
{
    public interface IImageProcessingServices
    {
        void CompressAndSaveImage(Stream imageStream, string path);
        Task<string> UploadImage(IFormFile imageFile, string name);
    }
    public class ImageProcessingServices : IImageProcessingServices
    {
        private const int COMPRESSED_IMAGE_QUALITY = 25;
        private readonly IConfiguration _configuration;
        private IHostEnvironment _hostEnvironment;
        public ImageProcessingServices(IConfiguration configuration,IHostEnvironment hostEnvironment)
        {
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
        }
        public void CompressAndSaveImage(Stream imageStream, string path)
        {
            using (var image = new MagickImage(imageStream))
            {
                image.Format = MagickFormat.Jpg;
                image.Quality = COMPRESSED_IMAGE_QUALITY;
                image.Write(path);
            }
        }

        public async Task<string> UploadImage(IFormFile imageFile, string name)
        {
            if (imageFile == null)
                return string.Empty;

            if (imageFile.Length > 0)
            {
                string fileName = name + Path.GetExtension(imageFile.FileName);
                string filePath = Path.Combine(_hostEnvironment.ContentRootPath, _configuration["DriverHireSettings:ImagePath"], Path.GetFileName(fileName));

                if (imageFile.Length > int.Parse(_configuration["DriverHireSettings:Image.SizeLimit"]))
                {
                    try
                    {
                        CompressAndSaveImage(imageFile.OpenReadStream(), filePath);
                    }
                    catch (Exception )
                    {
                        await SaveToFile(imageFile,filePath);
                    }
                }
                else
                {
                    await SaveToFile(imageFile,filePath);
                }

                return fileName;
            }

            return string.Empty;
        }

        public async  Task<string> SaveToFile(IFormFile formFile, string filePath)
        {
            using (var stream = File.Create(filePath))
            {
                await formFile.CopyToAsync(stream);
                return filePath;
            }
        }
    }
}
