using Microsoft.Extensions.Configuration;
using Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Common;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;

namespace Repository
{
    public class ImageRepository : IImageRepository
    {
        private Settings settings { get; set; }
        private string StorageConnectionString { get; set; }
        private string ContainerName = "blob";
        private BlobContainerClient ContainerClient { get; set; }
        private BlobClient BlobClient { get; set; }

        public ImageRepository(IConfiguration config)
        {
            settings = new Settings(config);
            StorageConnectionString = settings.GetStorageConnectionString();
            ContainerClient = new BlobContainerClient(StorageConnectionString, ContainerName);
            ContainerClient.CreateIfNotExists();
        }

        public async Task<string> UploadFile(string type, Guid Id, string fileExtension, Stream imageStream)
        {
            string now = DateTime.UtcNow.ToString("u", CultureInfo.InvariantCulture);
            BlobClient = ContainerClient.GetBlobClient($"{type}-{Id}-{now}.{fileExtension}");
            await BlobClient.UploadAsync(imageStream);
            return BlobClient.Uri.ToString();
        }
    }
}
