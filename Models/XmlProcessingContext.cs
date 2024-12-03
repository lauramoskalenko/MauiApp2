namespace XmlProcessorApp
{
    /// <summary>
    /// Контекст для вибору стратегії аналізу XML.
    /// </summary>
    public class XmlProcessingContext
    {
        private IXmlAnalyzer _analyzer;

        public XmlProcessingContext(IXmlAnalyzer analyzer)
        {
            _analyzer = analyzer;
        }

        /// <summary>
        /// Зміна стратегії аналізу.
        /// </summary>
        public void SetAnalyzer(IXmlAnalyzer analyzer)
        {
            _analyzer = analyzer;
        }

        /// <summary>
        /// Виконання аналізу за обраною стратегією.
        /// </summary>
        public string ExecuteAnalysis(string filePath, string keyword)
        {
            return _analyzer.Analyze(filePath, keyword);
        }
    }
}
