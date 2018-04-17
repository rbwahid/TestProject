namespace TestHR.AdminCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Branch",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Company_Id = c.Guid(),
                        Name = c.String(),
                        Description = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.Company_Id)
                .Index(t => t.Company_Id);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        Fax = c.String(),
                        Email = c.String(),
                        ContactPerson = c.String(),
                        ContactPersonEmail = c.String(),
                        ContactPersonPhone = c.String(),
                        FiscalYearStart = c.DateTime(),
                        CompanyLogo = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        MotherCompany_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.MotherCompany_Id)
                .Index(t => t.MotherCompany_Id);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Company_Id = c.Guid(),
                        DepartmentHead_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.Company_Id)
                .ForeignKey("dbo.Employee", t => t.DepartmentHead_Id)
                .Index(t => t.Company_Id)
                .Index(t => t.DepartmentHead_Id);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        FathersName = c.String(),
                        MothersName = c.String(),
                        SouseName = c.String(),
                        PhoneNumber = c.String(),
                        PresentAddress = c.String(),
                        PernamentAddress = c.String(),
                        Email = c.String(),
                        Religion = c.String(),
                        Nationality = c.String(),
                        Nid = c.String(),
                        PassportNo = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Branch_Id = c.Guid(),
                        Company_Id = c.Guid(),
                        Department_Id = c.Guid(),
                        Position_Id = c.Guid(),
                        Department_Id1 = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branch", t => t.Branch_Id)
                .ForeignKey("dbo.Company", t => t.Company_Id)
                .ForeignKey("dbo.Department", t => t.Department_Id)
                .ForeignKey("dbo.Position", t => t.Position_Id)
                .ForeignKey("dbo.Department", t => t.Department_Id1)
                .Index(t => t.Branch_Id)
                .Index(t => t.Company_Id)
                .Index(t => t.Department_Id)
                .Index(t => t.Position_Id)
                .Index(t => t.Department_Id1);
            
            CreateTable(
                "dbo.EmployeeCareerHistory",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CompanyName = c.String(),
                        CompanyAddress = c.String(),
                        Department = c.String(),
                        Position = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Employee_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employee", t => t.Employee_Id)
                .Index(t => t.Employee_Id);
            
            CreateTable(
                "dbo.EmployeeEducationHistory",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Degree = c.String(),
                        InstituteName = c.String(),
                        Subject = c.String(),
                        PassingYear = c.DateTime(nullable: false),
                        Result = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Employee_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employee", t => t.Employee_Id)
                .Index(t => t.Employee_Id);
            
            CreateTable(
                "dbo.Position",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Company_Id = c.Guid(),
                        Department_Id = c.Guid(),
                        ReportingPosition_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.Company_Id)
                .ForeignKey("dbo.Department", t => t.Department_Id)
                .ForeignKey("dbo.Position", t => t.ReportingPosition_Id)
                .Index(t => t.Company_Id)
                .Index(t => t.Department_Id)
                .Index(t => t.ReportingPosition_Id);
            
            CreateTable(
                "dbo.Holiday",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Description = c.String(),
                        DateFrom = c.DateTime(nullable: false),
                        DateTo = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LeaveApplication",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        DateFrom = c.DateTime(nullable: false),
                        DateTo = c.DateTime(nullable: false),
                        ReasonForLeave = c.String(),
                        AddressDuringLeave = c.String(),
                        ContactDuringLeave = c.String(),
                        Status = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Applicant_Id = c.Guid(nullable: false),
                        Approver_Id = c.Guid(),
                        LeaveType_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employee", t => t.Applicant_Id, cascadeDelete: true)
                .ForeignKey("dbo.Employee", t => t.Approver_Id)
                .ForeignKey("dbo.LeaveType", t => t.LeaveType_Id)
                .Index(t => t.Applicant_Id)
                .Index(t => t.Approver_Id)
                .Index(t => t.LeaveType_Id);
            
            CreateTable(
                "dbo.LeaveComment",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CommentDate = c.DateTime(nullable: false),
                        Comment = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                        Employee_Id = c.Guid(),
                        LeaveApplication_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employee", t => t.Employee_Id)
                .ForeignKey("dbo.LeaveApplication", t => t.LeaveApplication_Id)
                .Index(t => t.Employee_Id)
                .Index(t => t.LeaveApplication_Id);
            
            CreateTable(
                "dbo.LeaveType",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        NumberOfDays = c.Int(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shift",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Code = c.String(),
                        Type = c.String(),
                        Description = c.String(),
                        IsDefault = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        OfficeHourDescription = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        UpdatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LeaveApplication", "LeaveType_Id", "dbo.LeaveType");
            DropForeignKey("dbo.LeaveComment", "LeaveApplication_Id", "dbo.LeaveApplication");
            DropForeignKey("dbo.LeaveComment", "Employee_Id", "dbo.Employee");
            DropForeignKey("dbo.LeaveApplication", "Approver_Id", "dbo.Employee");
            DropForeignKey("dbo.LeaveApplication", "Applicant_Id", "dbo.Employee");
            DropForeignKey("dbo.Employee", "Department_Id1", "dbo.Department");
            DropForeignKey("dbo.Department", "DepartmentHead_Id", "dbo.Employee");
            DropForeignKey("dbo.Employee", "Position_Id", "dbo.Position");
            DropForeignKey("dbo.Position", "ReportingPosition_Id", "dbo.Position");
            DropForeignKey("dbo.Position", "Department_Id", "dbo.Department");
            DropForeignKey("dbo.Position", "Company_Id", "dbo.Company");
            DropForeignKey("dbo.EmployeeEducationHistory", "Employee_Id", "dbo.Employee");
            DropForeignKey("dbo.EmployeeCareerHistory", "Employee_Id", "dbo.Employee");
            DropForeignKey("dbo.Employee", "Department_Id", "dbo.Department");
            DropForeignKey("dbo.Employee", "Company_Id", "dbo.Company");
            DropForeignKey("dbo.Employee", "Branch_Id", "dbo.Branch");
            DropForeignKey("dbo.Department", "Company_Id", "dbo.Company");
            DropForeignKey("dbo.Company", "MotherCompany_Id", "dbo.Company");
            DropForeignKey("dbo.Branch", "CompanyId", "dbo.Company");
            DropIndex("dbo.LeaveComment", new[] { "LeaveApplication_Id" });
            DropIndex("dbo.LeaveComment", new[] { "Employee_Id" });
            DropIndex("dbo.LeaveApplication", new[] { "LeaveType_Id" });
            DropIndex("dbo.LeaveApplication", new[] { "Approver_Id" });
            DropIndex("dbo.LeaveApplication", new[] { "Applicant_Id" });
            DropIndex("dbo.Position", new[] { "ReportingPosition_Id" });
            DropIndex("dbo.Position", new[] { "Department_Id" });
            DropIndex("dbo.Position", new[] { "Company_Id" });
            DropIndex("dbo.EmployeeEducationHistory", new[] { "Employee_Id" });
            DropIndex("dbo.EmployeeCareerHistory", new[] { "Employee_Id" });
            DropIndex("dbo.Employee", new[] { "Department_Id1" });
            DropIndex("dbo.Employee", new[] { "Position_Id" });
            DropIndex("dbo.Employee", new[] { "Department_Id" });
            DropIndex("dbo.Employee", new[] { "Company_Id" });
            DropIndex("dbo.Employee", new[] { "Branch_Id" });
            DropIndex("dbo.Department", new[] { "DepartmentHead_Id" });
            DropIndex("dbo.Department", new[] { "Company_Id" });
            DropIndex("dbo.Company", new[] { "MotherCompany_Id" });
            DropIndex("dbo.Branch", new[] { "CompanyId" });
            DropTable("dbo.Shift");
            DropTable("dbo.LeaveType");
            DropTable("dbo.LeaveComment");
            DropTable("dbo.LeaveApplication");
            DropTable("dbo.Holiday");
            DropTable("dbo.Position");
            DropTable("dbo.EmployeeEducationHistory");
            DropTable("dbo.EmployeeCareerHistory");
            DropTable("dbo.Employee");
            DropTable("dbo.Department");
            DropTable("dbo.Company");
            DropTable("dbo.Branch");
        }
    }
}
