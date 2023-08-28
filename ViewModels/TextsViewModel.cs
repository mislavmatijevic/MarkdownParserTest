using MarkdownParserTest.Converters;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MarkdownParserTest.ViewModels
{
    internal class TextsViewModel : INotifyPropertyChanged
    {
        private readonly IConverter _markdownConverter = new Markdown2HtmlConverter();

        private string _markdownText = "";
        public string MarkdownText
        {
            get => _markdownText;
            set
            {
                HTMLText = _markdownConverter.Convert(value);
                _markdownText = value;
                OnPropertyChanged();
            }
        }

        private string _htmlText = " ";
        public string HTMLText
        {
            get => _htmlText;
            private set
            {
                _htmlText = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
