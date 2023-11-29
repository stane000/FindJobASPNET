
using FindJobWebAPI.Models;

namespace FindJobWebAPI.Interfaces
{
    public interface IWorkerInterface
    {
        ICollection<Worker> GetWorkers();
        //ICollection<WorkerDto> GetWorkersDto();

        Worker GetWorkerById(int id);

        public bool DeleteWorker(Worker worker);

        public bool WorkerExists(int workerId);

        public bool CreateWorker(Worker worker);

        public bool UpdateWorker(Worker worker);

        public bool Save();

    }
}
