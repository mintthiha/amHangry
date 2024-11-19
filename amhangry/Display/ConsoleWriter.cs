
public class ConsoleWriter : IConsole
{
    /// <summary>
    /// This method prints to the console, no restrictions. Acts like Console.WriteLine
    /// </summary>
    /// <param name="message"> Message to print to the console</param>
    public void WriteLine(string? message)
    {
        Console.WriteLine(message);
    }

    public static void DisplayOptions(List<string> options)
    {
        for (int i = 0; i < options.Count; i++)
        {
            Console.WriteLine("{" + i + "} " + options[i]);
        }
        Console.WriteLine("Please choose one of the options!");
    }

    /*
    * This method will let the user enter key they wish to continue the program, and then the program
    * will clear the console.
    */
    public static void ClearWithEnter()
    {
        Console.WriteLine("Press any key to continue!");
        Console.ReadLine();
        Console.Clear();
    }
}