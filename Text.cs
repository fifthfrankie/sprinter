public static class Text
{
    // How wide the border line is
    private const int BORDER_LENGTH = 31;
    private const char BORDER_CHAR = '=';

    // Method to show a title with borders
    public static void WriteTitle(string title)
    {
        // Make a line of equals signs
        string border = new string(BORDER_CHAR, BORDER_LENGTH);
        // Work out how many spaces to add to centre the text
        string padding = new string(' ', (BORDER_LENGTH - title.Length) / 2);

        // Show the border line at the top
        Console.WriteLine(border);
        // Show the title with spaces in front
        Console.WriteLine(padding + title);
        // Show the border line at the bottom
        Console.WriteLine(border);
    }

    // Method to show text in a box
    public static void WriteBox(params string[] lines)
    {
        // Go through each line of text one by one
        foreach (string line in lines)
        {
            // Show the line with a little space at the start
            Console.WriteLine($"  {line}");
        }
        // Show a border line at the end
        Console.WriteLine(new string(BORDER_CHAR, BORDER_LENGTH));
    }
}
