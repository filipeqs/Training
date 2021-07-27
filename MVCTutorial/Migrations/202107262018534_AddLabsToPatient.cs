namespace MVCTutorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLabsToPatient : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PatientLabs",
                c => new
                    {
                        Patient_Id = c.Int(nullable: false),
                        Lab_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Patient_Id, t.Lab_Id })
                .ForeignKey("dbo.Patients", t => t.Patient_Id, cascadeDelete: true)
                .ForeignKey("dbo.Labs", t => t.Lab_Id, cascadeDelete: true)
                .Index(t => t.Patient_Id)
                .Index(t => t.Lab_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PatientLabs", "Lab_Id", "dbo.Labs");
            DropForeignKey("dbo.PatientLabs", "Patient_Id", "dbo.Patients");
            DropIndex("dbo.PatientLabs", new[] { "Lab_Id" });
            DropIndex("dbo.PatientLabs", new[] { "Patient_Id" });
            DropTable("dbo.PatientLabs");
        }
    }
}
