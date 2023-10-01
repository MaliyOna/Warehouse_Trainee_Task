using InnowiseProject.Database.Models;
using System.Collections.Generic;

namespace InnowiseProject.Database.Repositories.Interfaces
{
    public interface IDepartmentRepository
    {
        Task CreateDepartment(Department department);

        Task<IReadOnlyList<Department>> GetDepartments();

        Task<IReadOnlyList<Department>> GetDepartmentsWithDetails();

        Task<Department> GetDepartmentById(int id);

        Task<Department> GetDepartmentByIdDetails(int id);

        Task<IReadOnlyList<Department>> GetDepartmentsByName(string name);

        Task UpdateDepartmentName(Department department);

        Task DeleteDepartment(int id);

        Task AddProduct(Product product);

        Task AddWorker(Worker worker, int departmentId);

        Task DeleteWorker(Worker worker, int departmentId);
    }
}
