namespace MusicSchedule_PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class musicstudents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MusicStudents",
                c => new
                    {
                        mStudentID = c.Int(nullable: false, identity: true),
                        mStudentFName = c.String(),
                        mStudentLName = c.String(),
                    })
                .PrimaryKey(t => t.mStudentID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MusicStudents");
        }
    }
}
