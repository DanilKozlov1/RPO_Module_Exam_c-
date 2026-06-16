using System.Windows;
using Serilog;

namespace MediTrack
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            LoggerConfig.Init();

            try
            {
                Log.Information("Приложение MediTrack запущено.");
            }
            catch (System.Exception ex)
            { 
                System.Console.WriteLine($"Критическая ошибка инициализации: {ex}");
                Shutdown(1);
            }
        }
    }
}