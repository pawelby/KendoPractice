using System.Collections.Generic;
using System.Web;
using System.Xml.Linq;
using PracticeKendo.Models;

namespace PracticeKendo.Services
{
    public interface IProcessingXmlFileService
    {
        List<CardModel> StartProcessingXmlFile(string postedFileName);
        HtmlString RenderHtmlFromXmlUsingXslt(XElement cardXElement);
    }
}
