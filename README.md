# Opal Page Structure Analyzer Tool

This repository provides an Opal-compatible tool for analyzing the structure of HTML pages. The tool is designed to be registered and used within an Opal CMS environment, following patterns similar to the official Opal SDK and sample tools.

## Features
- Counts total words in the HTML content
- Counts the number of header tags (`<h1>`–`<h6>`) 
- Counts the number of paragraphs (`<p>` tags)
- Calculates the average number of words per paragraph

## Usage

### 1. Register the Tool
The tool can be registered using the provided `OpalToolRegistry`:

```csharp
OpalToolRegistry.Register(new PageStructureTool());
```

### 2. Run the Tool
You can run the tool directly or via the registry:

```csharp
var tool = new PageStructureTool();
string html = "<h1>Title</h1><p>This is a test paragraph.</p>";
var result = tool.Run(html);
Console.WriteLine($"Words: {result.WordCount}");
Console.WriteLine($"Headers: {result.HeaderCount}");
Console.WriteLine($"Paragraphs: {result.ParagraphCount}");
Console.WriteLine($"Avg words/paragraph: {result.AverageWordsPerParagraph:F2}");
```

### 3. Example Output
```
Words: 6
Headers: 1
Paragraphs: 1
Avg words/paragraph: 6.00
```

## Project Structure
- `OpalPageStructureAnalyzer.cs`: Core logic for analyzing HTML structure
- `IOpalTool.cs`: Interface for Opal tools
- `PageStructureTool.cs`: Tool implementation for Opal
- `OpalToolRegistry.cs`: Simple registry for registering tools
- `Program.cs`: Example entry point

## Integration
This tool is designed to be easily integrated into Opal CMS or other .NET-based systems that support plugin or tool registration.

## License
MIT
