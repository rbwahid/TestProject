using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Entities;

namespace TestHR.AdminCenter
{
    public class CompanyManagementService
    {
        private AdminCenterDbContext _context;
        private CompanyUnitOfWork _companyUnitOfWork;

        public CompanyManagementService()
        {
            _context = new AdminCenterDbContext();
            _companyUnitOfWork = new CompanyUnitOfWork(_context);
        }

        public List<Company> GetAllCompanies()
        {
            return _companyUnitOfWork.CompanyRepository.GetAll().ToList();
        }

        public Company GetCompany(Guid id)
        {
            return _companyUnitOfWork.CompanyRepository.GetById(id);
        }

         public void DeleteCompany(Guid id)
        {
            _companyUnitOfWork.CompanyRepository.DeleteById(id);
            _companyUnitOfWork.Save();
        }

        public int GetCount()
        {

            return _companyUnitOfWork.CompanyRepository.GetAll().Count();
            
        }

        public void EditCompany(Guid id,string name, Guid? motherCompanyId, string address, string phone, string fax, string email,
            string contactPerson, string contactPersonEmail, string contactPersonPhone, string fiscalYearStart)
        {
            var company = GetCompany(id);
            company.Name = name;
            if (company.MotherCompany != null)
            {
                company.MotherCompany = GetCompany(motherCompanyId.Value);
            }
            company.Address = address;
            company.Phone = phone;
            company.Email = email;
            company.Fax = fax;
            company.ContactPerson = contactPerson;
            company.ContactPersonEmail = contactPersonEmail;
            company.ContactPersonPhone = contactPersonPhone;
            company.FiscalYearStart = fiscalYearStart;
            _companyUnitOfWork.Save();
        }
        public void AddCompany(string name, Guid? motherCompanyId, string address, string phone, string fax, string email, 
            string contactPerson, string contactPersonEmail, string contactPersonPhone, string fiscalYearStart)
        {
            var company = new Company();

            company.Name = name;
            if (motherCompanyId.HasValue)
            {
                company.MotherCompany = _companyUnitOfWork.CompanyRepository.GetById(motherCompanyId.Value);
            }
            company.Address = address;
            company.Phone = phone;
            company.Fax = fax;
            company.Email = email;
            company.ContactPerson = contactPerson;
            company.ContactPersonEmail = contactPersonEmail;
            company.ContactPersonPhone = contactPersonPhone;
            company.FiscalYearStart = fiscalYearStart;

            _companyUnitOfWork.CompanyRepository.Add(company);
            _companyUnitOfWork.Save();

        }
    }
}
