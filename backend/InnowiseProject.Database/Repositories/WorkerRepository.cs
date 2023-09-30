using InnowiseProject.Database.Models;
using InnowiseProject.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
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
            await dbContext.Workers
                .Where(x => x.Id == workerId)
                .DeleteAsync();

            await dbContext.SaveChangesAsync();
        }

        public async Task<Worker> GetWorkerById(string workerId)
        {
            var worker = await dbContext.Workers
                .Where(x => x.Id == workerId)
                .FirstOrDefaultAsync();

            return worker;
        }

        public async Task<Worker> GetWorkerDetailsById(string workerId)
        {
            var worker = await dbContext.Workers
                .Include(x => x.Departments)
                .Where(x => x.Id == workerId)
                .FirstOrDefaultAsync();

            return worker;
        }

        public async Task<IReadOnlyList<Worker>> GetWorkers()
        {
            return await dbContext.Workers.ToListAsync();
        }

        public async Task<IReadOnlyList<Worker>> GetWorkersByFirstName(string firstName)
        {
            return await dbContext.Workers
                .Where(x => x.FirstName == firstName)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Worker>> GetWorkersDetails()
        {
            return await dbContext.Workers
                .Include(x =>x.Departments)
                .ToListAsync();
        }

        public async Task UpdateWorker(Worker worker)
        {
            dbContext.Workers.Update(worker);
            await dbContext.SaveChangesAsync();
        }
    }
}
