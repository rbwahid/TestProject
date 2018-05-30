using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Entities;

namespace TestHR.AdminCenter
{
    public class BranchManagementService
    {
        private AdminCenterDbContext _context;
        private BranchUnitOfWork _branchUnitOfWork;
        private CompanyUnitOfWork _companyUnitOfWork;
        private CompanyManagementService _companyManagementService;


        public BranchManagementService()
        {
            _context = new AdminCenterDbContext();
            _branchUnitOfWork = new BranchUnitOfWork(_context);
            _companyUnitOfWork = new CompanyUnitOfWork(_context);
        }
        public void AddToBranchExcel(string companyName, string name, string description)
        {
            var company = _companyUnitOfWork.CompanyRepository.GetAll().FirstOrDefault(e => e.Name == companyName);
            var branch = _branchUnitOfWork.BranchRepository.GetAll().FirstOrDefault(e => e.Name == name);
            if (branch == null)
            {
                if (company != null)
                {
                    AddBranch(name, company.Id, description);
                }
                else
                {
                    _companyManagementService = new CompanyManagementService();
                    _companyManagementService.AddCompany(companyName, Guid.Empty, string.Empty, string.Empty,
                        string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    AddBranch(name,
                        _companyUnitOfWork.CompanyRepository.GetAll().FirstOrDefault(e => e.Name == companyName).Id,
                        description);
                }
            }
        }
        public List<Branch> GetAllBranches()
        {
            return _branchUnitOfWork.BranchRepository.GetAll().ToList();
        }

        public Branch GetBranch(Guid id)
        {
            return _branchUnitOfWork.BranchRepository.GetById(id);
        }

        public void DeleteBranch(Guid id)
        {
            _branchUnitOfWork.BranchRepository.DeleteById(id);
            _branchUnitOfWork.Save();
        }

        public int GetCount()
        {

            return _branchUnitOfWork.BranchRepository.GetAll().Count();

        }
        public void AddBranch(string name, Guid CompanyId, string Description)
        {
            var branch = new Branch();

            branch.Name = name;
            branch.Company = _companyUnitOfWork.CompanyRepository.GetById(CompanyId);
            branch.Description = Description;

            _branchUnitOfWork.BranchRepository.Add(branch);
            _branchUnitOfWork.Save();

        }

        public void EditBranch(Guid id, string name, Guid CompanyId, string Description)
        {
            var branch = GetBranch(id);
            branch.Name = name;
            branch.Company = _companyUnitOfWork.CompanyRepository.GetById(CompanyId);
            branch.Description = Description;
            _branchUnitOfWork.Save();
        }
    }
}
