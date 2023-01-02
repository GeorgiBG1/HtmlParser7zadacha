using System.Text.RegularExpressions;

namespace htmlParser
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TextWithoutHtml();
        }
        static void TextWithoutHtml()
        {
            string input = Console.ReadLine()!;
            HtmlText htmlText = new HtmlText();
            htmlText.RawText.Add(input);
            while (input != "end")
            {
                input = Console.ReadLine()!;
                htmlText.RawText.Add(input);
            }
            htmlText.Convert();
            Console.Clear();
            Console.WriteLine($"Title:{string.Join(" ", htmlText.Title)}");
            Console.WriteLine($"Content:{string.Join(" ", htmlText.Content)}");
        }
    }
    public class HtmlText
    {
        private string htmlTitlePattern = @"<title\s*[a-z]*\=*[""a-z\s\.]*>[\\]*(<[a-z]\s*[a-z]*\=*[""a-z\s\.]*>)*[\\]*(?<title>[A-Z*a-z\s]+)[\s?]*((<[a-z]+\s*[a-z]*\=*[""a-z\s\.]*>(?<title>[A-Z*a-z\s]*))|(?<title>[A-Za-z\s]*)*|(<\/[a-z]+>|\\.?(?<title>[a-z\s*])+)*)*(?<title>[A-Z*a-z\s]*)*((<[a-z]+>)|(<\/[a-z]+>|\\[a-z]))*<\/title>";
        private string htmlContentPattern = @"<body\s*[a-z]*\=*[""a-z\s\.]*>[\\]*(<[a-z]\s*[a-z]*\=*[""a-z\s\.]*>)*[\\]*(?<content>[A-Z*a-z\s]+)[\s?]*((<[a-z]+\s*[a-z]*\=*[""a-z\s\.]*>(?<content>[A-Z*a-z\s]*))|(?<content>[A-Z*a-z\s]*)[\s?]*|(<\/[a-z]+>|\\.?(?<content>[a-z\s*])+)*)*(?<content>[A-Z*a-z\s]*)*((<[a-z]+>)|(<\/[a-z]+>|\\[a-z]))*<\/body>";
        private string rawText = "";
        public List<string> RawText { get; set; } = new List<string>();
        public List<string> Title { get; set; } = new List<string>();
        public List<string> Content { get; set; } = new List<string>();
        public void ListToString()
        {
            RawText.Remove(RawText.Last());
            rawText = string.Concat(RawText);
        }
        public void Convert()
        {
            ListToString();
            MatchCollection titleMatch = Regex.Matches(rawText, htmlTitlePattern);
            MatchCollection contentMatch = Regex.Matches(rawText, htmlContentPattern);
            List<string> Title = titleMatch.Select(m => m.Groups["title"].Value).ToList();
            List<string> Content = contentMatch.Select(m => m.Groups["content"].Value).ToList();
            this.Title = Title;
            this.Content = Content;
        }

    }
}