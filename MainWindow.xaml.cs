using Serilog;
using System.Windows;

namespace MediTrack
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var dbService = new DatabaseService();
            var data = dbService.GetMedications();
            
            this.DataContext = data;
            
            if (data.Rows.Count > 0)
                Log.Information($"Успешно загружено {data.Rows.Count} записей из БД.");
        }
    }
}