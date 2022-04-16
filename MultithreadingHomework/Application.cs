namespace MultithreadingHomework;

public class Application
{
    private IRequestHandler _handler;
    private object _consoleLock = 0;

    public Application()
    {
        _handler = new DummyRequestHandler();
    }

    public void Run()
    {
        Console.WriteLine("Приложение запущено.");
        MainCycle();
        Console.WriteLine("Приложение завершило работу. Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    private void MainCycle()
    {
        while (true)
        {
            Console.WriteLine("Введите текст запроса для отправки. Для выхода введите /exit");
            var request = ReadLine();
            if (request == "/exit") break;

            var args = ReadArgs();

            new Thread(() => SendRequest(request, args)).Start();
        }
    }

    private void SendRequest(string request, string[] args)
    {
        var uuid = Guid.NewGuid().ToString("D");
        WriteLine($"Было отправлено сообщение '{request}'. Присвоен идентификатор {uuid}", ConsoleColor.Green);

        try
        {
            var response = _handler.HandleRequest(request, args);
            WriteLine($"Сообщение с идентификатором {uuid} получило ответ - {response}", ConsoleColor.Green);
        }
        catch (Exception ex)
        {
            WriteLine($"Сообщение с идентификатором {uuid} упало с ошибкой - {ex.Message}", ConsoleColor.Red);
        }
    }

    private string[] ReadArgs()
    {
        var args = new List<string>();

        Console.WriteLine("Введите аргументы сообщения. Если аргументы не нужны - введите /end");

        while (true)
        {
            var arg = ReadLine();
            if (arg == "/end") break;

            args.Add(arg);

            Console.WriteLine("Введите следующий аргумент сообщения. Для окончания добавления аргументов введите /end");
        }

        return args.ToArray();
    }

    private string ReadLine()
    {
        lock (_consoleLock)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            var line = Console.ReadLine() ?? "";
            Console.ResetColor();

            return line;
        }
    }

    private void WriteLine(string line, ConsoleColor color)
    {
        lock (_consoleLock)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(line);
            Console.ResetColor();
        }
    }
}