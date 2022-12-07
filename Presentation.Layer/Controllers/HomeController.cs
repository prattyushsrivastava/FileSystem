using Business.Layer.Services;
using Microsoft.AspNetCore.Mvc;
using Presentation.Layer.Models;
using System.Diagnostics;
using File = DataAccess.Layer.Models.File;

namespace Presentation.Layer.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        private readonly IFileService _fileService;

        public HomeController(IFileService fileService)
        {
            _fileService = fileService;
        }


        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> show()
        {

            List<File> files = await _fileService.GetFiles();
            return View(files);
        }

        public async Task<IActionResult> Delete(int id, string blobName)
        {
            var x = await _fileService.DeleteAsync(id, blobName);

            return RedirectToAction(nameof(show));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile ImageFile, string? Title)
        {
            var f = new File();



            f = await _fileService.Create(ImageFile, Title);
            List<File> files = await _fileService.GetFiles();

            if (f == null)
            {
                return View("Exists");
            }
            return View("show", files);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}