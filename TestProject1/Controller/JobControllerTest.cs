using AutoMapper;
using FakeItEasy;
using FindJobWebAPI.Controllers;
using FindJobWebAPI.Dto;
using FindJobWebAPI.Interfaces;
using FindJobWebAPI.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;


namespace TestProject1.Controller
{
    public class JobControllerTest
    {
        private readonly IJobInterface _jobRepository;
        private readonly ICompanyInterface _companyRepository;
        private IMapper _mapper;

        public JobControllerTest()
        {
            _jobRepository = A.Fake<IJobInterface>();
            _companyRepository = A.Fake<ICompanyInterface>();
            _mapper = A.Fake<IMapper>();
        }
        [Fact]
        public void JobController_GetJobs_ReturnOK()
        {
            var jobs = A.Fake<ICollection<JobDto>>();
            var jobsList = A.Fake<List<JobDto>>();
 
            A.CallTo(() => _mapper.Map<List<JobDto>>(jobs)).Returns(jobsList);
            var controller = new JobController(_jobRepository, _companyRepository, _mapper);

            //Act
            var result = controller.GetJobs();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }

        [Fact]
        public void JobController_CreateJob_ReturnOK()
        {

            var controller = new JobController(_jobRepository, _companyRepository, _mapper);

            Job job = new Job();
            job.CompanyId = 1;
            job.College = College.PMF;
            job.Experience = 3;
            job.Position = Position.SoftwareEngineer;
            A.CallTo(() => _jobRepository.CreateJob(job)).Returns(true);

            //Act
            var result = controller.CreateJob(job);

            //Asserts
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);

        }

        [Fact]
        public void JobController_UpdateJob_ReturnOK()
        {

            Job job = new Job();
            job.Id = 1;
            job.CompanyId = 1;
            job.College = College.PMF;
            job.Experience = 3;
            job.Position = Position.SoftwareEngineer;

            A.CallTo(() => _jobRepository.JobExists(1)).Returns(true);
            A.CallTo(() => _jobRepository.UpdateJob(job)).Returns(true);

            //Act
            var controller = new JobController(_jobRepository, _companyRepository, _mapper);
            var result = controller.UpdateJob(1, job);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<NoContentResult>().Which.StatusCode.Should().Be(204);

        }

        [Fact]
        public void JobController_SortJobs_ReturnOK()
        {
            var jobs = A.Fake<ICollection<JobDto>>();
            var jobsList = A.Fake<List<JobDto>>();


            A.CallTo(() => _mapper.Map<List<JobDto>>(jobs)).Returns(jobsList);
            A.CallTo(() => _jobRepository.SortJobs(jobs, JobSort.expSort)).Returns(jobs);

            var controller = new JobController(_jobRepository, _companyRepository, _mapper);
            var result = controller.SortFilterJobs(JobSort.expSort);

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
        }
    }
}

