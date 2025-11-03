using System;
using System.Linq;
using System.Text.RegularExpressions;

public class OpalPageStructureAnalyzer
{
    public class AnalysisResult
    {
        public int WordCount { get; set; }
        public int HeaderCount { get; set; }
        public int ParagraphCount { get; set; }
        public double AverageWordsPerParagraph { get; set; }
    }

    public AnalysisResult Analyze(string htmlContent)
    {
        if (string.IsNullOrWhiteSpace(htmlContent))
            throw new ArgumentException("Content cannot be null or empty.", nameof(htmlContent));

        // Remove script and style tags
        string cleaned = Regex.Replace(htmlContent, @"<script[^>]*>[\s\S]*?</script>|<style[^>]*>[\s\S]*?</style>", "", RegexOptions.IgnoreCase);

        // Count headers
        int headerCount = Regex.Matches(cleaned, @"<h[1-6][^>]*>", RegexOptions.IgnoreCase).Count;

        // Count paragraphs
        int paragraphCount = Regex.Matches(cleaned, @"<p[^>]*>", RegexOptions.IgnoreCase).Count;

        // Remove all HTML tags for word count
        string textOnly = Regex.Replace(cleaned, "<.*?>", " ");
        int wordCount = Regex.Matches(textOnly, @"\b\w+\b").Count;

        // Words per paragraph
        double avgWordsPerParagraph = paragraphCount > 0 ? (double)wordCount / paragraphCount : 0;

        return new AnalysisResult
        {
            WordCount = wordCount,
            HeaderCount = headerCount,
            ParagraphCount = paragraphCount,
            AverageWordsPerParagraph = avgWordsPerParagraph
        };
    }
}
