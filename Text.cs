public static class Text
{
    private const int BORDER_LENGTH = 31;
    private const char BORDER_CHAR = '=';

    public static void WriteTitle(string title)
    {
        string border = new string(BORDER_CHAR, BORDER_LENGTH);
        string padding = new string(' ', (BORDER_LENGTH - title.Length) / 2);

        Console.WriteLine(border);
        Console.WriteLine(padding + title);
        Console.WriteLine(border);
    }

    public static void WriteBox(params string[] lines)
    {
        foreach (string line in lines)
        {
            Console.WriteLine($"  {line}");
        }
        Console.WriteLine(new string(BORDER_CHAR, BORDER_LENGTH));
    }
}
