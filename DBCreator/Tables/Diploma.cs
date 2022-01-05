using FluentMigrator;

namespace DBCreator.Tables
{
    [Migration(20220104135502)]
    public class Diploma : Migration
    {
        public override void Up()
        {
            Create.Table("Diploma").InSchema("public")
                .WithColumn("Id").AsInt64().PrimaryKey().NotNullable().Unique().Identity().Indexed().WithColumnDescription("ID диплома")
                .WithColumn("Seria").AsFixedLengthString(32).NotNullable().WithColumnDescription("Серия диплома")
                .WithColumn("Number").AsFixedLengthString(32).Nullable().WithColumnDescription("Номер диплома")
                .WithColumn("EducatInstitName").AsFixedLengthString(128).NotNullable().WithColumnDescription("Название учебного заведения")
                .WithColumn("EducatInstitAddress").AsFixedLengthString(1024).NotNullable().WithColumnDescription("Адрес учебного заведения")
                .WithColumn("YearOfGrad").AsInt32().NotNullable().WithColumnDescription("Год окончания учебного заведения")
                .WithColumn("Qualification").AsFixedLengthString(64).NotNullable().WithColumnDescription("Квалификация")
                .WithColumn("Course").AsFixedLengthString(64).Nullable().WithColumnDescription("Направление обучения");
        }

        public override void Down()
        {
            Delete.Table("Diploma").InSchema("public");
        }
    }
}