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
            _connectionString = LoadConnectionString();
        }


        private bool IsValideData(string data, string dataName)
        {
            if (string.IsNullOrEmpty(data))
            {
                Log.ForContext("SourceContext", "DatabaseService")
                    .Warning($"Файл .env не найден или {dataName} не найден.");
                return false;
            }

            return true;
        }

        private string LoadConnectionString()
        {
            Env.TraversePath().Load();

            // Чтение из .env (если пакет подключен)
            var host = Env.GetString("DB_HOST");
            IsValideData(host, "DB_HOST");

            var port = Env.GetInt("DB_PORT");
            if (port <= 0)
            {
                Log.ForContext("SourceContext", "DatabaseService")
                    .Warning($"Файл .env не найден или DB_PORT не найден.");
            }

            var db = Env.GetString("DB_NAME");
            IsValideData(host, "DB_HOST");

            var user = Env.GetString("DB_USER");
            IsValideData(host, "DB_HOST");

            var pass = Env.GetString("DB_PASSWORD");
            IsValideData(host, "DB_HOST");

            return $"Host={host};Port={port};Database={db};Username={user};Password={pass};";
        }

        public DataTable GetMedications()
        {
            var table = new DataTable();
            
            try
            {
                using var conn = new NpgsqlConnection(_connectionString);
                conn.Open();
                Log.ForContext("SourceContext", "DatabaseService")
                    .Information("Успешное подключение к базе данных PostgreSQL.");
                
                using var cmd = new NpgsqlCommand("SELECT * FROM medications", conn);
                using var adapter = new NpgsqlDataAdapter(cmd);
                adapter.Fill(table);
            }
            catch (Exception ex)
            {
                Log.ForContext("SourceContext", "DatabaseService")
                    .Error("Ошибка базы данных: {exMessage}", ex);
            }

            return table;
        }
    }
}