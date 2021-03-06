using System.Collections.Generic;
using System.Linq;
using System.Data;
using Npgsql;
using Dapper;
using Dto;

namespace Services
{
    internal class DataConnector
    {
        string _conStr;

        internal DataConnector()
        {
            _conStr = "Host=localhost;Port=5432;Database=postgres;Username=postgres";
        }

        internal List<Employee> GetEmployees()
        {
            using var con = new NpgsqlConnection(_conStr);
            return con.Query<Employee>("SELECT * FROM \"Employee\"").ToList();
        }

        internal Relative GetRelative(long employeeId)
        {
            using var con = new NpgsqlConnection(_conStr);
            return con.Query<Relative>("SELECT * FROM \"Relative\" WHERE \"EmployeeId\" = @employeeId", new { employeeId }).FirstOrDefault();
        }

        internal List<Profession> GetProfessions(long employeeId)
        {
            using var con = new NpgsqlConnection(_conStr);
            return con.Query<Profession>("SELECT p.\"Name\", pl.\"IsMain\" " +
                                              "FROM \"ProfessionLink\" pl INNER JOIN \"Profession\" p ON pl.\"ProfessionId\" = p.\"Id\" " +
                                              "WHERE pl.\"EmployeeId\" = @employeeId",
                    new { employeeId }).ToList();
        }

        internal List<Education> GetEducations(long employeeId)
        {
            using var con = new NpgsqlConnection(_conStr);
            return con.Query<Education>("SELECT * FROM \"Education\" WHERE \"EmployeeId\" = @employeeId", new { employeeId }).ToList();
        }

        internal List<ForeignPassport> GetForeignPassports(long employeeId)
        {
            using var con = new NpgsqlConnection(_conStr);
            return con.Query<ForeignPassport>("SELECT c.\"Name\" as CountryName, fp.\"Number\", fp.\"StartDate\", fp.\"FinishDate\" " +
                                                   "FROM \"ForeignPassport\" fp INNER JOIN \"Country\" c ON fp.\"CountryId\" = c.\"Id\" " +
                                                   "WHERE fp.\"EmployeeId\" = @employeeId",
                    new { employeeId }).ToList();
        }

        internal List<Language> GetLanguages(long employeeId)
        {
            using var con = new NpgsqlConnection(_conStr);
            return con.Query<Language>("SELECT l.\"Name\" as LanguageName, lc.\"Name\" as LanguageCompetence " +
                                            "FROM \"LanguageKnowledge\" lk INNER JOIN \"Language\" l ON lk.\"LanguageId\" = l.\"Id\" " +
                                            "INNER JOIN \"LanguageCompetence\" lc ON lk.\"LanguageCompId\" = lc.\"Id\" " +
                                            "WHERE lk.\"EmployeeId\" = @employeeId",
                    new { employeeId }).ToList();
        }

        internal void AddEmployee(Employee employee)
        {
            using var con = new NpgsqlConnection(_conStr);
            var id = con.Execute(@"INSERT INTO ""Employee"" VALUES (DEFAULT, @INN, @SNILS, @Position, @Surname, @Name, @Patronymic, @Sex, @Bithday, @Nationality, 
                                        @RegAddress, @FactAddress, @InMarriage, @Phone, @Additional) RETURNING ""Id""", employee);

            var relative = employee.Relative;
            con.Execute(@"INSERT INTO ""Relative"" VALUES (DEFAULT, @id, @FIO, @DegreeOfKinship, @Phone)",
                new
                {
                    id,
                    relative.FIO,
                    relative.DegreeOfKinship,
                    relative.Phone
                });

            if (employee.Professions != null)
                foreach (var prof in employee.Professions)
                {
                    var profId = con.Query<int>("SELECT \"Id\" FROM \"Profession\" p WHERE p.\"Name\" = @Name", new { prof.Name }).Single();
                    con.Execute(@"INSERT INTO ""ProfessionLink"" VALUES (DEFAULT, @id, @profId, @IsMain)",
                        new
                        {
                            id,
                            profId,
                            prof.IsMain
                        });
                }

            if (employee.Educations != null)
                foreach (var educ in employee.Educations)
                {
                    con.Execute(@"INSERT INTO ""Education"" VALUES (DEFAULT, @id, @EducationLevel, @DipSeria, @DipNumber, @EducatInstitName, @EducatInstitAddress, 
                                    @YearOfGrad, @Qualification)",
                        new
                        {
                            id,
                            educ.EducationLevel,
                            educ.DipSeria,
                            educ.DipNumber,
                            educ.EducatInstitName,
                            educ.EducatInstitAddress,
                            educ.YearOfGrad,
                            educ.Qualification
                        });
                }

            if (employee.ForeignPassports != null)
                foreach (var forPas in employee.ForeignPassports)
                {
                    var countryId = con.Query<int>("SELECT \"Id\" FROM \"Country\" c WHERE c.\"Name\" = @name", new { @name = forPas.CountryName }).Single();
                    con.Execute(@"INSERT INTO ""ForeignPassport"" VALUES (DEFAULT, @id, @countryId, @Number, @StartDate, @FinishDate)",
                        new
                        {
                            id,
                            countryId,
                            forPas.Number,
                            forPas.StartDate,
                            forPas.FinishDate
                        });
                }

            if (employee.Languages != null)
                foreach (var lang in employee.Languages)
                {
                    var langId = con.Query<int>("SELECT \"Id\" FROM \"Language\" l WHERE l.\"Name\" = @name", new { @name = lang.LanguageName }).Single();
                    var compId = con.Query<int>("SELECT \"Id\" FROM \"LanguageCompetence\" lc WHERE lc.\"Name\" = @name", new { @name = lang.LanguageCompetence }).Single();
                    con.Execute(@"INSERT INTO ""LanguageKnowledge"" VALUES (DEFAULT, @id, @langId, @compId)", new { id, langId, compId });
                }

        }

        internal void UpdateEmployeeAdditional(long id, string additional)
        {
            using var con = new NpgsqlConnection(_conStr);
            con.Execute(@"UPDATE ""Employee"" SET ""Additional"" = @additional WHERE ""Id"" = @id", new { id, additional });
        }

        internal void DeleteEmployee(long employeeId)
        {
            using var con = new NpgsqlConnection(_conStr);
            con.Execute("DELETE FROM \"Relative\" r WHERE r.\"EmployeeId\" = @employeeId", new { employeeId });
            con.Execute("DELETE FROM \"Education\" e WHERE e.\"EmployeeId\" = @employeeId", new { employeeId });
            con.Execute("DELETE FROM \"ForeignPassport\" fp WHERE fp.\"EmployeeId\" = @employeeId", new { employeeId });
            con.Execute("DELETE FROM \"LanguageKnowledge\" lk WHERE lk.\"EmployeeId\" = @employeeId", new { employeeId });
            con.Execute("DELETE FROM \"ProfessionLink\" pl WHERE pl.\"EmployeeId\" = @employeeId", new { employeeId });
            con.Execute("DELETE FROM \"Employee\" e WHERE e.\"Id\" = @employeeId", new { employeeId });
        }
    }
}
