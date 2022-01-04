using FluentMigrator;

namespace DBCreator.Tables
{
    [Migration(20220104135500)]
    public class Education : Migration
    {
        public override void Up()
        {
            Create.Table("Education").InSchema("public")
                .WithColumn("Id").AsInt64().PrimaryKey().NotNullable().Unique().Indexed().WithColumnDescription("ID образования")
                .WithColumn("EmployeeId").AsInt64().ForeignKey("Employee", "Id").NotNullable().WithColumnDescription("Табельный номер")
                .WithColumn("DiplomaId").AsInt64().ForeignKey("Diploma", "Id").NotNullable().WithColumnDescription("Табельный номер")
                .WithColumn("EducationLevel").AsFixedLengthString(32).NotNullable().WithColumnDescription("Уровень образования");
        }

        public override void Down()
        {
            Delete.Table("Education").InSchema("public");
        }
    }
}
