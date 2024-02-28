namespace MusicSchedule_PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LessonStudent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Lessons", "mStudentID", c => c.Int(nullable: false));
            CreateIndex("dbo.Lessons", "mStudentID");
            AddForeignKey("dbo.Lessons", "mStudentID", "dbo.MusicStudents", "mStudentID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lessons", "mStudentID", "dbo.MusicStudents");
            DropIndex("dbo.Lessons", new[] { "mStudentID" });
            DropColumn("dbo.Lessons", "mStudentID");
        }
    }
}
