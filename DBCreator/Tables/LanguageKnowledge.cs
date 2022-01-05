using FluentMigrator;

namespace DBCreator.Tables
{
    [Migration(20220104135506)]
    public class LanguageKnowledge : Migration
    {
        public override void Up()
        {
            Create.Table("LanguageKnowledge").InSchema("public")
                .WithColumn("Id").AsInt64().PrimaryKey().NotNullable().Unique().Identity().Indexed().WithColumnDescription("ID знания иностранного языка")
                .WithColumn("EmployeeId").AsInt64().NotNullable().WithColumnDescription("Табельный номер")      //foreign
                .WithColumn("LanguageId").AsInt32().NotNullable().WithColumnDescription("ID языка")      //foreign
                .WithColumn("LanguageCompId").AsInt32().NotNullable().WithColumnDescription("ID уровня знания языка");      //foreign
        }

        public override void Down()
        {
            Delete.Table("LanguageKnowledge").InSchema("public");
        }
    }
}
