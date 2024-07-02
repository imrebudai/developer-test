namespace Taxually.TechnicalTest.Utilities.XmlSerializerWrapper
{
    public interface IXmlSerializerWrapper
    {
        public string Serialize<T>(T value);
    }
}
