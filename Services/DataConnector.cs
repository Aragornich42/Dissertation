using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using Dto;

namespace Services
{
    internal class DataConnector
    {
        string _conStr;
        IDbConnection _con;
        
        internal DataConnector()
        {
            _conStr = "postgresql://postgres@localhost:5432/postgres";
        }

        internal List<Employee> GetEmployees()
        {
            using (_con = new SqlConnection(_conStr))
            {
                return _con.Query<Employee>("SELECT * FROM Employee").ToList();
            }
        }

        internal Relative GetRelative(long employeeId)
        {
            using (_con = new SqlConnection(_conStr))
            {
                return _con.Query<Relative>("SELECT * FROM Employee WHERE EmployeeId = @employeeId", new { employeeId }).FirstOrDefault();
            }
        }

        internal List<Profession> GetProfessions(long employeeId)
        {
            using (_con = new SqlConnection(_conStr))
            {
                return _con.Query<Profession>("SELECT p.Name, pl.IsMain " +
                                              "FROM ProfessionLink pl INNER JOIN Profession p ON pl.ProfessionId = p.Id " +
                                              "WHERE pl.EmployeeId = @employeeId", 
                    new { employeeId }).ToList();
            }
        }

        internal List<Education> GetEducations(long employeeId)
        {
            using (_con = new SqlConnection(_conStr))
            {
                return _con.Query<Education>("SELECT * FROM Education WHERE EmployeeId = @employeeId", new { employeeId }).ToList();
            }
        }

        internal List<ForeignPassport> GetForeignPassports(long employeeId)
        {
            using (_con = new SqlConnection(_conStr))
            {
                return _con.Query<ForeignPassport>("SELECT c.Name as CountryName, fp.Number, fp.StartDate, fp.FinishDate, " +
                                                   "FROM ForeignPassport fp INNER JOIN Country c ON fp.CountryId = c.Id " +
                                                   "WHERE fp.EmployeeId = @employeeId",
                    new { employeeId }).ToList();
            }
        }

        internal List<Language> GetLanguages(long employeeId)
        {
            using (_con = new SqlConnection(_conStr))
            {
                return _con.Query<Language>("SELECT l.Name as LanguageName, lc.Name as LanguageCompetence " +
                                              "FROM LanguageKnowledge lk INNER JOIN Language l ON lk.LanguageId = l.Id " +
                                                "INNER JOIN LanguageCompetence lc ON lk.LanguageCompId = lc.Id " +
                                              "WHERE lk.EmployeeId = @employeeId",
                    new { employeeId }).ToList();
            }
        }

        internal void AddEmployee(Employee employee)
        {
            using (_con = new SqlConnection(_conStr))
            {
                
            }
        }
    }
}
