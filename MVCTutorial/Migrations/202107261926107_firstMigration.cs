namespace MVCTutorial.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Speciality = c.String(),
                        HospitalId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hospitals", t => t.HospitalId)
                .Index(t => t.HospitalId);
            
            CreateTable(
                "dbo.Hospitals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        Mobile = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Mobile = c.String(),
                        Ailment = c.String(),
                        Email = c.String(),
                        HospitalId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hospitals", t => t.HospitalId)
                .Index(t => t.HospitalId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "HospitalId", "dbo.Hospitals");
            DropForeignKey("dbo.Doctors", "HospitalId", "dbo.Hospitals");
            DropIndex("dbo.Patients", new[] { "HospitalId" });
            DropIndex("dbo.Doctors", new[] { "HospitalId" });
            DropTable("dbo.Patients");
            DropTable("dbo.Hospitals");
            DropTable("dbo.Doctors");
        }
    }
}
