using System.Text;
using System.Xml;

namespace XmlProcessorApp
{
    public class SaxXmlAnalyzer : IXmlAnalyzer
    {
        /// <summary>
        /// Аналіз XML-файлу за допомогою SAX API.
        /// </summary>
        public string Analyze(string filePath, string keyword)
        {
            var result = new StringBuilder();
            using (var reader = XmlReader.Create(filePath))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.HasAttributes)
                    {
                        while (reader.MoveToNextAttribute())
                        {
                            if (reader.Value.Contains(keyword))
                            {
                                result.AppendLine($"[SAX] {reader.Name}: {reader.Value}");
                            }
                        }
                    }
                }
            }
            return result.ToString();
        }
    }
}
