namespace HoverDragon;

public static class ConsoleCommands
{
    public static void Echo(params string[] s)
    {
        ErrorMessage.AddMessage(string.Join(" ", s));
    }
}