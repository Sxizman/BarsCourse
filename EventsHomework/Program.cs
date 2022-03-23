public class InputHandler
{
    public event EventHandler<char> OnKeyPressed;

    public void Run()
    {
        char key = Console.ReadKey().KeyChar;
        while (char.ToLower(key) != 'c')
        {
            OnKeyPressed?.Invoke(this, key);
            key = Console.ReadKey().KeyChar;
        }
    }
}

class Program
{
    static void Main()
    {
        InputHandler sample = new InputHandler();
        sample.OnKeyPressed +=
            (sender, key) => Console.Write(key);
        sample.Run();
    }
}