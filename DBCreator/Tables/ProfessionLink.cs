using FluentMigrator;

namespace DBCreator.Tables
{
    [Migration(20220104135500)]
    public class ProfessionLink : Migration
    {
        public override void Up()
        {
            Create.Table("ProfessionLink").InSchema("public")
                .WithColumn("Id").AsInt64().PrimaryKey().NotNullable().Unique().Indexed().WithColumnDescription("ID связки сотрудник-профессия")
                .WithColumn("EmployeeId").AsInt64().ForeignKey("Employee", "Id").NotNullable().WithColumnDescription("Табельный номер")
                .WithColumn("ProfessionId").AsInt32().ForeignKey("Profession", "Id").NotNullable().WithColumnDescription("ID профессии")
                .WithColumn("IsMain").AsBoolean().NotNullable().WithColumnDescription("Основная?");
        }

        public override void Down()
        {
            Delete.Table("ProfessionLink").InSchema("public");
        }
    }
}