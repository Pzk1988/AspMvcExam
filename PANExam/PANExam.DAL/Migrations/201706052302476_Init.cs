namespace PANExam.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(),
                        Ects = c.Int(nullable: false),
                        Department_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Department", t => t.Department_Id)
                .Index(t => t.Department_Id);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Emblem = c.Binary(storeType: "image"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LastName = c.String(),
                        FirstName = c.String(),
                        Department_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Department", t => t.Department_Id)
                .Index(t => t.Department_Id);
            
            CreateTable(
                "dbo.Enrollment",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Grade = c.Int(nullable: false),
                        Course_Id = c.Guid(),
                        Student_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Course", t => t.Course_Id)
                .ForeignKey("dbo.Student", t => t.Student_Id)
                .Index(t => t.Course_Id)
                .Index(t => t.Student_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Student", "Department_Id", "dbo.Department");
            DropForeignKey("dbo.Enrollment", "Student_Id", "dbo.Student");
            DropForeignKey("dbo.Enrollment", "Course_Id", "dbo.Course");
            DropForeignKey("dbo.Course", "Department_Id", "dbo.Department");
            DropIndex("dbo.Enrollment", new[] { "Student_Id" });
            DropIndex("dbo.Enrollment", new[] { "Course_Id" });
            DropIndex("dbo.Student", new[] { "Department_Id" });
            DropIndex("dbo.Course", new[] { "Department_Id" });
            DropTable("dbo.Enrollment");
            DropTable("dbo.Student");
            DropTable("dbo.Department");
            DropTable("dbo.Course");
        }
    }
}
