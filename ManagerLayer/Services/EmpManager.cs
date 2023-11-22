
using ManagerLayer.Interfaces;
using ModelLayer;
using RepositoryLayer.Interfaces;
using System.Collections.Generic;

namespace ManagerLayer.Services
{
    public class EmpManager: IEmpManager
    {
        private readonly IEmployeeRepo employeeRepo;

        public EmpManager(IEmployeeRepo employeeRepo)
        {
            this.employeeRepo = employeeRepo;
        }

        public string AddEmployee(EmployeeModel employee)
        {
            return employeeRepo.AddEmployee(employee);
        }

        public List<EmployeeModel> GetAllEmployee()
        {
            return employeeRepo.GetAllEmployee();
        }

        public string UpdateEmployee(EmployeeModel employee)
        {
            return employeeRepo.UpdateEmployee(employee);
        }

        public EmployeeModel GetEmployeeById(int id)
        {
            return employeeRepo.GetEmployeeById(id);
        }

        public string DeleteEmployee(int id)
        {
            return employeeRepo.DeleteEmployee(id);
        }

        public EmployeeModel login(EmpLogin login)
        {
            return employeeRepo.login(login);
        }

        public EmployeeModel GetDetailsByName(string name)
        {
            return employeeRepo.GetDetailsByName(name);
        }

        public EmployeeModel AddOrUpdateEmployee(EmployeeModel employee)
        {
            return employeeRepo.AddOrUpdateEmployee(employee);
        }
    }
}
