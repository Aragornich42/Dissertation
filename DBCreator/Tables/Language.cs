using FluentMigrator;

namespace DBCreator.Tables
{
    [Migration(20220104135504)]
    public class Language : Migration
    {
        public override void Up()
        {
            Create.Table("Language").InSchema("public")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Unique().Identity().Indexed().WithColumnDescription("ID языка")
                .WithColumn("Name").AsFixedLengthString(32).NotNullable().WithColumnDescription("Название языка");
        }

        public override void Down()
        {
            Delete.Table("Language").InSchema("public");
        }
    }
}