using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Grpc.Core;
using Services;
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
                var employees = _employeeOperations.GetEmployees();
                _logger.LogInformation("Получение сотрудников успешно");
                var result = new GetReply();
                result.Employees.AddRange(Map(employees));
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
                _employeeOperations.AddEmployees(Map(request.Employees));
                _logger.LogInformation("Добавление сотрудников успешно");
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
                _employeeOperations.UpdateEmployeesAdditional(Map(request.Patches));
                _logger.LogInformation("Изменение сотрудников успешно");
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
                _employeeOperations.DeleteEmployees(request.Ids.ToList());
                _logger.LogInformation("Удаление сотрудников успешно");
                return Task.FromResult(new Empty());
            }
            catch (Exception ex)
            {
                var message = ex.Message + "\n\n" + ex.StackTrace;
                _logger.LogError($"Произошла ошибка:    {message}");
                throw new RpcException(new Status(StatusCode.Internal, message));
            }
        }

        private static IEnumerable<GRPC.Employee> Map(List<Dto.Employee> employees)
        {
            var config = new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<Dto.Employee, GRPC.Employee>();
                cfg.CreateMap<Dto.Education, GRPC.Education>();
                cfg.CreateMap<Dto.ForeignPassport, GRPC.ForeignPassport>();
                cfg.CreateMap<Dto.Language, GRPC.Language>();
                cfg.CreateMap<Dto.Profession, GRPC.Profession>();
                cfg.CreateMap<Dto.Relative, GRPC.Relative>();
            });
            var mapper = config.CreateMapper();

            return mapper.Map<List<Dto.Employee>, IEnumerable<GRPC.Employee>>(employees);
        }

        private static List<Dto.Employee> Map(IEnumerable<GRPC.Employee> employees)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GRPC.Employee, Dto.Employee>();
                cfg.CreateMap<GRPC.Employee, Dto.Employee>();
                cfg.CreateMap<GRPC.Employee, Dto.Employee>();
                cfg.CreateMap<GRPC.Employee, Dto.Employee>();
                cfg.CreateMap<GRPC.Employee, Dto.Employee>();
                cfg.CreateMap<GRPC.Employee, Dto.Employee>();
            });
            var mapper = config.CreateMapper();

            return mapper.Map<IEnumerable<GRPC.Employee>, List<Dto.Employee>>(employees);
        }

        private static List<Dto.PatchDto> Map(IEnumerable<GRPC.PatchDto> patches)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<GRPC.PatchDto, Dto.PatchDto>());
            var mapper = config.CreateMapper();

            return mapper.Map<IEnumerable<GRPC.PatchDto>, List<Dto.PatchDto>>(patches);
        }
    }
}
