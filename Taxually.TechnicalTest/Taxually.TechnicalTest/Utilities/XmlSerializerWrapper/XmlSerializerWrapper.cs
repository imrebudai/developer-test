using System.Xml.Serialization;

namespace Taxually.TechnicalTest.Utilities.XmlSerializerWrapper
{
    public class XmlSerializerWrapper : IXmlSerializerWrapper
    {
        public string Serialize<T>(T value)
        {
            XmlSerializer serializer = new(typeof(T));

            using (StringWriter stringwriter = new())
            {
                serializer.Serialize(stringwriter, value);
                string xml = stringwriter.ToString();
                return xml;
            }
        }
    }
}
