using System;

class Program
{
    static void Main(string[] args)
    {
        // Register the tool
        OpalToolRegistry.Register(new PageStructureTool());

        // Example usage: run the tool on sample HTML
        var tool = new PageStructureTool();
        string html = "<h1>Title</h1><p>This is a test paragraph.</p>";
        var result = tool.Run(html);
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result, Newtonsoft.Json.Formatting.Indented));
    }
}
