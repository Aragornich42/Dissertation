using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Postgres;
using DBCreator.Tables;

namespace DBCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = CreateServices();

            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }
        }

        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres()
                    .WithGlobalConnectionString("Host=localhost;Port=5432;Database=postgres;Username=postgres")
                    .ScanIn(typeof(Employee).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            runner.MigrateUp();
        }
    }
}
