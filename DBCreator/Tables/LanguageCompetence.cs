using FluentMigrator;

namespace DBCreator.Tables
{
    [Migration(20220104135500)]
    public class LanguageCompetence : Migration
    {
        public override void Up()
        {
            Create.Table("LanguageCompetence").InSchema("public")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Unique().Indexed().WithColumnDescription("ID уровня знания языка")
                .WithColumn("Name").AsFixedLengthString(32).NotNullable().WithColumnDescription("Уровень знания языка");
        }

        public override void Down()
        {
            Delete.Table("LanguageCompetence").InSchema("public");
        }
    }
}
