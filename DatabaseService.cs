using Npgsql;
using System;
using System.Data;
using Serilog;
using DotNetEnv;

namespace MediTrack
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService()
        {
            // Чтение из .env (если пакет подключен)
            var host = Env.GetString("DB_HOST", "localhost");
            var port = Env.GetString("DB_PORT", "5432");
            var db = Env.GetString("DB_NAME", "meditrack_db");
            var user = Env.GetString("DB_USER", "postgres");
            var pass = Env.GetString("DB_PASSWORD", "");

            _connectionString = $"Host={host};Port={port};Database={db};Username={user};Password={pass};";
        }

        public DataTable GetMedications()
        {
            var table = new DataTable();
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                conn.Open();
                Log.Information("Успешное подключение к базе данных PostgreSQL.");
                
                using var cmd = new NpgsqlCommand("SELECT * FROM medications", conn);
                using var adapter = new NpgsqlDataAdapter(cmd);
                adapter.Fill(table);
            }
            catch (Exception ex)
            {
                
                System.Windows.MessageBox.Show("Произошла ошибка при загрузке данных.");
            }
            return table;
        }
    }
}