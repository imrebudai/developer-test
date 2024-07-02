using System.Globalization;

namespace Taxually.TechnicalTest.Utilities.CsvHelperWrapper
{
    public interface ICsvHelperWrapper
    {
        public string GenerateCsv<T>(IEnumerable<T> records, CultureInfo cultureInfo);
    }
}
