namespace backend.Interfaces
{
    public interface IFileLoggerHelper
    {
        void LogEmailError(string logMessage);
        void LogError(string logMessage);
    }
}