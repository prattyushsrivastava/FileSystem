using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using File = DataAccess.Layer.Models.File;

namespace Business.Layer.Services
{
    public interface IFileService
    {
        Task<List<File>> GetFiles();

        Task<File> DeleteAsync(int id, string blobName);

        Task<File> Create(IFormFile ImageFile, string? Title);
    }
}
