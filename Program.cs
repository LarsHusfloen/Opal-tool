using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Health check endpoint
app.MapGet("/", () => Results.Ok(new { status = "Opal Page Structure Analyzer is running." }))

// Discovery endpoint for Opal (recommended manifest structure)
.MapGet("/discovery", () => Results.Ok(new {
    name = "Opal Page Structure Analyzer",
    description = "Analyzes the structure of a web page (word count, headers, paragraphs, etc).",
    functions = new[]
    {
        new {
            name = "analyze",
            description = "Analyzes a web page's structure.",
            method = "POST",
            path = "/analyze",
            parameters = new[]
            {
                new {
                    name = "url",
                    type = "string",
                    description = "The URL of the page to analyze.",
                    required = true
                }
            },
            response = new[]
            {
                new { name = "wordCount", type = "int", description = "Total word count" },
                new { name = "headerCount", type = "int", description = "Number of headers" },
                new { name = "paragraphCount", type = "int", description = "Number of paragraphs" },
                new { name = "averageWordsPerParagraph", type = "double", description = "Average words per paragraph" }
            }
        }
    }
}))

.MapPost("/analyze", async (HttpContext context) =>
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
