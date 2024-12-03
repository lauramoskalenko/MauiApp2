namespace XmlProcessorApp
{
    public interface IXmlAnalyzer
    {
        /// <summary>
        /// Аналіз XML-файлу за ключовим словом.
        /// </summary>
        /// <param name="filePath">Шлях до XML-файлу.</param>
        /// <param name="keyword">Ключове слово для пошуку.</param>
        /// <returns>Результат аналізу.</returns>
        string Analyze(string filePath, string keyword);
    }
}
