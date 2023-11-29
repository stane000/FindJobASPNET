//using AutoFixture;
using Bogus;
using Bogus.DataSets;
using FindJobWebAPI.Data;
using FindJobWebAPI.Models;
using FindJobWebAPI.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using Xunit;

namespace TestIntegrationProject
{
    public class WorkerRepositoryTests
    {

        private WorkerRepository _workerRepository;
        private CompanyRepository _companyRepository;

        public WorkerRepositoryTests()
        {
            IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            var _context = new DataContext(optionsBuilder.Options);
            _workerRepository = new WorkerRepository(_context);
            _companyRepository = new CompanyRepository(_context);

        }

        private int GetRandomWorkerId()
        {
            var workers = _workerRepository.GetWorkers().ToList();
            var ids = workers.Select(w => w.Id).ToList();
            return ids[new Random().Next(0, ids.Count - 1)];
        }

        [Fact]
        public void WorkerRepositoryGetAll()
        {
            for (int i = 0; i <= 10; i++)
            {
                var workers = _workerRepository.GetWorkers().ToList();
                workers.Should().NotBeEmpty();
            }
        }

        [Fact]
        public void WorkerRepositoryGetById()
        {
            int id = this.GetRandomWorkerId();
            var worker = _workerRepository.GetWorkerById(id);
            worker.Should().NotBeNull();
            worker.Should().BeOfType<Worker>();
        }

        [Fact]
        public void WorkerRepositoryWorkerExists()
        {
            for (int i = 0; i <= 10; i++)
            {
                int id = this.GetRandomWorkerId();
                bool exists = _workerRepository.WorkerExists(id);
                exists.Should().BeTrue("WorkerExits didn't find user with id:" + id);
            }
        }

        [Fact]
        public void WorkerRepositoryWorker_NotExists()
        {
            for (int i = 0; i <= 10; i++)
            {
                bool exists = _workerRepository.WorkerExists(-1);
                exists.Should().BeFalse();
            }
        }

        [Fact]
        public void WorkerRepositoryWorker_CreateWorker()
        {
            var companies = _companyRepository.GetCompanies().ToList();
            var companyId = new Random().Next(companies.MinBy(w => w.Id).Id, companies.MaxBy(w => w.Id).Id);
            var faker = new Faker<Worker>().RuleFor(w => w.CompanyId, f => companyId)
                                            .RuleFor(w => w.FirstName, f => f.Person.FirstName)
                                            .RuleFor(w => w.Age, f => f.Random.Int(0, 100))
                                            .RuleFor(w => w.LastName, f => f.Person.LastName)
                                            .RuleFor(w => w.Experience, f => f.Random.Int(0, 35));


            var worker = faker.Generate();
            bool result = _workerRepository.CreateWorker(worker);
            result.Should().BeTrue("Create worker didn't create worker: " + worker);
        }

        [Fact]
        public void WorkerRepositoryWorker_DeleteWorker()
        {
            int workersCount = _workerRepository.GetWorkers().Count();
            Worker worker = _workerRepository.GetWorkerById(this.GetRandomWorkerId());
            bool result = _workerRepository.DeleteWorker(worker);
            result.Should().BeTrue();
            int workersCountAfter = _workerRepository.GetWorkers().Count();
            int diff = workersCount - workersCountAfter;
            diff.Should().Be(1, "Number of worker should be reduced by 1, but was reduced by: " + diff);
        }
    }
}