namespace MusicSchedule_PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class studentinstrument : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MusicStudents", "InstrumentID", c => c.Int(nullable: false));
            CreateIndex("dbo.MusicStudents", "InstrumentID");
            AddForeignKey("dbo.MusicStudents", "InstrumentID", "dbo.MusicInstruments", "InstrumentID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MusicStudents", "InstrumentID", "dbo.MusicInstruments");
            DropIndex("dbo.MusicStudents", new[] { "InstrumentID" });
            DropColumn("dbo.MusicStudents", "InstrumentID");
        }
    }
}
