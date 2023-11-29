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
    public class WorkerController : Controller
    {
        private readonly IWorkerInterface _workerRepository;
        private readonly ICompanyInterface _companyRepository;
        private readonly IMapper _mapper;
        public WorkerController(IWorkerInterface workerRepository, ICompanyInterface companyRepository, IMapper mapper)
        {
            _workerRepository = workerRepository;
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<WorkerDto>))]
        public IActionResult GetWorkers()
        {
            var workers = _mapper.Map<List<WorkerDto>>(_workerRepository.GetWorkers());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(workers);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateWorker([FromBody] Worker workerCreate)
        {
            if (workerCreate == null)
            {
                BadRequest(ModelState);
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_workerRepository.CreateWorker(workerCreate))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok(workerCreate);
        }

        [HttpPut("{workerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateWorker(int workerId, [FromBody] Worker workerUpdate)
        {
            if (workerUpdate == null)
            {
                return BadRequest(ModelState);
            }

            if (workerUpdate.Id != workerId)
            {
                return BadRequest(ModelState);
            }

            if (!_workerRepository.WorkerExists(workerId)) { return NotFound(); }

            if (!ModelState.IsValid) { return BadRequest(); }

            var result = _workerRepository.UpdateWorker(workerUpdate);

            if (!result)
            {
                ModelState.AddModelError("", "Something went wrong updating worker");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{workerId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult DeleteWorker(int workerId)
        {
            if (!_workerRepository.WorkerExists(workerId)) { return NotFound(); }

            var workerDelete = _workerRepository.GetWorkerById(workerId);

            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            if (_workerRepository.DeleteWorker(workerDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting worker");
            }

            return NoContent();
        }


    }


}
