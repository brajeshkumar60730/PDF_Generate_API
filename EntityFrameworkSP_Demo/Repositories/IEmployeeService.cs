using EntityFrameworkSP_Demo.Entities;

namespace EntityFrameworkSP_Demo.Repositories
{
    public interface IEmployeeService
    {
        public Task<List<Employee>> GetEmployeeListAsync();
        public Task<IEnumerable<Employee>> GetEmployeeByIdAsync(int Id);
        public Task<int> AddEmployeeAsync(Employee employee);
        public Task<int> UpdateEmployeeAsync(Employee employee);
        public Task<int> DeleteEmployeeAsync(int Id);
    }
}
