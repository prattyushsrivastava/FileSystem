using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataAccess.Layer.Models;
using Microsoft.AspNetCore.Http;
using File = DataAccess.Layer.Models.File;

namespace DataAccess.Layer.Repository
{
    public interface IGenricRepository<TModel> where TModel :class
    {

        Task<List<TModel>> GetFiles();

        Task<File> Create(IFormFile ImageFile, string? Title);

        Task<File> DeleteAsync(int id, string blobName);


    }
}
