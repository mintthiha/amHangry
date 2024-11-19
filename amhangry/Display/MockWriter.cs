public class MockWriter : IConsole
{

  public List<string> Outputs = new List<string>();

  public string this[int index]
  {
    get { return Outputs[index]; }
    set { Outputs[index] = value; }
  }
  public void WriteLine(string? message)
  {
    Console.WriteLine(message);
    if (message != null)
    {
      Outputs.Add(message);
    }
    else
    {
      throw new ArgumentNullException("The message passed in was null!");
    }
  }

}