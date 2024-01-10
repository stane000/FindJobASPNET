
using FindJobWebAPI.Data;
using FindJobWebAPI.Dto;
using FindJobWebAPI.Interfaces;
using FindJobWebAPI.Models;
using Microsoft.EntityFrameworkCore;

//fsgfsgfs
namespace FindJobWebAPI.Repository
{
    public class JobRepository: IJobInterface
    {
        readonly DataContext _context;
        public JobRepository(DataContext context) 
        {
            _context = context;
        }

        public ICollection<Job> GetJobs() 
        {
            return _context.Jobs.Include(c => c.Company).OrderBy(x => x.Id).ToList(); 
        }

        public Job GetJobById(int id)
        {
            return _context.Jobs.FirstOrDefault(w => w.Id == id);
        }

        public bool CreateJob(Job job)
        {
            _context.Add(job);
            return Save();
        }

        public bool UpdateJob(Job job)
        {
            _context.Update(job);
            return Save();
        }

        public bool DeleteJob(Job job)
        {
            _context.Remove(job);
            return Save();
        }

        public bool JobExists(int jobId)
        {
            return _context.Jobs.Any(w => w.Id == jobId);
        }

        public ICollection<JobDto> SortJobs(ICollection<JobDto> jobs, JobSort jobSort)
        {
            if (jobSort == JobSort.AvgSalary)
            {
                return jobs.OrderBy(j => j.CompanyAvgSalary).ToList();
            }

            if (jobSort == JobSort.AvgSalaryDesc)
            {
                return jobs.OrderByDescending(j => j.CompanyAvgSalary).ToList();
            }

            if (jobSort == JobSort.expSort)
            {
                return jobs.OrderBy(j => j.Experience).ToList();
            }

            if (jobSort == JobSort.exoSortDesc)
            {
                return jobs.OrderByDescending(j => j.Experience).ToList();
            }
            else return jobs;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
