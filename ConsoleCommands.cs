namespace HoverDragon;

public static class ConsoleCommands
{
    public static void Echo(params string[] s)
    {
        foreach (var line in s)
        {
            ErrorMessage.AddMessage(line);
        }
    }
}