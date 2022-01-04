using FluentMigrator;

namespace DBCreator.Tables
{
    [Migration(20220104135500)]
    public class ForeignPassport : Migration
    {
        public override void Up()
        {
            Create.Table("ForeignPassport").InSchema("public")
                .WithColumn("Id").AsInt64().PrimaryKey().NotNullable().Unique().Indexed().WithColumnDescription("ID иностранного паспорта")
                .WithColumn("EmployeeId").AsInt64().ForeignKey("Employee", "Id").NotNullable().WithColumnDescription("Табельный номер")
                .WithColumn("CountryId").AsInt32().ForeignKey("Country", "Id").NotNullable().WithColumnDescription("ID страны")
                .WithColumn("Number").AsFixedLengthString(64).NotNullable().WithColumnDescription("Серия и номер")
                .WithColumn("StartDate").AsDate().Nullable().WithColumnDescription("Дата начала действия")
                .WithColumn("FinishDate").AsDate().Nullable().WithColumnDescription("Дата окончания действия");
        }

        public override void Down()
        {
            Delete.Table("ForeignPassport").InSchema("public");
        }
    }
}
