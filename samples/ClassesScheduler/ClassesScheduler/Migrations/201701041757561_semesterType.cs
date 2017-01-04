namespace ClassesScheduler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class semesterType : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Courses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        IsMandatorySubject = c.Boolean(nullable: false),
                        NumberOfCredits = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
