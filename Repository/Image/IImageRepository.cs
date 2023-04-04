using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IImageRepository
    {
        Task<string> UploadFile(string type, Guid Id, string fileExtension, Stream imageStream);
    }
}
