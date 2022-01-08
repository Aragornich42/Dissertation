using System.Collections.Generic;
using FluentMigrator;

namespace DBCreator.TablesFilling
{
    [Migration(20220104135510)]
    public class Filling : Migration
    {
        public override void Up()
        {
            var scripts = new List<string>();

            // Словари
            scripts.Add(new CountryFilling().GetSql());
            scripts.Add(new LanguageFilling().GetSql());
            scripts.Add(new LanguageCompetenceFilling().GetSql());
            scripts.Add(new ProfessionFilling().GetSql());

            // Сотрудник и сведения о нем
            scripts.AddRange(new EmployeeFilling().GetSqlArr());
            scripts.AddRange(new EducationFilling().GetSqlArr());
            scripts.AddRange(new ForeignPassportFilling().GetSqlArr());
            scripts.AddRange(new LanguageKnowledgeFilling().GetSqlArr());
            scripts.AddRange(new ProfessionLinkFilling().GetSqlArr());
            scripts.AddRange(new RelativeFilling().GetSqlArr());

            foreach (var script in scripts)
                Execute.Sql(script);
        }

        public override void Down()
        {
            
        }
    }
}
