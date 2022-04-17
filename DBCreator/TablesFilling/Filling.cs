using System;
using System.Collections.Generic;
using FluentMigrator;

namespace DBCreator.TablesFilling
{
    [Migration(20220104135510)]
    public class Filling : Migration
    {
        public override void Up()
        {
            Console.WriteLine("Начинаем заполнение БД данными! Это долгий процесс, имейте терпение...");
            var scripts = new List<string>();

            // Словари
            scripts.Add(new CountryFilling().GetSql());
            Console.WriteLine("Создан скрипт для заполнения Country");

            scripts.Add(new LanguageFilling().GetSql());
            Console.WriteLine("Создан скрипт для заполнения Language");

            scripts.Add(new LanguageCompetenceFilling().GetSql());
            Console.WriteLine("Создан скрипт для заполнения LanguageCompetence");

            scripts.Add(new ProfessionFilling().GetSql());
            Console.WriteLine("Создан скрипт для заполнения Profession");

            Console.WriteLine("Заполняем словари...");
            foreach (var script in scripts)
            {
                Execute.Sql(script);
            }
            Console.WriteLine("Словари заполнены!");
            scripts.Clear();

            Console.WriteLine();

            // Сотрудник и сведения о нем
            scripts.AddRange(new EmployeeFilling().GetSqlArr());
            Console.WriteLine("Созданы скрипты для заполнения Employee");
            Console.WriteLine("Начинаем заполнять таблицу...");
            foreach (var script in scripts)
            {
                Execute.Sql(script);
            }
            Console.WriteLine("Таблица заполнена!");
            scripts.Clear();

            scripts.AddRange(new EducationFilling().GetSqlArr());
            Console.WriteLine("Созданы скрипты для заполнения Education");
            Console.WriteLine("Начинаем заполнять таблицу...");
            foreach (var script in scripts)
            {
                Execute.Sql(script);
            }
            Console.WriteLine("Таблица заполнена!");
            scripts.Clear();

            scripts.AddRange(new ForeignPassportFilling().GetSqlArr());
            Console.WriteLine("Созданы скрипты для заполнения ForeignPassport");
            Console.WriteLine("Начинаем заполнять таблицу...");
            foreach (var script in scripts)
            {
                Execute.Sql(script);
            }
            Console.WriteLine("Таблица заполнена!");
            scripts.Clear();

            scripts.AddRange(new LanguageKnowledgeFilling().GetSqlArr());
            Console.WriteLine("Созданы скрипты для заполнения LanguageKnowledge");
            Console.WriteLine("Начинаем заполнять таблицу...");
            foreach (var script in scripts)
            {
                Execute.Sql(script);
            }
            Console.WriteLine("Таблица заполнена!");
            scripts.Clear();

            scripts.AddRange(new ProfessionLinkFilling().GetSqlArr());
            Console.WriteLine("Созданы скрипты для заполнения ProfessionLink");
            Console.WriteLine("Начинаем заполнять таблицу...");
            foreach (var script in scripts)
            {
                Execute.Sql(script);
            }
            Console.WriteLine("Таблица заполнена!");
            scripts.Clear();

            scripts.AddRange(new RelativeFilling().GetSqlArr());
            Console.WriteLine("Созданы скрипты для заполнения Relative");
            Console.WriteLine("Начинаем заполнять таблицу...");
            foreach (var script in scripts)
            {
                Execute.Sql(script);
            }
            Console.WriteLine("Таблица заполнена!");
            scripts.Clear();
        }

        public override void Down()
        {
            
        }
    }
}
