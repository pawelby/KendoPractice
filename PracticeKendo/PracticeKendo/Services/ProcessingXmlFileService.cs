#region Usings
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Xsl;
using PracticeKendo.Models;
#endregion

namespace PracticeKendo.Services
{
    /// <summary>
    /// Класс для обработки и преобразования xml в html
    /// </summary>
    public class ProcessingXmlFileService : IProcessingXmlFileService
    {
#region Constants
        private const string XMLFILELOCATIONTEXT = "XmlFileLocation";
        private const string XSLTFILELOCATIONTEXT = "XsltFileLocation";
        private const int ENCODING = 1251;
#endregion

        #region Public Methods
        /// <summary>
        /// Начало обработки файла, заполнение Card модели
        /// </summary>
        /// <param name="postedFileName">Имя загружаемого файл</param>
        /// <returns>Лист card моделей</returns>
        public List<CardModel> StartProcessingXmlFile(string postedFileName)
        {
            var models = new List<CardModel>();

            var xmlFileLocation = ConfigurationManager.AppSettings[XMLFILELOCATIONTEXT];

            if (xmlFileLocation == null)
            {
                return models;
            }
            try
            {
                var pathToXmlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xmlFileLocation, postedFileName);
                var document = XDocument.Load(pathToXmlFile);

                decimal result = 0;
                var dt = DateTime.Now;

                var cards = document.Descendants("Card");
                foreach (var card in cards)
                {
                    var html = RenderHtmlFromXmlUsingXslt(card);
                    var cardModel = new CardModel { CardInfo = html };
                    double cardId = 0;
                    if (double.TryParse(card.Attribute("Id").Value, out cardId))
                    {
                        cardModel.Id = cardId;
                    }
                    cardModel.Transactions = card.Descendants("Transaction")
                        .Select(x =>
                        {
                            var idElement = x.Element("ID");
                            var ammountElement = x.Element("Ammount");
                            var categoryNameElement = x.Element("CategoryName");
                            var cityElement = x.Element("City");
                            var countryElement = x.Element("Country");
                            var detailsElement = x.Element("Details");
                            var transTypeNameElement = x.Element("TransTypeName");
                            var transDateElement = x.Element("TransDate");
                            return new TransactionModel
                            {
                                Id = idElement != null ? idElement.Value : string.Empty,
                                Ammount = ammountElement != null && decimal.TryParse(ammountElement.Value, out result) ? result : 0,
                                CategoryName = categoryNameElement != null ? categoryNameElement.Value : string.Empty,
                                City = cityElement != null ? cityElement.Value : string.Empty,
                                Country = countryElement != null ? countryElement.Value : string.Empty,
                                Details = detailsElement != null ? detailsElement.Value : string.Empty,
                                TransTypeName = transTypeNameElement != null ? transTypeNameElement.Value : string.Empty,
                                TransactionDate = transDateElement != null && 
                                    DateTime.TryParse(transDateElement.Value, out dt) ? dt : DateTime.Now
                            };
                        }).ToList();
                    models.Add(cardModel);
                }
            }
            catch (Exception)
            {
                return new List<CardModel>();
            }
            
            return models;
        }

        /// <summary>
        /// Преобразование xml фрагмента в html, используя xslt
        /// </summary>
        /// <param name="cardXElement">card xElement</param>
        /// <returns>Html строку</returns>
        public HtmlString RenderHtmlFromXmlUsingXslt(XElement cardXElement)
        {
            var xsltArgumentList = new XsltArgumentList();
            var xslCompiledTransform = new XslCompiledTransform();
            var pathToXslt = ConfigurationManager.AppSettings[XSLTFILELOCATIONTEXT];
            var emptyHtmlString = new HtmlString(string.Empty);
            if (pathToXslt == null)
            {
                return emptyHtmlString;
            }
            pathToXslt = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pathToXslt);
            try
            {
                xslCompiledTransform.Load(pathToXslt);
            }
            catch (Exception)
            {
                return emptyHtmlString;
            }

            var xmlReaderSettings = new XmlReaderSettings();
            using (var reader = XmlReader.Create(cardXElement.CreateReader(), xmlReaderSettings))
            {
                var sw = new StringWriterWithEncoding(new StringBuilder(), Encoding.GetEncoding(ENCODING));
                xslCompiledTransform.Transform(reader, xsltArgumentList, sw);
                var htmlString = new HtmlString(sw.ToString());
                return htmlString;
            }
        }
#endregion 
    }
}