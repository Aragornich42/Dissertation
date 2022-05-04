using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Grpc.Core;
using Services;
using Timestamp = Google.Protobuf.WellKnownTypes.Timestamp;
using Empty = Google.Protobuf.WellKnownTypes.Empty;

namespace GRPC
{
    public class EmployeeService : EmployeeSrv.EmployeeSrvBase
    {
        private readonly ILogger<EmployeeService> _logger;
        private readonly IEmployeeOperations _employeeOperations;

        public EmployeeService(ILogger<EmployeeService> logger, IEmployeeOperations employeeOperations)
        {
            _logger = logger;
            _employeeOperations = employeeOperations;
        }

        public override Task<GetReply> Get(Empty request, ServerCallContext context)
        {
            try
            {
                //Console.WriteLine($"Размер запроса: {Marshal.SizeOf(request)}б");
                var employees = _employeeOperations.GetEmployees();
                _logger.LogInformation("Получение сотрудников успешно");
                var result = new GetReply();
                result.Employees.AddRange(Map(employees));
                //Console.WriteLine($"Размер ответа: {Marshal.SizeOf(result) / 1024.0 / 1024.0}мб");
                return Task.FromResult(result);
            }
            catch (Exception ex)
            {
                var message = ex.Message + "\n\n" + ex.StackTrace;
                _logger.LogError($"Произошла ошибка:    {message}");
                throw new RpcException(new Status(StatusCode.Internal, message));
            }
        }

        public override Task<Empty> Post(PostRequest request, ServerCallContext context)
        {
            try
            {
                //Console.WriteLine($"Размер запроса: {Marshal.SizeOf(request)/1024.0}кб");
                _employeeOperations.AddEmployees(Map(request.Employees));
                _logger.LogInformation("Добавление сотрудников успешно");
                //Console.WriteLine($"Размер ответа: {Marshal.SizeOf(typeof(Empty))}б");
                return Task.FromResult(new Empty());
            }
            catch (Exception ex)
            {
                var message = ex.Message + "\n\n" + ex.StackTrace;
                _logger.LogError($"Произошла ошибка:    {message}");
                throw new RpcException(new Status(StatusCode.Internal, message));
            }
        }

        public override Task<Empty> Patch(PatchRequest request, ServerCallContext context)
        {
            try
            {
                //Console.WriteLine($"Размер запроса: {Marshal.SizeOf(request)}б");
                _employeeOperations.UpdateEmployeesAdditional(Map(request.Patches));
                _logger.LogInformation("Изменение сотрудников успешно");
                //Console.WriteLine($"Размер ответа: {Marshal.SizeOf(typeof(Empty))}б");
                return Task.FromResult(new Empty());
            }
            catch (Exception ex)
            {
                var message = ex.Message + "\n\n" + ex.StackTrace;
                _logger.LogError($"Произошла ошибка:    {message}");
                throw new RpcException(new Status(StatusCode.Internal, message));
            }
        }

        public override Task<Empty> Delete(DeleteRequest request, ServerCallContext context)
        {
            try
            {
                //Console.WriteLine($"Размер запроса: {Marshal.SizeOf(request)}б");
                _employeeOperations.DeleteEmployees(request.Ids.ToList());
                _logger.LogInformation("Удаление сотрудников успешно");
                //Console.WriteLine($"Размер ответа: {Marshal.SizeOf(typeof(Empty))}б");
                return Task.FromResult(new Empty());
            }
            catch (Exception ex)
            {
                var message = ex.Message + "\n\n" + ex.StackTrace;
                _logger.LogError($"Произошла ошибка:    {message}");
                throw new RpcException(new Status(StatusCode.Internal, message));
            }
        }

