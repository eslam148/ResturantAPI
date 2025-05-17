using Microsoft.AspNetCore.Http;
using ResturantAPI.Services.Enums;
using ResturantAPI.Services.IService;
using ResturantAPI.Services.Model;

namespace ResturantAPI.Services.Service
{
    public class UploudServices : IUploudServices
    {
        public async Task<Response<string>> UploadImageAsync(IFormFile file)
        {
            const long MaxFileSize = 5 * 1024 * 1024; // 5MB
            var allowedTypes = new[] { "image/jpeg", "image/png", "application/pdf" };
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");

            if (file == null || file.Length == 0)
            {
                return Response<string>.Fail("File is empty.", ResponseStatus.BadRequest);
            }

            if (file.Length > MaxFileSize)
            {
                return Response<string>.Fail("File too large.", ResponseStatus.BadRequest);
            }

            if (!allowedTypes.Contains(file.ContentType))
            {
                return Response<string>.Fail("Only JPG, PNG, or PDF files are allowed.", ResponseStatus.BadRequest);
            }

            var extension = Path.GetExtension(file.FileName);
            var newFileName = $"{Guid.NewGuid()}{extension}";
            var fullPath = Path.Combine(uploadFolder, newFileName);

            try
            {
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var relativePath = $"/uploads/{newFileName}";
                return Response<string>.Success(relativePath, "File uploaded successfully.");
            }
            catch (Exception ex)
            {
                 return  Response<string>.Error(
                      message: "An unexpected error occurred while uploading the file.",
                      internalMessage: ex.Message
                  );
            }
        }

    }
}
