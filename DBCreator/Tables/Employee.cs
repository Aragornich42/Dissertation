using FluentMigrator;

namespace DBCreator.Tables
{
    [Migration(20220104135500)]
    public class Employee : Migration
    {
        public override void Up()
        {
            Create.Table("Employee").InSchema("public")
                .WithColumn("Id").AsInt64().PrimaryKey().NotNullable().Unique().Identity().WithColumnDescription("Табельный номер")
                .WithColumn("INN").AsFixedLengthString(12).Nullable().WithColumnDescription("ИНН")
                .WithColumn("SNILS").AsFixedLengthString(14).Nullable().WithColumnDescription("СНИЛС")
                .WithColumn("Position").AsFixedLengthString(256).NotNullable().WithColumnDescription("Должность")
                .WithColumn("Surname").AsFixedLengthString(128).NotNullable().WithColumnDescription("Фамилия")
                .WithColumn("Name").AsFixedLengthString(64).NotNullable().WithColumnDescription("Имя")
                .WithColumn("Patronymic").AsFixedLengthString(128).Nullable().WithColumnDescription("Отчество")
                .WithColumn("Sex").AsFixedLengthString(1).NotNullable().WithColumnDescription("Пол")
                .WithColumn("Bithday").AsDate().NotNullable().WithColumnDescription("Дата рождения")
                .WithColumn("Nationality").AsFixedLengthString(64).NotNullable().WithColumnDescription("Гражданство")
                .WithColumn("RegAddress").AsFixedLengthString(1024).NotNullable().WithColumnDescription("Адрес регистрации")
                .WithColumn("FactAddress").AsFixedLengthString(1024).Nullable().WithColumnDescription("Фактический адрес")
                .WithColumn("InMarriage").AsFixedLengthString(128).NotNullable().WithColumnDescription("Состояние в браке")
                .WithColumn("Phone").AsFixedLengthString(16).NotNullable().WithColumnDescription("Телефон")
                .WithColumn("Additional").AsFixedLengthString(8192).Nullable().WithColumnDescription("Доп. сведения");
        }

        public override void Down()
        {
            Delete.Table("Employee").InSchema("public");
        }
    }
}
