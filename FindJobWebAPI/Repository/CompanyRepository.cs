using FindJobWebAPI.Data;
using FindJobWebAPI.Interfaces;
using FindJobWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace FindJobWebAPI.Repository
{
    public class CompanyRepository: ICompanyInterface
    {
        readonly DataContext _context;
        public CompanyRepository(DataContext context) 
        {
            _context = context;
        }

        public ICollection<Company> GetCompanies() 
        {
            return _context.Companies.Include(c => c.Workers).Include(c => c.Jobs).OrderBy(x => x.Id).ToList(); 

        }

        public Company GetCompanyById(int id)
        {
            return _context.Companies.Include(c => c.Workers).Include(c => c.Jobs).FirstOrDefault(w => w.Id == id);
        }

        public bool CreateCompany(Company company)
        {
            _context.Add(company);
            return Save();
        }

        public bool UpdateCompany(Company company)
        {
            if (company.Workers.Any())
            {

                foreach (var worker in company.Workers)
                {
                    if (!_context.Workers.Any(w => w.Id == worker.Id))
                    {
                        _context.Add(worker);
                    }
                }
            }

            if (company.Jobs.Any())
            {
                foreach (var job in company.Jobs)
                {
                    if (!_context.Jobs.Any(w => w.Id == job.Id))
                    {
                        _context.Add(job);
                    }
                }
            }

            _context.Update(company);
            return Save();
        }

        public bool DeleteCompany(Company company)
        {
            _context.Remove(company);
            return Save();
        }

        public bool CompanyExists(int companyId)
        {
            return _context.Companies.Any(w => w.Id == companyId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
