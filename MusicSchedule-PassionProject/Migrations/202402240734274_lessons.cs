namespace MusicSchedule_PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lessons : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lessons",
                c => new
                    {
                        LessonId = c.Int(nullable: false, identity: true),
                        LessonName = c.String(),
                        LessonDescription = c.String(),
                        LessonTime = c.DateTime(nullable: false),
                        LessonInstructor = c.String(),
                    })
                .PrimaryKey(t => t.LessonId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Lessons");
        }
    }
}
