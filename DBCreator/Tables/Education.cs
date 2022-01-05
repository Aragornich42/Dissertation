using FluentMigrator;

namespace DBCreator.Tables
{
    [Migration(20220104135503)]
    public class Education : Migration
    {
        public override void Up()
        {
            Create.Table("Education").InSchema("public")
                .WithColumn("Id").AsInt64().PrimaryKey().NotNullable().Unique().Identity().Indexed().WithColumnDescription("ID образования")
                .WithColumn("EmployeeId").AsInt64().NotNullable().WithColumnDescription("Табельный номер")      //foreign
                .WithColumn("DiplomaId").AsInt64().NotNullable().WithColumnDescription("Табельный номер")      //foreign
                .WithColumn("EducationLevel").AsFixedLengthString(32).NotNullable().WithColumnDescription("Уровень образования");
        }

        public override void Down()
        {
            Delete.Table("Education").InSchema("public");
        }
    }
}
