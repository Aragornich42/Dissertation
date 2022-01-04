using FluentMigrator;

namespace DBCreator.Tables
{
    [Migration(20220104135500)]
    public class Profession : Migration
    {
        public override void Up()
        {
            Create.Table("Profession").InSchema("public")
                .WithColumn("Id").AsInt32().PrimaryKey().NotNullable().Unique().Indexed().WithColumnDescription("ID профессии")
                .WithColumn("Name").AsFixedLengthString(64).NotNullable().WithColumnDescription("Название профессии");
        }

        public override void Down()
        {
            Delete.Table("Profession").InSchema("public");
        }
    }
}
