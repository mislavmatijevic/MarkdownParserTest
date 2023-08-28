using System.Text;

namespace MarkdownParserTest.Converters
{
    internal class Markdown2HtmlConverter : IConverter
    {
        public string Convert(string value)
        {
            value = ReplaceHtmlSpecificChars(value);
            value = ReplaceNewLinesWithBreakLines(value);
            value = CreateHtmlHeaders(value);
            return value;
        }

        private string ReplaceNewLinesWithBreakLines(string value)
        {
            return value.Replace("\n", "<br>");
        }

        private string ReplaceHtmlSpecificChars(string value)
        {
            return value
                .Replace("\"", "&quot;")
                .Replace("\'", "&apos;")
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;");
        }

        private string CreateHtmlHeaders(string value)
        {
            value = CreateHtmlHeaderOfLevel(value, 6);
            value = CreateHtmlHeaderOfLevel(value, 5);
            value = CreateHtmlHeaderOfLevel(value, 4);
            value = CreateHtmlHeaderOfLevel(value, 3);
            value = CreateHtmlHeaderOfLevel(value, 2);
            value = CreateHtmlHeaderOfLevel(value, 1);
            return value;
        }

        private string CreateHtmlHeaderOfLevel(string allText, int headerLevel)
        {
            StringBuilder headerLevelTagBuilder = new StringBuilder();
            for (int i = 0; i < headerLevel; i++)
            {
                headerLevelTagBuilder.Append('#');
            }
            string headerLevelValue = headerLevelTagBuilder.ToString();
            string htmlHeaderOpeningTag = $"<h{headerLevel}>";
            string htmlHeaderClosingTag = $"</h{headerLevel}>";

            int indexOfHeader = 0;
            bool doesHeaderLevelExist = false;
            do
            {
                indexOfHeader = allText.IndexOf(headerLevelValue, indexOfHeader);
                doesHeaderLevelExist = indexOfHeader != -1;

                if (doesHeaderLevelExist)
                {
                    allText = InsertOpeningTagInPlaceOfMarkdownHeaderValue(allText, htmlHeaderOpeningTag, headerLevel, indexOfHeader);
                    allText = InsertClosingTagInPlaceOfEndline(allText, htmlHeaderClosingTag, indexOfHeader);
                }

            } while (doesHeaderLevelExist);

            return allText;
        }

        private string InsertOpeningTagInPlaceOfMarkdownHeaderValue(string allText, string htmlHeaderOpeningTag, int headerLevel, int indexOfHeaderPlacement)
        {
            allText = allText.Remove(indexOfHeaderPlacement, headerLevel);
            allText = allText.Insert(indexOfHeaderPlacement, htmlHeaderOpeningTag);
            return allText;
        }

        private static string InsertClosingTagInPlaceOfEndline(string allText, string htmlHeaderClosingTag, int searchAfter)
        {
            string newLineCharacter = "<br>";
            int indexOfEndlineAfterHeader = allText.IndexOf(newLineCharacter, searchAfter);

            if (indexOfEndlineAfterHeader != -1)
            {
                allText = allText.Remove(indexOfEndlineAfterHeader, newLineCharacter.Length);
            }
            else
            {
                indexOfEndlineAfterHeader = allText.Length;
            }

            allText = allText.Insert(indexOfEndlineAfterHeader, htmlHeaderClosingTag);
            return allText;
        }
    }
}