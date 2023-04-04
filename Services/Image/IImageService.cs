using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Image
{
    public interface IImageService
    {
        Task<string> UploadFile(string type, Guid Id, string fileExtension, byte[] image);
    }
}
