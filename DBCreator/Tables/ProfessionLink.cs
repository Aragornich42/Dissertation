using FluentMigrator;

namespace DBCreator.Tables
{
    [Migration(20220104135508)]
    public class ProfessionLink : Migration
    {
        public override void Up()
        {
            Create.Table("ProfessionLink").InSchema("public")
                .WithColumn("Id").AsInt64().PrimaryKey().NotNullable().Unique().Identity().WithColumnDescription("ID связки сотрудник-профессия")
                .WithColumn("EmployeeId").AsInt64().NotNullable().WithColumnDescription("Табельный номер")      //foreign
                .WithColumn("ProfessionId").AsInt32().NotNullable().WithColumnDescription("ID профессии")      //foreign
                .WithColumn("IsMain").AsBoolean().NotNullable().WithColumnDescription("Основная?");
        }

        public override void Down()
        {
            Delete.Table("ProfessionLink").InSchema("public");
        }
    }
}