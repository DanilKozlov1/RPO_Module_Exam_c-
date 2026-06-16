using Serilog;
using System;
using System.IO;

namespace MediTrack
{
    public static class LoggerConfig
    {
        private static readonly string OUTPUT_TEMPLATE
                = "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level:u3}] [{SourceContext}] - {Message:lj}{NewLine}{Exception}";

        private static readonly RollingInterval ROLLING_INTERVAL = RollingInterval.Day;

        private static readonly string LOGS_DIRECTORY = "logs";
        
        private static readonly string LOGFILE_NAME = "app_.log";
        
        private static readonly int LOGFILE_MAX_SIZE = 10 * 1024 * 1024;
        
        private static readonly int LOGFILE_COUNT_LIMIT = 5;


        public static void Init()
        {
            string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LOGS_DIRECTORY, LOGFILE_NAME);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File(
                    path: logFilePath,
                    rollingInterval: ROLLING_INTERVAL,
                    fileSizeLimitBytes: LOGFILE_MAX_SIZE,
                    rollOnFileSizeLimit: true,
                    retainedFileCountLimit: LOGFILE_COUNT_LIMIT,
                    outputTemplate: OUTPUT_TEMPLATE,
                    shared: true
                )
                .CreateLogger();
                
            Log.ForContext("SourceContext", "LoggerConfig")
                .Information("Логгер инициализирован.");
        }
    }
}