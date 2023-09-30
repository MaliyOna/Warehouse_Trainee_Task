using InnowiseProject.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnowiseProject.Database.Repositories.Interfaces
{
    public interface IWorkerRepository
    {
        Task CreateWorker(Worker worker);
        Task DeleteWorker(string workerId);
        Task UpdateWorker(Worker worker);
        Task<Worker> GetWorkerById(string workerId);
        Task<Worker> GetWorkerDetailsById(string workerId);
        Task<List<Worker>> GetWorkersByFirstName(string firstName);
        Task<List<Worker>> GetWorkers();
        Task<List<Worker>> GetWorkersDetails();
    }
}
