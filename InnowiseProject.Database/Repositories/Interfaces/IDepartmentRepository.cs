using InnowiseProject.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnowiseProject.Database.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        Task CreateDepartment(Department department);
        Task<List<Department>> GetDepartments();
        Task<Department> GetDepartmentById(int id);
        Task<List<Department>> GetDepartmentsByName(string name);
        Task UpdateDepartment(int id, string name);
        Task DeleteDepartment(int id);
    }
}
