using System;
using System.IO;
using System.Text;

namespace CoreLibrary.Utilities
{
    public static class LogService
    {
        private static readonly object _lock = new();
        private static readonly string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");

        /// <summary>
        /// Logs an informational message.
        /// </summary>
        public static void Info(string message) => WriteLog("INFO", message);

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        public static void Warning(string message) => WriteLog("WARNING", message);

        /// <summary>
        /// Logs an error message, including exception details if provided.
        /// </summary>
        public static void Error(string message, Exception? ex = null)
        {
            var fullMessage = ex != null ? $"{message} | Exception: {ex.Message}" : message;
            WriteLog("ERROR", fullMessage);
        }

        /// <summary>
        /// Logs a debug message, useful for development and deeper analysis.
        /// </summary>
        public static void Debug(string message) => WriteLog("DEBUG", message);

        /// <summary>
        /// Logs a critical error message that requires immediate attention.
        /// </summary>
        public static void Critical(string message) => WriteLog("CRITICAL", message);

        private static void WriteLog(string level, string message)
        {
            lock (_lock)
            {
                string logLine = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} [{level}] {message}";
                //File.AppendAllText(logFilePath, logLine + Environment.NewLine, Encoding.UTF8);
                FileHelper.SaveToFile(logFilePath, logLine + Environment.NewLine);
            }
        }
    }
}