using System.Collections.Generic;

public static class OpalToolRegistry
{
    private static readonly List<IOpalTool> Tools = new List<IOpalTool>();

    public static void Register(IOpalTool tool)
    {
        Tools.Add(tool);
    }

    public static IEnumerable<IOpalTool> GetTools() => Tools;
}
