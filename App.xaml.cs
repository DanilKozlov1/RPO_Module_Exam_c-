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
                Log.Error($"Критическая ошибка инициализации: {ex}");

                Log.Information("Приложение MediTrack завершает свою работу.");
                Shutdown(1);
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Log.Information("Приложение MediTrack завершает свою работу.");
            base.OnExit(e);
        }
    }
}