
using FindJobWebAPI.Dto;
using FindJobWebAPI.Models;

namespace FindJobWebAPI.Interfaces
{
    public interface IJobInterface
    {
        ICollection<Job> GetJobs();

        Job GetJobById(int id);

        public bool DeleteJob(Job job);

        public bool JobExists(int jobId);

        public bool CreateJob(Job job);

        public bool UpdateJob(Job job);

        public bool Save();

        public ICollection<JobDto> SortJobs(ICollection<JobDto> jobs, JobSort jobSort);

    }
}
