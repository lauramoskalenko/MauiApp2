using System.Text;
using System.Xml.Linq;
using System.Linq;

namespace XmlProcessorApp
{
    public class LinqXmlAnalyzer : IXmlAnalyzer
    {
        /// <summary>
        /// Аналіз XML-файлу за допомогою LINQ to XML.
        /// </summary>
        public string Analyze(string filePath, string keyword)
        {
            var doc = XDocument.Load(filePath);
            var result = new StringBuilder();

            var matches = doc.Descendants("Scientist")
                             .Attributes()
                             .Where(attr => attr.Value.Contains(keyword));

            foreach (var match in matches)
            {
                result.AppendLine($"[LINQ] {match.Name}: {match.Value}");
            }

            return result.ToString();
        }
    }
}
