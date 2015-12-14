#region Usings
using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
using System.Xml.Linq;
using System.Xml.Schema;
#endregion

namespace PracticeKendo.Services
{
    /// <summary>
    /// Класс для проверки загружаемого пользователем файла
    /// </summary>
    public class CheckXmlFileService : ICheckXmlFileService
    {

#region Constants
        private const int ENCODING = 1251;
        private const string XMLEXTENSIONTEXT = ".xml";
        private const string XSDFILELOCATIONTEXT = "XsdFileLocation";
        private const string XMLFILELOCATIONTEXT = "XmlFileLocation";
#endregion

#region Public methods
        /// <summary>
        /// Проверка файла на соответствующее расширение и формат. В случае успеха сохраняем в /Content/Xml
        /// </summary>
        /// <param name="xmlFile">Загруженный пользователем файл</param>
        /// <returns>Найденные ошибки</returns>
        public string CheckFileError(HttpPostedFileBase xmlFile)
        {
            var sb = new StringBuilder();

            var extension = Path.GetExtension(xmlFile.FileName);
            if (!string.Equals(extension, XMLEXTENSIONTEXT))
            {
                sb.Append("Файл должен быть только xml");
                return sb.ToString();
            }

            var xmlSchemaSet = new XmlSchemaSet();
            var xsd = ConfigurationManager.AppSettings[XSDFILELOCATIONTEXT];
            var document = new XDocument();
            if (xsd != null)
            {
                try
                {
                    var pathToXsdFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xsd);
                    xmlSchemaSet.Add("", pathToXsdFile);
                    document = XDocument.Load(new StreamReader(xmlFile.InputStream, Encoding.GetEncoding(ENCODING)));
                    var error = false;
                    document.Validate(xmlSchemaSet, (o, e) =>
                    {
                        error = true;
                    });
                    if (error)
                    {
                        sb.Append("Xml файл не соответствует формату схемы");
                        return sb.ToString();
                    }
                }
                catch (Exception)
                {
                    sb.Append("Ошибка сервиса проверки формата файла");
                }
            }
            else
            {
                sb.Append("Не найдено местоположение xsd схемы.");
            }

            if (sb.Length != 0) return sb.ToString();

            var xmlFileLocation = ConfigurationManager.AppSettings[XMLFILELOCATIONTEXT];
            if (xmlFileLocation != null)
            {
                try
                {
                    var pathToXmlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xmlFileLocation, xmlFile.FileName);
                    document.Save(pathToXmlFile);
                }
                catch (Exception)
                {
                    sb.Append("Ошибка сохранения файла");
                    return sb.ToString();
                }
            }
            else
            {
                sb.Append("Не найдено местоположение xml файла.");
            }

            return sb.ToString();
        }
#endregion

    }
}