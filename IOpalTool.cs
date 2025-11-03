public interface IOpalTool
{
    string Name { get; }
    string Description { get; }
    object Run(string input);
}
