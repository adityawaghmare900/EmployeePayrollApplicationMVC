

using ModelLayer;
using System.Collections.Generic;

namespace RepositoryLayer.Interfaces
{
    public interface IEmployeeRepo
    {
        public string AddEmployee(EmployeeModel employee);
        public List<EmployeeModel> GetAllEmployee();

        public string UpdateEmployee(EmployeeModel employee);
        public EmployeeModel GetEmployeeById(int id);
        public string DeleteEmployee(int id);
        public EmployeeModel login(EmpLogin login);

        public EmployeeModel GetDetailsByName(string name);

        public EmployeeModel AddOrUpdateEmployee(EmployeeModel employee);

    }
}
