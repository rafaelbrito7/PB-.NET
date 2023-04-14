using Microsoft.AspNetCore.Http;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Image
{
    public class ImageService : IImageService
    {
        private IImageRepository ImageRepository { get; set; }

        public ImageService(IImageRepository imageRepository)
        {
            ImageRepository = imageRepository;
        }

        public async Task<string> UploadFile(string type, Guid Id, string fileExtension, byte[] image)
        {
            Stream imageStream = new MemoryStream(image);
            string photoUrl = await ImageRepository.UploadFile(type, Id, fileExtension, imageStream);
            return photoUrl;
        }
    }
}
