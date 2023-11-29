using AutoMapper;
using FakeItEasy;
using FindJobWebAPI.Controllers;
using FindJobWebAPI.Dto;
using FindJobWebAPI.Interfaces;
using FindJobWebAPI.Models;
using FindJobWebAPI.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Npgsql.PostgresTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1.Controller
{
    public class WorkerControllerTest
    {
        private readonly IWorkerInterface _workerRepository;
        private readonly ICompanyInterface _companyRepository;
        private IMapper _mapper;

        public WorkerControllerTest()
        {
            _workerRepository = A.Fake<IWorkerInterface>();
            _companyRepository = A.Fake<ICompanyInterface>();
            _mapper = A.Fake<Mapper>();
        }

        //[Fact]
        //public void WorkerController_GetWorkers_ReturnOK()
        //{
        //    var worekrs = A.Fake<ICollection<WorkerDto>>();
        //    var workerList = A.Fake<List<WorkerDto>>();

        //    A.CallTo(() => _mapper.Map<List<JobDto>>(workers)).Returns(jobsList);
        //    var controller = new WorkerController(_workerRepository, _companyRepository, _mapper);

        //    //Act
        //    var result = controller.GetJobs();

        //    //Assert
        //    result.Should().NotBeNull();
        //    result.Should().BeOfType(typeof(OkObjectResult));

        //}
    }
}

