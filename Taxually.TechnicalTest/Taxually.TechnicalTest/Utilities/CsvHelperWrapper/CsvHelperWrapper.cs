using CsvHelper;
using System.Globalization;

namespace Taxually.TechnicalTest.Utilities.CsvHelperWrapper
{
    public class CsvHelperWrapper : ICsvHelperWrapper
    {
        public string GenerateCsv<T>(IEnumerable<T> records, CultureInfo cultureInfo)
        {
            using (var writer = new StringWriter())
            using (var csv = new CsvWriter(writer, cultureInfo))
            {
                csv.WriteRecords(records);

                return writer.ToString();
            }
        }
    }
}
