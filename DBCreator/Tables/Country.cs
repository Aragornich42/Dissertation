using FluentMigrator;

namespace DBCreator.Tables
{
    [Migration(20220104135501)]
    public class Country : Migration
    {
        public override void Up()
        {
            Create.Table("Country").InSchema("public")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Unique().Identity().Indexed().WithColumnDescription("ID страны")
                .WithColumn("Name").AsFixedLengthString(64).NotNullable().WithColumnDescription("Название страны");
        }

        public override void Down()
        {
            Delete.Table("Country").InSchema("public");
        }
    }
}
