using InnowiseProject.Database.Models;
using InnowiseProject.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace InnowiseProject.Database.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly ApplicationDbContext dbContext;

        public WorkerRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateWorker(Worker worker)
        {
            dbContext.Workers.Add(worker);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteWorker(string workerId)
        {
            await dbContext.Workers.Where(x => x.Id == workerId).DeleteAsync();
            await dbContext.SaveChangesAsync();
        }

        public async Task<Worker> GetWorkerById(string workerId)
        {
            var worker = await dbContext.Workers.Where(x => x.Id == workerId).FirstOrDefaultAsync();
            return worker != null ? worker : null;
        }

        public async Task<List<Worker>> GetWorkers()
        {
            return await dbContext.Workers.ToListAsync();
        }

        public async Task<List<Worker>> GetWorkersByFirstName(string firstName)
        {
            return await dbContext.Workers.Where(x => x.FirstName == firstName).ToListAsync();
        }

        public async Task<List<Worker>> GetWorkersByFirstNameAndLastName(string lastName, string firstName)
        {
            return await dbContext.Workers.Where(x => x.LastName == lastName && x.FirstName == firstName).ToListAsync();
        }

        public async Task<List<Worker>> GetWorkersByLastName(string lastName)
        {
            return await dbContext.Workers.Where(x => x.LastName == lastName).ToListAsync();
        }

        public async Task UpdateWorker(Worker worker)
        {
            dbContext.Workers.Update(worker);
            await dbContext.SaveChangesAsync();
        }
    }
}
