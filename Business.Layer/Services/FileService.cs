using DataAccess.Layer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess.Layer.Models;
using File = DataAccess.Layer.Models.File;
using Microsoft.AspNetCore.Http;

namespace Business.Layer.Services
{
    public class FileService : IFileService
    {
        private readonly IGenricRepository<File> _repo;

        public FileService(IGenricRepository<File> repo)
        {
            _repo = repo;
        }

        public Task<File> Create(IFormFile ImageFile, string? Title)
        {
            return _repo.Create(ImageFile, Title);
        }

        public Task<File> DeleteAsync(int id, string blobName)
        {
            return _repo.DeleteAsync(id, blobName);
        }

        public async Task<List<DataAccess.Layer.Models.File>> GetFiles()
        {
           
                return await _repo.GetFiles();
           
        }
    }
}
