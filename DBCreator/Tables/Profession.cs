using FluentMigrator;

namespace DBCreator.Tables
{
    [Migration(20220104135507)]
    public class Profession : Migration
    {
        public override void Up()
        {
            Create.Table("Profession").InSchema("public")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Unique().Identity().WithColumnDescription("ID профессии")
                .WithColumn("Name").AsFixedLengthString(256).NotNullable().WithColumnDescription("Название профессии");
        }

        public override void Down()
        {
            Delete.Table("Profession").InSchema("public");
        }
    }
}
