using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestHR.AdminCenter;
using TestHR.Entities;

namespace TestHR.Web.Areas.Admin.Models
{
    public class PositionModel
    {
        private CompanyManagementService _companyManagementService { get; set; }
        private PositionManagementService _positionManagementService { get; set; }
        private DepartmentManagementService _departmentManagementService { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Company> Companies { get; set; }
        public Guid CompanyId { get; set; }

        public List<Department> Departments { get; set; }
        public Guid DepartmentId { get; set; }
        public List<Position> ReportingPositions { get; set; } 
        public Guid ReportingPositionId { get; set; }

        public PositionModel()
        {
            _positionManagementService = new PositionManagementService();
            _departmentManagementService = new DepartmentManagementService();
            _companyManagementService = new CompanyManagementService();
            Companies = GetAllCompanies();
            ReportingPositions = GetAllPositions();
            Departments = GetAllDepartments();
        }

        private List<Department> GetAllDepartments()
        {
            return _departmentManagementService.GelAllDepartments();
        }

        public List<Position> GetAllPositions()
        {
            return _positionManagementService.GelAllPositions();
        }

        public List<Company> GetAllCompanies()
        {
            return _companyManagementService.GetAllCompanies();
        }
          public PositionModel(Guid id) : this()
        {
            var position=_positionManagementService.GetPosition(id);

            this.Id = position.Id;
            this.Name = position.Name;
            if (position.Company != null)
            {
                this.CompanyId = position.Company.Id;
            }
            if (position.ReportingPosition != null)
            {
                this.ReportingPositionId = position.ReportingPosition.Id;
            }  
              if (position.Department != null)
            {
                this.DepartmentId = position.Department.Id;
            }
        }
          public void AddPosition()
          {
              _positionManagementService.AddPosition(Name, CompanyId, DepartmentId, ReportingPositionId);
          }

          internal void DeletePosition(Guid? id)
          {
              if (id.HasValue)
              {
                  _positionManagementService.DeletePosition(id.Value);
              }
              else
              {
                  throw new Exception();
              }

          }

          public Position LoadPositionData(Guid? id)
          {
              if (id.HasValue)
              {
                  return _positionManagementService.GetPosition(id.Value);
              }
              else
              {
                  throw new Exception();
              }

          }

    }
}