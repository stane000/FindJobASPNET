
using FindJobWebAPI.Models;

namespace FindJobWebAPI.Interfaces
{
    public interface ICompanyInterface
    {
        ICollection<Company> GetCompanies();

        Company GetCompanyById(int id);

        public bool DeleteCompany(Company company);

        public bool CompanyExists(int companyId);

        public bool CreateCompany(Company company);

        public bool UpdateCompany(Company company);

        public bool Save();
    }
}
