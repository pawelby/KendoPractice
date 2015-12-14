using System.Web;

namespace PracticeKendo.Services
{
    public interface ICheckXmlFileService
    {
        string CheckFileError(HttpPostedFileBase xmlFile);
    }
}
