using InnowiseProject.Database.Models;

namespace InnowiseProject.Database.Repositories.Interfaces
{
    public interface IWorkerRepository
    {
        Task CreateWorker(Worker worker);

        Task DeleteWorker(string workerId);

        Task UpdateWorker(Worker worker);

        Task<Worker> GetWorkerById(string workerId);

        Task<Worker> GetWorkerDetailsById(string workerId);

        Task<IReadOnlyList<Worker>> GetWorkersByFirstName(string firstName);

        Task<IReadOnlyList<Worker>> GetWorkers();

        Task<IReadOnlyList<Worker>> GetWorkersDetails();
    }
}
