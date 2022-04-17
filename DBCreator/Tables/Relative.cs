using FluentMigrator;

namespace DBCreator.Tables
{
    [Migration(20220104135509)]
    public class Relative : Migration
    {
        public override void Up()
        {
            Create.Table("Relative").InSchema("public")
                .WithColumn("Id").AsInt64().PrimaryKey().NotNullable().Unique().Identity().WithColumnDescription("ID родственника")
                .WithColumn("EmployeeId").AsInt64().NotNullable().WithColumnDescription("Табельный номер")      //foreign
                .WithColumn("FIO").AsFixedLengthString(512).Nullable().WithColumnDescription("ФИО родственника")
                .WithColumn("DegreeOfKinship").AsFixedLengthString(32).Nullable().WithColumnDescription("Степень родства")
                .WithColumn("Phone").AsFixedLengthString(16).WithColumnDescription("Телефон");
        }

        public override void Down()
        {
            Delete.Table("Relative").InSchema("public");
        }
    }
}
