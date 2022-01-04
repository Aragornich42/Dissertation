﻿using System;
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
                    .WithGlobalConnectionString("postgresql://postgres@localhost:5432/postgres")
                    .ScanIn(typeof(Employee).Assembly, 
                            typeof(Country).Assembly, 
                            typeof(Language).Assembly, 
                            typeof(LanguageCompetence).Assembly, 
                            typeof(Profession).Assembly,
                            typeof(Diploma).Assembly,
                            typeof(Education).Assembly,
                            typeof(ForeignPassport).Assembly,
                            typeof(LanguageKnowledge).Assembly,
                            typeof(ProfessionLink).Assembly,
                            typeof(Relative).Assembly)
                    .For.Migrations())
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
