using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SnacksUdemy.Models;

namespace SnacksUdemy.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminImagensController : Controller
    {

        private readonly ConfigurationImagens _myConfig;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AdminImagensController(IWebHostEnvironment hostingEnvironment,
            IOptions<ConfigurationImagens> myConfiguration)
        {
            _myConfig = myConfiguration.Value;
            _hostingEnvironment = hostingEnvironment;
        }


        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {

            if (files == null)
            {
                ViewData["Erro"] = "Error: Not Have files.";
                return View(ViewData);
            }
            if (files.Count > 10)
            {
                ViewData["Erro"] = "Error: Count files max ";
                return View(ViewData);
            }

            long size = files.Sum(f => f.Length);

            var filePathName = new List<string>();
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                _myConfig.NamePastaImagensProducts);

            foreach (var formFile in files)
            {
                if (formFile.FileName.Contains(".jpg"))
                {
                    var fileNameWithPath = string.Concat(filePath, "\\", formFile.FileName);
                    filePathName.Add(fileNameWithPath);

                    using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            ViewData["Result"] = $"{files.Count} files send to server," + $"with size total: {size} bytes";
            ViewBag.Files = filePathName;
            return View(ViewData);
        }


        public IActionResult GetImagens()
        {
            FileManagerModel model = new FileManagerModel();

            var userImagesPath = Path.Combine(_hostingEnvironment.WebRootPath,
              _myConfig.NamePastaImagensProducts);

            DirectoryInfo dir = new DirectoryInfo(userImagesPath);

            FileInfo[] files = dir.GetFiles();

            model.PathImagensProducts = _myConfig.NamePastaImagensProducts;

            if (files.Length == 0)
            {
                ViewData["Erro"] = $"not have file in path{userImagesPath}";
            }

            model.Files = files;
            return View(model);

        }

        public IActionResult Deletefile(string fname)
        {
            string _imagemDeleta = Path.Combine(_hostingEnvironment.WebRootPath,
                _myConfig.NamePastaImagensProducts + "\\", fname);

            if ((System.IO.File.Exists(_imagemDeleta)))
            {
                System.IO.File.Delete(_imagemDeleta);
                ViewData["Deletado"] = $"Arquivo(s) {_imagemDeleta} deletado com sucesso";
            }
            return View("index");
        }


    }
}
