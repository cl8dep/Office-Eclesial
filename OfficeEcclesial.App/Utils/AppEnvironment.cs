using Microsoft.EntityFrameworkCore;

using OfficeEcclesial.App.Database;

using System;
using System.IO;

namespace OfficeEcclesial.App.Utils
{
    public class AppEnvironment
    {
        private const string ApplicationFolderName ="Office Eclesial";
        private const string MainDatabaseName = "MainDatabase.db";

        public static string WorkspaceFolder => ResolveWorkspaceFolder();
        public static string MainDatabasePath => ResolveMainDatabasePath();
        public static string MainDatabaseConnectionString => $"Data Source=\"{MainDatabasePath}\"";

        private static string ResolveMainDatabasePath()
        {
            return Path.Combine(ResolveWorkspaceFolder(), MainDatabaseName);
        }

        private static string ResolveWorkspaceFolder()
        {
            var applicationsFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(applicationsFolder, ApplicationFolderName);
        }

        public static bool DatabaseExist()
        {
            return File.Exists(MainDatabasePath);
        }

        public static void CreateEmptyDatabase()
        {
            if (!Directory.Exists(WorkspaceFolder))
                Directory.CreateDirectory(WorkspaceFolder);
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlite(MainDatabaseConnectionString);
            var db = new MainDatabase(builder.Options);

            db.Database.EnsureCreated();

        }
    }
}
