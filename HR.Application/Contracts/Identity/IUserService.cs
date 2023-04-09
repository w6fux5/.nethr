using HR.Application.Models.Identity;

namespace HR.Application.Contracts.Identity;

public interface IUserService
{
    Task<List<Employee>> GetEmployees();

    Task<Employee> GetEmployee(string userId);
}
