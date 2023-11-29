using FindJobWebAPI.Data;
using FindJobWebAPI.Dto;
using FindJobWebAPI.Interfaces;
using FindJobWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FindJobWebAPI.Repository
{
    public class WorkerRepository: IWorkerInterface
    {
        readonly DataContext _context;
        public WorkerRepository(DataContext context) 
        {
            _context = context;
        }

        public ICollection<Worker> GetWorkers() 
        {
            return _context.Workers.Include(w => w.Company).OrderBy(x => x.Id).ToList(); 
        }

        //public ICollection<WorkerDto> GetWorkersDto()
        //{
        //    return _context.Workers.Include((worker => worker.Company)).Select(w => WorkerDto.CreateFromWorker(w)).ToList();
        //}

        public Worker GetWorkerById(int id) 
        {
            return _context.Workers.Include(w => w.Company).Include(c => c.Company.Jobs).FirstOrDefault(w => w.Id == id);    
        }

        public bool CreateWorker(Worker worker)
        {
            if(worker.Id == 0 && _context.Workers.Any())
            {
                worker.Id = _context.Workers.Max(x => x.Id) + 1;
            }
            _context.Add(worker);
            return Save();
        }

        public bool UpdateWorker(Worker worker)
        {
            _context.Update(worker);
            return Save();  
        }

        public bool DeleteWorker(Worker worker)
        {
            _context.Remove(worker);
            return Save();
        }

        public bool WorkerExists(int workerId)
        {
           return _context.Workers.Any(w => w.Id == workerId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
