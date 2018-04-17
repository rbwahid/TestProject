namespace TestHR.AdminCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Updatefirst : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Department", name: "Company_Id", newName: "CompanyId");
            RenameColumn(table: "dbo.Department", name: "DepartmentHead_Id", newName: "DepartmentHeadId");
            RenameColumn(table: "dbo.Position", name: "Company_Id", newName: "CompanyId");
            RenameColumn(table: "dbo.Position", name: "Department_Id", newName: "DepartmentId");
            RenameColumn(table: "dbo.Position", name: "ReportingPosition_Id", newName: "ReportPositionId");
            RenameIndex(table: "dbo.Department", name: "IX_Company_Id", newName: "IX_CompanyId");
            RenameIndex(table: "dbo.Department", name: "IX_DepartmentHead_Id", newName: "IX_DepartmentHeadId");
            RenameIndex(table: "dbo.Position", name: "IX_Company_Id", newName: "IX_CompanyId");
            RenameIndex(table: "dbo.Position", name: "IX_Department_Id", newName: "IX_DepartmentId");
            RenameIndex(table: "dbo.Position", name: "IX_ReportingPosition_Id", newName: "IX_ReportPositionId");
            AddColumn("dbo.Department", "Employee_Id", c => c.Guid());
            CreateIndex("dbo.Department", "Employee_Id");
            AddForeignKey("dbo.Department", "Employee_Id", "dbo.Employee", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Department", "Employee_Id", "dbo.Employee");
            DropIndex("dbo.Department", new[] { "Employee_Id" });
            DropColumn("dbo.Department", "Employee_Id");
            RenameIndex(table: "dbo.Position", name: "IX_ReportPositionId", newName: "IX_ReportingPosition_Id");
            RenameIndex(table: "dbo.Position", name: "IX_DepartmentId", newName: "IX_Department_Id");
            RenameIndex(table: "dbo.Position", name: "IX_CompanyId", newName: "IX_Company_Id");
            RenameIndex(table: "dbo.Department", name: "IX_DepartmentHeadId", newName: "IX_DepartmentHead_Id");
            RenameIndex(table: "dbo.Department", name: "IX_CompanyId", newName: "IX_Company_Id");
            RenameColumn(table: "dbo.Position", name: "ReportPositionId", newName: "ReportingPosition_Id");
            RenameColumn(table: "dbo.Position", name: "DepartmentId", newName: "Department_Id");
            RenameColumn(table: "dbo.Position", name: "CompanyId", newName: "Company_Id");
            RenameColumn(table: "dbo.Department", name: "DepartmentHeadId", newName: "DepartmentHead_Id");
            RenameColumn(table: "dbo.Department", name: "CompanyId", newName: "Company_Id");
        }
    }
}
