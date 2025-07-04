using System.Text;
using backend.Exceptions;
using backend.Interfaces;

namespace backend.Helpers
{
    public class FileLoggerHelper(IConfiguration config, IHostEnvironment env) : IFileLoggerHelper
    {
        private readonly IConfiguration _config = config;
        private readonly IHostEnvironment _env = env;
        public async void LogEmailError(string logMessage)
        {
            var fileName = _config["Logs:Email"];

            if (string.IsNullOrEmpty(fileName))
            {
                throw new InvalidConfiguratioException("Logs:Email");
            }

            string logDirectory = Path.Combine(_env.ContentRootPath, "Logs");

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            var logPath = Path.Combine(logDirectory, fileName);

            using (FileStream fsOut = File.Open(logPath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(fsOut, Encoding.UTF8))
                {
                    await writer.WriteLineAsync(logMessage);
                }
            }
        }

        public async void LogError(string logMessage)
        {
            var fileName = _config["Logs:Exception"];

            if (string.IsNullOrEmpty(fileName))
            {
                throw new InvalidConfiguratioException("Logs:Exception");
            }

            string logDirectory = Path.Combine(_env.ContentRootPath, "Logs");

            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            var logPath = Path.Combine(logDirectory, fileName);

            using (FileStream fsOut = File.Open(logPath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter writer = new StreamWriter(fsOut, Encoding.UTF8))
                {
                    await writer.WriteLineAsync(logMessage);
                }
            }
        }
    }
}