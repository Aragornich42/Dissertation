using FluentMigrator;

namespace DBCreator.Tables
{
    [Migration(20220104135500)]
    public class Relative : Migration
    {
        public override void Up()
        {
            Create.Table("Relative").InSchema("public")
                .WithColumn("Id").AsInt64().PrimaryKey().NotNullable().Unique().Indexed().WithColumnDescription("ID родственника")
                .WithColumn("EmployeeId").AsInt64().ForeignKey("Employee", "Id").NotNullable().WithColumnDescription("Табельный номер")
                .WithColumn("FIO").AsFixedLengthString(256).Nullable().WithColumnDescription("ФИО родственника")
                .WithColumn("DegreeOfKinship").AsFixedLengthString(32).Nullable().WithColumnDescription("Степень родства")
                .WithColumn("Phone").AsFixedLengthString(16).WithColumnDescription("Телефон");
        }

        public override void Down()
        {
            Delete.Table("Relative").InSchema("public");
        }
    }
}
