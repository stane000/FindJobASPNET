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
    public class CompanyController : Controller
    {
        private readonly ICompanyInterface _companyRepository;
        private readonly IWorkerInterface _workerRepository;
        private readonly IJobInterface _jobRepository;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyInterface companyRepository, IWorkerInterface workerRepository, IJobInterface jobRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _workerRepository = workerRepository;
            _jobRepository = jobRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Company>))]
        public IActionResult GetCompanies()
        {

            var companies = _mapper.Map<List<CompanyDto>>(_companyRepository.GetCompanies());


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(companies);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCompany([FromBody] Company companyCreate)
        {
            if (companyCreate == null)
            {
                BadRequest(ModelState);
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            if (!_companyRepository.CreateCompany(companyCreate))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(companyCreate);
        }

        [HttpPut("{companyId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCompany(int companyId, [FromBody] Company companyUpdate)
        {
            if (companyUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (companyUpdate.Id != companyId)
            {
                return BadRequest(ModelState);
            }

            var company = _companyRepository.GetCompanyById(companyId);

            if (company == null) { return NotFound(); }

            if (!ModelState.IsValid) { return BadRequest(); }

            if(companyUpdate.Workers == null) { 
                companyUpdate.Workers = company.Workers;    
            }

            if (companyUpdate.Jobs == null)
            {
                companyUpdate.Jobs = company.Jobs;
            }

            var result = _companyRepository.UpdateCompany(companyUpdate);

            if (!result)
            {
                ModelState.AddModelError("", "Something went wrong updating company");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpPut("{companyId}EmployWorker")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult EmployWorker(int companyId, int workerId)
        {
            var worker = _workerRepository.GetWorkerById(workerId);

            if (worker == null)
            {
                return BadRequest(ModelState);
            }

            var company = _companyRepository.GetCompanyById(companyId);

            if (company == null)
            {
                return BadRequest(ModelState);
            }


            company.Workers.Add(worker);
            var result = _companyRepository.UpdateCompany(company);

            if (!result)
            {
                ModelState.AddModelError("", "Something went wrong updating company");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

        [HttpPut("{companyId}AddJob")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult AddJob(int companyId, [FromBody] Job job)
        {
            if (job == null)
            {
                return BadRequest(ModelState);
            }

            var company = _companyRepository.GetCompanyById(companyId);

            if (company == null)
            {
                return BadRequest(ModelState);
            }

            company.Jobs.Add(job);
            var result = _companyRepository.UpdateCompany(company);

            if (!result)
            {
                ModelState.AddModelError("", "Something went wrong updating company");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

        [HttpPut("{companyId}RemoveJob")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult RemoveJob(int companyId, int jobID)
        {
            var job = _jobRepository.GetJobById(jobID);

            if (job == null)
            {
                return BadRequest(ModelState);
            }

            var result = _jobRepository.DeleteJob(job);
         
            if (!result)
            {
                ModelState.AddModelError("", "Something went wrong updating company");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

        [HttpPut("{companyId}FireWorker")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult FireWorker(int companyId, int workerId)
        {
            var worker = _workerRepository.GetWorkerById(workerId);

            if (worker == null)
            {
                return BadRequest(ModelState);
            }

            var company = _companyRepository.GetCompanyById(companyId);

            if (company == null)
            {
                return BadRequest(ModelState);
            }

            if(company.Workers.Any(w => w.Id == workerId))
            {
                worker.CompanyId = null;
                worker.Company = null;

            }
            else
            {
                return BadRequest(ModelState);
            }

            var result = _workerRepository.UpdateWorker(worker);    

            if (!result)
            {
                ModelState.AddModelError("", "Something went wrong updating company");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

        [HttpDelete("{companyId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCompany(int companyId)
        {
            if (!_companyRepository.CompanyExists(companyId)) { return NotFound(); }

            var companyDelelte = _companyRepository.GetCompanyById(companyId);

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (_companyRepository.DeleteCompany(companyDelelte))
            {
                ModelState.AddModelError("", "Something went wrong deleting company");
            }

            return NoContent();
        }
    }


}
