public interface ILogger
{
    public void LogInfo(string message);
    public void LogWarning(string message);
    public void LogError(string message, Exception ex);
}

public class LocalFileLogger<T> : ILogger
{
    private string _fileName;

    public LocalFileLogger(string fileName)
    {
        _fileName = fileName;
    }

    public void LogInfo(string message)
    {
        File.AppendAllText(_fileName, $"[Info] : [{typeof(T).Name}] : {message}\n");
    }

    public void LogWarning(string message)
    {
        File.AppendAllText(_fileName, $"[Warning] : [{typeof(T).Name}] : {message}\n");
    }

    public void LogError(string message, Exception ex)
    {
        File.AppendAllText(_fileName, $"[Error] : [{typeof(T).Name}] : {message}. {ex.Message}\n");
    }
}

class Program
{
    static void Main()
    {
        var loggerInt = new LocalFileLogger<int>("logInt.txt");
        loggerInt.LogInfo("info example");
        loggerInt.LogWarning("warning example");
        loggerInt.LogError("error example", new Exception("some exception"));

        var loggerStr = new LocalFileLogger<string>("logStr.txt");
        loggerStr.LogInfo("info example");
        loggerStr.LogWarning("warning example");
        loggerStr.LogError("error example", new Exception("some exception"));

        var logger = new LocalFileLogger<ILogger>("log.txt");
        logger.LogInfo("info example");
        logger.LogWarning("warning example");
        logger.LogError("error example", new Exception("some exception"));
    }
}