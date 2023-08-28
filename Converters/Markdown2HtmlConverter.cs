namespace MarkdownParserTest.Converters
{
    internal class Markdown2HtmlConverter : IConverter
    {
        public string Convert(string value)
        {
            value = ReplaceHtmlSpecificChars(value);
            return value;
        }

        private string ReplaceHtmlSpecificChars(string value)
        {
            return value
                .Replace("\"", "&#34;")
                .Replace("\'", "&#39;")
                .Replace("&", "&#38;")
                .Replace("<", "&#60;")
                .Replace(">", "&#62;");
        }
    }
}