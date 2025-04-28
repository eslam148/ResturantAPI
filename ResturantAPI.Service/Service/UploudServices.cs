using Microsoft.AspNetCore.Http;
using ResturantAPI.Services.IService;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
namespace ResturantAPI.Services.Service
{
    public class UploudServices : IUploudServices
    {
        public async Task<string> UploadImageToAzureAsync(IFormFile file)
        {
            //string connectionString = "DefaultEndpointsProtocol=https;AccountName=eslam123456789;AccountKey=BxCgqrKzR1m5GoMbFJNybJysiEeKHvg1YAI0Uz0eVnDQ+zd7yRQWVGI8AbkGyAZUVuVp14H6SKqX+AStcqf2Ng==;EndpointSuffix=core.windows.net";
            //string containerName = "eslam1";

            //BlobContainerClient container = new BlobContainerClient(connectionString, containerName);
            //await container.CreateIfNotExistsAsync(PublicAccessType.Blob); // يتيح الوصول العام للصور

            //string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            //BlobClient blobClient = container.GetBlobClient(fileName);

            //using (var stream = file.OpenReadStream())
            //{
            //    await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });
            //}

            return "Hello"; // blobClient.Uri.ToString(); // الرابط المباشر للصورة
        }
    }
}
