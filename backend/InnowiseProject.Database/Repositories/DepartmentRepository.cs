﻿using InnowiseProject.Database.Models;
using InnowiseProject.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace InnowiseProject.Database.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext dbContext;

        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateDepartment(Department department)
        {
            dbContext.Departments.Add(department);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteDepartment(int id)
        {
            await dbContext.Departments.Where(x => x.Id == id).DeleteAsync();
            await dbContext.SaveChangesAsync();
        }

        public async Task<Department> GetDepartmentById(int id)
        {
            var department = await dbContext.Departments.Where(x => x.Id == id).FirstOrDefaultAsync();

            return department != null ? department : null;
        }

        public async Task<List<Department>> GetDepartmentsByName(string name)
        {
            return await dbContext.Departments.Where(x => x.Name == name).ToListAsync();
        }

        public async Task<List<Department>> GetDepartments()
        {
            return await dbContext.Departments.ToListAsync();
        }

        public async Task UpdateDepartmentName(int id, string name)
        {
            var department = await dbContext.Departments.Where(x => x.Id == id).FirstOrDefaultAsync();
            department.Name = name;
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Department>> GetDepartmentsWithDetails()
        {
            return await dbContext.Departments.Include(x => x.Workers).Include(x => x.Products).ToListAsync();
        }

        public async Task AddProduct(Product product)
        {
            var department = await dbContext.Departments.Where(x => x.Id == product.DepartmentId).FirstOrDefaultAsync();
            department.Products.Add(product);
            await dbContext.SaveChangesAsync();
        }
    }
}