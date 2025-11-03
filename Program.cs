using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Health check or discovery endpoint
app.MapGet("/", () => Results.Ok(new { status = "Opal Page Structure Analyzer is running." }));

// Discovery endpoint for Opal (object with 'functions' property)
app.MapGet("/discovery", () => Results.Ok(new {
    functions = new[]
    {
        new {
            name = "analyze",
            method = "POST",
            path = "/analyze",
            description = "Analyzes a web page's structure. Expects JSON: { url: string }.",
            request = new { url = "string (required)" },
            response = new {
                wordCount = "int",
                headerCount = "int",
                paragraphCount = "int",
                averageWordsPerParagraph = "double"
            }
        }
    }
}));

app.MapPost("/analyze", async (HttpContext context) =>
{
    try
    {
        var form = await context.Request.ReadFromJsonAsync<AnalyzeRequest>();
        if (form == null || string.IsNullOrWhiteSpace(form.Url))
            return Results.BadRequest(new { error = "Missing or invalid URL." });

        using var httpClient = new HttpClient();
        var html = await httpClient.GetStringAsync(form.Url);

        var tool = new PageStructureTool();
        var result = tool.Run(html);
        return Results.Ok(result);
    }
    catch (HttpRequestException ex)
    {
        return Results.BadRequest(new { error = $"Failed to fetch URL: {ex.Message}" });
    }
    catch (System.Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.Run();

public class AnalyzeRequest
{
    public string Url { get; set; }
}
