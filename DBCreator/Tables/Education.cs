using FluentMigrator;

namespace DBCreator.Tables
{
    [Migration(20220104135502)]
    public class Education : Migration
    {
        public override void Up()
        {
            Create.Table("Education").InSchema("public")
                .WithColumn("Id").AsInt64().PrimaryKey().NotNullable().Unique().Identity().WithColumnDescription("ID образования")
                .WithColumn("EmployeeId").AsInt64().NotNullable().WithColumnDescription("Табельный номер")      //foreign
                .WithColumn("EducationLevel").AsFixedLengthString(128).NotNullable().WithColumnDescription("Уровень образования")
                .WithColumn("DipSeria").AsFixedLengthString(32).NotNullable().WithColumnDescription("Серия диплома")
                .WithColumn("DipNumber").AsFixedLengthString(32).Nullable().WithColumnDescription("Номер диплома")
                .WithColumn("EducatInstitName").AsFixedLengthString(1024).NotNullable().WithColumnDescription("Название учебного заведения")
                .WithColumn("EducatInstitAddress").AsFixedLengthString(1024).NotNullable().WithColumnDescription("Адрес учебного заведения")
                .WithColumn("YearOfGrad").AsInt32().NotNullable().WithColumnDescription("Год окончания учебного заведения")
                .WithColumn("Qualification").AsFixedLengthString(256).Nullable().WithColumnDescription("Квалификация");
        }

        public override void Down()
        {
            Delete.Table("Education").InSchema("public");
        }
    }
}
