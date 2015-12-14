#region Usings
using System.Web;
using System.Web.Mvc;
using PracticeKendo.Services;
#endregion

namespace PracticeKendo.Controllers
{
    /// <summary>
    /// Main application controller
    /// </summary>
    public class HomeController : Controller
    {
#region Private Fields
        private ICheckXmlFileService _checkXmlFileService;
        private IProcessingXmlFileService _processXmlFileService;
#endregion

        public HomeController(ICheckXmlFileService checkXmlFileService, IProcessingXmlFileService processXmlFileService)
        {
            _checkXmlFileService = checkXmlFileService;
            _processXmlFileService = processXmlFileService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(HttpPostedFileBase fileToUpload)
        {
            if (fileToUpload != null)
            {
                var result = _checkXmlFileService.CheckFileError(fileToUpload);
                if (!string.IsNullOrEmpty(result))
                {
                    ViewBag.Result = result;
                }
                else
                {
                    var models = _processXmlFileService.StartProcessingXmlFile(fileToUpload.FileName);
                    return View("Information", models);
                }
            }
            else
            {
                ViewBag.Result = "Загрузите файл";
            }

            return View("Index");

        }

        public ActionResult Error()
        {
            return View();
        }
    }
}