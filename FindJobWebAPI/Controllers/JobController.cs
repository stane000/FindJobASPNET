using AutoMapper;
using FindJobWebAPI.Dto;
using FindJobWebAPI.Interfaces;
using FindJobWebAPI.Models;
using FindJobWebAPI.Repository;
using Microsoft.AspNetCore.Mvc;


namespace FindJobWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController: Controller
    {
        private readonly IJobInterface _jobRepository;
        private readonly ICompanyInterface _companyRepository;
        private readonly IMapper _mapper;

        public JobController(IJobInterface jobRepository, ICompanyInterface companyRepository, IMapper mapper)
        {
            _jobRepository = jobRepository;
            _companyRepository = companyRepository; 
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<JobDto>))]
        public IActionResult GetJobs()
        {
            var jobs = _mapper.Map<List<JobDto>>(_jobRepository.GetJobs());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(jobs);
        }

        [HttpGet("filter-jobs")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<JobDto>))]
        public IActionResult GetFilterJobs([FromQuery] JobFilter jobFilter)
        {
            string companyName = jobFilter.Company != null ? jobFilter.Company : "";
            var jobs = _mapper.Map<List<JobDto>>(_jobRepository.GetJobs());

            //Filter by Company name and Experience
            var filterJobs = jobs.Where(j => j.CompanyName.ToLower().Contains(companyName.ToLower()) && j.Experience > jobFilter.Experience).ToList();

            //Filter by Position
            if (jobFilter.Position != null)
            {
                filterJobs = filterJobs.Where(j => j.Position.ToString().ToLower() == jobFilter.Position.ToLower()).ToList();
            }

            //Filter by Collage
            if (jobFilter.College != null)
            {
                filterJobs = filterJobs.Where(j => j.College.ToString().ToLower() == jobFilter.College.ToLower()).ToList();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(filterJobs);
        }

        [HttpGet("sort-jobs")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<JobDto>))]
        public IActionResult SortFilterJobs([FromQuery] JobSort jobSort)
        {

            ICollection<JobDto>? jobs = null;
            ICollection<JobDto>? sortedJobs = null;


            jobs = _mapper.Map<List<JobDto>>(_jobRepository.GetJobs());

            if (jobs != null)
            {
                sortedJobs = _jobRepository.SortJobs(jobs, jobSort);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(sortedJobs);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateJob([FromBody] Job jobCreate)
        {
            if (jobCreate == null)
            {
                BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_jobRepository.CreateJob(jobCreate))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(jobCreate);
        }

        [HttpPut("{jobId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateJob(int jobId, [FromBody] Job jobUpdate)
        {
            if (jobUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (jobUpdate.Id != jobId)
            {
                return BadRequest(ModelState);
            }

            if (!_jobRepository.JobExists(jobId)) { return NotFound(); }

            if (!ModelState.IsValid) { return BadRequest(); }

            var result = _jobRepository.UpdateJob(jobUpdate);

            if (!result)
            {
                ModelState.AddModelError("", "Something went wrong updating job");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{jobId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteJob(int jobId)
        {
            if (!_jobRepository.JobExists(jobId)) { return NotFound(); }

            var jobDelete = _jobRepository.GetJobById(jobId);

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (_jobRepository.DeleteJob(jobDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting job");
            }

            return NoContent();
        }


    }


}
