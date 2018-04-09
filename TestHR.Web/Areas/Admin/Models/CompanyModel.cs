using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestHR.Entities;
using TestHR.AdminCenter;

namespace TestHR.Web.Areas.Admin.Models
{
    public class CompanyModel
    {
        private CompanyManagementService _companyManagementService { get; set; }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Company> MotherCompanies { get; set; }
        public Guid MotherCompanyId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonPhone { get; set; }
        public DateTime? FiscalYearStart { get; set; }
        public string CompanyLogo { get; set; }

        public CompanyModel()
        {
            _companyManagementService = new CompanyManagementService();
            MotherCompanies = GetAllCompanies();
        }

        public List<Company> GetAllCompanies()
        {
            return _companyManagementService.GetAllCompanies();
        }

        public CompanyModel(Guid id) : this()
        {
            var company=_companyManagementService.GetCompany(id);

            this.Id = company.Id;
            this.Name = company.Name;
            if (company.MotherCompany!=null)
            {
                this.MotherCompanyId = company.MotherCompany.Id;
            }
            this.Address = company.Address;
            this.Phone = company.Address;
            this.Email = company.Address;
            this.Fax = company.Address;
            this.ContactPerson = company.Address;
            this.ContactPersonEmail = company.Address;
            this.ContactPersonPhone = company.ContactPersonPhone;
            this.FiscalYearStart = company.FiscalYearStart;
            
        }
        public void AddCompany()
        {
            _companyManagementService.AddCompany(Name, MotherCompanyId, Address, Phone, Fax, Email, ContactPerson, ContactPersonEmail, ContactPersonPhone, FiscalYearStart);
        }

        internal void DeleteCompany(Guid? id)
        {
            if (id.HasValue)
            {
                _companyManagementService.DeleteCompany(id.Value);
            }                
            else
            {
                throw new Exception();
            }
                
        }

        public Company LoadCompanyData(Guid? id)
        {
            if (id.HasValue)
            {
                return _companyManagementService.GetCompany(id.Value);
            }
            else
            {
                throw new Exception();
            }
                
        }
    }
}