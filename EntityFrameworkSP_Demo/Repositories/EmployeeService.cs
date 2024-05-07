using EntityFrameworkSP_Demo.Data;
using EntityFrameworkSP_Demo.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkSP_Demo.Repositories
{
    public class EmployeeService : IEmployeeService
    {
        private readonly DbContextClass _dbContext;

        public EmployeeService(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Employee>> GetEmployeeListAsync()
        {
            return await _dbContext.Employee
                .FromSqlRaw<Employee>("GetEmployeeList")
                .ToListAsync();
        }

        public async Task<IEnumerable<Employee>> GetEmployeeByIdAsync(int Id)
        {
            var param = new SqlParameter("@Id", Id);

            var employeeDetails = await Task.Run(() => _dbContext.Employee
                            .FromSqlRaw(@"exec GetPrductByID @Id", param).ToListAsync());

            return employeeDetails;
        }

        public async Task<int> AddEmployeeAsync(Employee employee)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Name", employee.Name));
            parameter.Add(new SqlParameter("Designation", employee.Designation));
            parameter.Add(new SqlParameter("@Address", employee.Address));
            parameter.Add(new SqlParameter("@RecordCreatedOn", employee.RecordCreatedOn));

            var result = await Task.Run(() => _dbContext.Database
           .ExecuteSqlRawAsync(@"exec AddNewEmployee @Name, @Designation, @Address, @RecordCreatedOn", parameter.ToArray()));

            return result;
        }
        public async Task<int> UpdateEmployeeAsync(Employee employee)
        {
            var parameter = new List<SqlParameter>();
            parameter.Add(new SqlParameter("@Id", employee.Id));
            parameter.Add(new SqlParameter("@Name", employee.Name));
            parameter.Add(new SqlParameter("Designation", employee.Designation));
            parameter.Add(new SqlParameter("@Address", employee.Address));
            parameter.Add(new SqlParameter("@RecordCreatedOn", employee.RecordCreatedOn));

            var result = await Task.Run(() => _dbContext.Database
           .ExecuteSqlRawAsync(@"exec UpdateEmployee @Id, @Name, @Designation, @Address, @RecordCreatedOn", parameter.ToArray()));

            return result;
        }


        public async Task<int> DeleteEmployeeAsync(int Id)
        {
            return await Task.Run(() => _dbContext.Database.ExecuteSqlInterpolatedAsync($"DeleteEmployeeByID {Id}"));
        }
    }
}
