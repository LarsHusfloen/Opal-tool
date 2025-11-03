public class PageStructureTool : IOpalTool
{
    public string Name => "Page Structure Analyzer";
    public string Description => "Analyzes HTML for word count, header count, and paragraph structure.";

    public object Run(string input)
    {
        var analyzer = new OpalPageStructureAnalyzer();
        return analyzer.Analyze(input);
    }
}
