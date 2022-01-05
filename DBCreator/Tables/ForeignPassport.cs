using FluentMigrator;

namespace DBCreator.Tables
{
    [Migration(20220104135507)]
    public class ForeignPassport : Migration
    {
        public override void Up()
        {
            Create.Table("ForeignPassport").InSchema("public")
                .WithColumn("Id").AsInt64().PrimaryKey().NotNullable().Unique().Identity().Indexed().WithColumnDescription("ID иностранного паспорта")
                .WithColumn("EmployeeId").AsInt64().NotNullable().WithColumnDescription("Табельный номер")      //foreign
                .WithColumn("CountryId").AsInt32().NotNullable().WithColumnDescription("ID страны")      //foreign
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
