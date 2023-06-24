using astute.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace astute.Repository
{
    public partial interface IEmployeeService
    {
        #region Employee Master
        Task<int> InsertEmployee(Employee_Master employee_Master);
        Task<int> UpdateEmployee(Employee_Master employee_Master);
        Task<int> DeleteEmployee(int employeeId);
        Task<Employee_Master> GetEmployeeById(int empId);
        Task<IList<Employee_Master>> GetEmployees(string userName);
        Task<Employee_Master> EmployeeLogin(UserModel userModel);
        #endregion

        #region Employee Document
        Task<bool> IsExistUserName(string userName);
        Task<int> InsertEmployeeDocument(Employee_Document employee_Document);
        Task<int> UpdateEmployeeDocument(Employee_Document employee_Document);
        Task<int> DeleteEmployeeDocument(int employeeId);
        Task<IList<Employee_Document>> GetEmployeeDocuments(int employeeId);
        Task<Employee_Document> GetEmployeeDocumentById(int employeeId);
        #endregion

        #region Employee Salary
        Task<int> InsertEmployeeSalary(Employee_Salary employee_Salary);
        Task<int> UpdateEmployeeSalary(Employee_Salary employee_Salary);
        Task<int> DeleteEmployeeSalary(int employeeId);
        Task<IList<Employee_Salary>> GetEmployeeSalary(int employeeId);
        Task<Employee_Salary> GetEmployeeSalaryById(int employeeId);
        #endregion
    }
}