        private static List<GRPC.Employee> Map(IEnumerable<Dto.Employee> employees)
        {
            return employees?.Select(emp => 
            {
                var employee = new Employee
                {
                    Id = emp.Id,
                    Inn = emp.INN,
                    Snils = emp.SNILS,
                    Position = emp.Position,
                    Surname = emp.Surname,
                    Name = emp.Name,
                    Patronymic = emp.Patronymic ?? string.Empty,
                    Sex = emp.Sex,
                    Bithday = Timestamp.FromDateTime(emp.Bithday.ToUniversalTime()),
                    Nationality = emp.Nationality,
                    RegAddress = emp.RegAddress,
                    FactAddress = emp.FactAddress ?? string.Empty,
                    InMarriage = emp.InMarriage,
                    Phone = emp.Phone,
                    Additional = emp.Additional ?? string.Empty,
                    Relative = emp.Relative == null ? new Relative() : new Relative
                    {
                        Fio = emp.Relative.FIO,
                        DegreeOfKinship = emp.Relative.DegreeOfKinship,
                        Phone = emp.Relative.Phone,
                    },
                };

                if (emp.Professions != null)
                    employee.Professions.AddRange(emp.Professions.Select(prof => new Profession
                    {
                        Name = prof.Name,
                        IsMain = prof.IsMain,
                    }).ToList());

                if (emp.Educations != null)
                    employee.Educations.AddRange(emp.Educations.Select(educ => new Education
                    {
                        EducationLevel = educ.EducationLevel,
                        DipSeria = educ.DipSeria,
                        DipNumber = educ.DipNumber,
                        EducatInstitName = educ.EducatInstitName,
                        EducatInstitAddress = educ.EducatInstitAddress,
                        YearOfGrad = educ.YearOfGrad,
                        Qualification = educ.Qualification ?? string.Empty,
                    }).ToList());

                if (emp.ForeignPassports != null)
                    employee.ForeignPassports.AddRange(emp.ForeignPassports.Select(fp => new ForeignPassport
                    {
                        CountryName = fp.CountryName,
                        Number = fp.Number,
                        StartDate = Timestamp.FromDateTime(fp.StartDate.ToUniversalTime()),
                        FinishDate = Timestamp.FromDateTime(fp.FinishDate.ToUniversalTime()),
                    }).ToList());

                if (emp.Languages != null)
                    employee.Languages.AddRange(emp.Languages.Select(lang => new Language
                    {
                        LanguageName = lang.LanguageName,
                        LanguageCompetence = lang.LanguageCompetence,
                    }).ToList());

                return employee;
            }).ToList();
        }

        private static List<Dto.Employee> Map(IEnumerable<GRPC.Employee> employees)
        {
            return employees?.Select(emp =>
            {
                var employee = new Dto.Employee
                {
                    Id = emp.Id,
                    INN = emp.Inn,
                    SNILS = emp.Snils,
                    Position = emp.Position,
                    Surname = emp.Surname,
                    Name = emp.Name,
                    Patronymic = emp.Patronymic,
                    Sex = emp.Sex,
                    Bithday = emp.Bithday.ToDateTime(),
                    Nationality = emp.Nationality,
                    RegAddress = emp.RegAddress,
                    FactAddress = emp.FactAddress,
                    InMarriage = emp.InMarriage,
                    Phone = emp.Phone,
                    Additional = emp.Additional,
                    Relative = new Dto.Relative
                    {
                        FIO = emp.Relative?.Fio,
                        DegreeOfKinship = emp.Relative?.DegreeOfKinship,
                        Phone = emp.Relative?.Phone,
                    },
                };

                if (emp.Professions.Any())
                {
                    employee.Professions = new List<Dto.Profession>();
                    employee.Professions.AddRange(emp.Professions.Select(prof => new Dto.Profession
                    {
                        Name = prof.Name,
                        IsMain = prof.IsMain,
                    }).ToList());
                }

                if (emp.Educations.Any())
                {
                    employee.Educations = new List<Dto.Education>();
                    employee.Educations.AddRange(emp.Educations.Select(educ => new Dto.Education
                    {
                        EducationLevel = educ.EducationLevel,
                        DipSeria = educ.DipSeria,
                        DipNumber = educ.DipNumber,
                        EducatInstitName = educ.EducatInstitName,
                        EducatInstitAddress = educ.EducatInstitAddress,
                        YearOfGrad = educ.YearOfGrad,
                        Qualification = educ.Qualification,
                    }).ToList());
                }

                if (emp.ForeignPassports.Any())
                {
                    employee.ForeignPassports = new List<Dto.ForeignPassport>();
                    employee.ForeignPassports.AddRange(emp.ForeignPassports.Select(fp => new Dto.ForeignPassport
                    {
                        CountryName = fp.CountryName,
                        Number = fp.Number,
                        StartDate = fp.StartDate.ToDateTime(),
                        FinishDate = fp.FinishDate.ToDateTime(),
                    }).ToList());
                }

                if (emp.Languages.Any())
                {
                    employee.Languages = new List<Dto.Language>();
                    employee.Languages.AddRange(emp.Languages.Select(lang => new Dto.Language
                    {
                        LanguageName = lang.LanguageName,
                        LanguageCompetence = lang.LanguageCompetence,
                    }).ToList());
                }

                return employee;
            }).ToList();
        }

        private static List<Dto.PatchDto> Map(IEnumerable<GRPC.PatchDto> patches)
        {
            return patches?.Select(patch => new Dto.PatchDto
            {
                Id = patch.Id,
                Additional = patch.Additional,
            }).ToList();
        }
    }
}
