using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using DataAccess.Layer.Models;
using DataAccess.Layer.Repository;

using Microsoft.EntityFrameworkCore;
using File = DataAccess.Layer.Models.File;
using Microsoft.AspNetCore.Http;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;

namespace DataAccess.Layer.Repository
{
    public class GenricRepository<TModel> : IGenricRepository<TModel> where TModel : class
    {

        private readonly FilesDbContext _dbContext;

        File ob1 = new File();

        public GenricRepository(FilesDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }
        

     
        public async  Task<List<TModel>> GetFiles()
        {
            try{
                return await _dbContext.Set<TModel>().ToListAsync();
            }
            catch{
                throw;
            }
        }

        public async Task<File> DeleteAsync(int id, string blobName) 
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=taskmgi;AccountKey=tuJIS7vmEmFmfkS2PXsVXL3vECCPx0nfJEieWpHZYz9k+Uc+kqcsUHQAj+hDp3nqq62Pp9Z22tRZ+AStggzgvg==;EndpointSuffix=core.windows.net";
            string containerName = "development";

            BlobClient blob = new BlobClient(connectionString, containerName, blobName);
            blob.Delete();
            var f = await _dbContext.Files.FirstOrDefaultAsync(x =>x.Id== id);
            if (f == null)
                return null;
            _dbContext.Files.Remove(f);
            await _dbContext.SaveChangesAsync();
            return f;
         
        }

        public async Task<File> Create(IFormFile ImageFile, string? Title)
        {
            string fileName = Path.GetFileName(ImageFile.FileName);
            string p1 = "D:\\projects\\File_Upload\\wwwroot\\Images\\";
            string p = "D:\\projects\\File_Upload\\wwwroot\\";
            string file_path = Path.Combine(p1, fileName);
            Directory.CreateDirectory(p1);
            var stream = new FileStream(file_path, FileMode.Create);
            await ImageFile.CopyToAsync(stream);
            stream.Close();
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=taskmgi;AccountKey=tuJIS7vmEmFmfkS2PXsVXL3vECCPx0nfJEieWpHZYz9k+Uc+kqcsUHQAj+hDp3nqq62Pp9Z22tRZ+AStggzgvg==;EndpointSuffix=core.windows.net";
            string containerName = "development";
            BlobContainerClient containerClient = new BlobContainerClient(connectionString, containerName);
            var filePathOverCloud = file_path.Replace(p, string.Empty);
            long filesize = ImageFile.Length;
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
            CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);


            //CloudBlob blockBlob = containerClient.GetBlobReference("my-image-blob");
            //blockBlob.Properties.ContentType = "image/jpg";

            //using (var fileStream = System.IO.File.OpenRead(filePath))
            //{
            //    blockBlob.UploadFromStream(fileStream);
            //}
            if (filesize <= 5000000)
            {
                MemoryStream str = new MemoryStream(System.IO.File.ReadAllBytes(file_path));
                foreach (BlobItem blob in containerClient.GetBlobs())
                {
                    string s = blob.Name;
                    string a = "/";
                    string b = @"\";
                    if (s.Replace(a, b) == filePathOverCloud)
                    {
                        return null;
                    }

                }
                Console.WriteLine("File started to upload");
                containerClient.UploadBlob(filePathOverCloud, str);
                str.Close();

                ob1.Fpath = filePathOverCloud;
                ob1.Title = Title;
                _dbContext.Add(ob1);
                await _dbContext.SaveChangesAsync();
                CloudBlob blockBlob = cloudBlobContainer.GetBlobReference(filePathOverCloud);

                blockBlob.Properties.ContentType = "image/jpg";

                await blockBlob.SetPropertiesAsync();
                return ob1;
            }
            //ob1.Fpath = filePathOverCloud;
            //ob1.Title = Title;
            //_dbContext.Add(ob1);
            //await _dbContext.SaveChangesAsync();
            //return ob1;
            
            return ob1;
        }
    }
    
}
