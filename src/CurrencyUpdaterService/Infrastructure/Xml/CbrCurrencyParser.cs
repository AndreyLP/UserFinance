using System.Globalization;
using System.Xml.Linq;

namespace Infrastructure.Xml
{
    public static class CbrCurrencyParser
    {
        public static List<(string Name, decimal Rate)> Parse(string xml)
        {
            var doc = XDocument.Parse(xml);

            var currencies = doc.Descendants("Valute")
                .Select(x => (
                    Name: x.Element("Name")!.Value,
                    Rate: decimal.Parse(
                        x.Element("VunitRate")!.Value,
                        NumberStyles.Float,
                        new CultureInfo("ru-RU")
                    )
                ))
                .ToList();

            return currencies;
        }
    }
}
