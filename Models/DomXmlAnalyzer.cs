using System.Text;
using System.Xml;

namespace XmlProcessorApp
{
    public class DomXmlAnalyzer : IXmlAnalyzer
    {
        /// <summary>
        /// Аналіз XML-файлу за допомогою DOM API.
        /// </summary>
        public string Analyze(string filePath, string keyword)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            var result = new StringBuilder();

            foreach (XmlNode node in doc.SelectNodes("//Scientist"))
            {
                if (node.Attributes != null)
                {
                    foreach (XmlAttribute attr in node.Attributes)
                    {
                        if (attr.Value.Contains(keyword))
                        {
                            result.AppendLine($"[DOM] {attr.Name}: {attr.Value}");
                        }
                    }
                }
            }

            return result.ToString();
        }
    }
}
