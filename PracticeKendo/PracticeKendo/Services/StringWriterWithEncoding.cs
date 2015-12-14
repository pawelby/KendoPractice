using System.IO;
using System.Text;

namespace PracticeKendo.Services
{
    /// <summary>
    /// Класс, заменяющий StringWriter, когда нужно задать кодировку
    /// </summary>
    public class StringWriterWithEncoding : StringWriter
    {
        private Encoding _encoding;

        public StringWriterWithEncoding(StringBuilder builder, Encoding encoding)
        : base(builder)
        {
            _encoding = encoding;
        }

        public override Encoding Encoding
        {
            get { return _encoding; }
        }
    }
}