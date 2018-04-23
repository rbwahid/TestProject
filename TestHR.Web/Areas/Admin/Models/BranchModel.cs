using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestHR.AdminCenter;
using TestHR.Entities;

namespace TestHR.Web.Areas.Admin.Models
{
    public class BranchModel
    {
        private CompanyManagementService _companyManagementService { get; set; }
        private BranchManagementService _branchManagementService { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Company> Company { get; set; }
        public Guid CompanyId { get; set; }
        public string Description { get; set; }

        public BranchModel()
        {
            _companyManagementService = new CompanyManagementService();
            _branchManagementService = new BranchManagementService();
            Company = GetAllCompanies();
        }
        public List<Company> GetAllCompanies()
        {
           
            return _companyManagementService.GetAllCompanies();
        }

        public List<Branch> GetAllBranches()
        {

            return _branchManagementService.GetAllBranches();
        }

        public BranchModel(Guid id) : this()
        {
            var branch=_branchManagementService.GetBranch(id);

            this.Id = branch.Id;
            this.Name = branch.Name;
            if (branch.Company!=null)
            {
                this.CompanyId = branch.Company.Id;
            }
            this.Description = branch.Description;
       
            
        }
        public void AddBranch()
        {
            _branchManagementService.AddBranch(Name, CompanyId, Description);
        }
        public void EditBranch(Guid id)
        {
            _branchManagementService.EditBranch(id,Name, CompanyId, Description);
        }

        internal void DeleteBranch(Guid? id)
        {
            if (id.HasValue)
            {
                _branchManagementService.DeleteBranch(id.Value);
            }                
            else
            {
                throw new Exception();
            }
                
        }

        public Branch LoadBranchData(Guid? id)
        {
            if (id.HasValue)
            {
                return _branchManagementService.GetBranch(id.Value);
            }
            else
            {
                throw new Exception();
            }
                
        }
    }
}