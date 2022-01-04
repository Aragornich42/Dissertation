using FluentMigrator;

namespace DBCreator.Tables
{
    [Migration(20220104135500)]
    public class LanguageKnowledge : Migration
    {
        public override void Up()
        {
            Create.Table("LanguageKnowledge").InSchema("public")
                .WithColumn("Id").AsInt64().PrimaryKey().NotNullable().Unique().Indexed().WithColumnDescription("ID знания иностранного языка")
                .WithColumn("EmployeeId").AsInt64().ForeignKey("Employee", "Id").NotNullable().WithColumnDescription("Табельный номер")
                .WithColumn("LanguageId").AsInt32().ForeignKey("Language", "Id").NotNullable().WithColumnDescription("ID языка")
                .WithColumn("LanguageCompId").AsInt32().ForeignKey("LanguageCompetence", "Id").NotNullable().WithColumnDescription("ID уровня знания языка");
        }

        public override void Down()
        {
            Delete.Table("LanguageKnowledge").InSchema("public");
        }
    }
}
