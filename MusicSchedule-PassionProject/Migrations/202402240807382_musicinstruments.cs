namespace MusicSchedule_PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class musicinstruments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MusicInstruments",
                c => new
                    {
                        InstrumentID = c.Int(nullable: false, identity: true),
                        InstrumentType = c.String(),
                    })
                .PrimaryKey(t => t.InstrumentID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MusicInstruments");
        }
    }
}
