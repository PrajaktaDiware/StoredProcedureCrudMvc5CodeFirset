namespace CrudUsingSpMVC5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTable1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities1",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                        StateId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CityId);
            
            CreateTable(
                "dbo.States1",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                    })
                .PrimaryKey(t => t.StateId);
            
            AlterColumn("dbo.States", "State", c => c.Int(nullable: false));
            AlterColumn("dbo.Cities", "City", c => c.Int(nullable: false));
            AlterStoredProcedure(
                "dbo.CustomerVM_Insert",
                p => new
                    {
                        Name = p.String(),
                        Email = p.String(),
                        CurrentAddress = p.String(),
                        PermanantAddress = p.String(),
                        State = p.Int(),
                        City = p.Int(),
                    },
                body:
                    @"INSERT [dbo].[Customers]([Name], [Email])
                      VALUES (@Name, @Email)
                      
                      DECLARE @Id int
                      SELECT @Id = [Id]
                      FROM [dbo].[Customers]
                      WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
                      
                      INSERT [dbo].[Address]([Id], [CurrentAddress], [PermanantAddress])
                      VALUES (@Id, @CurrentAddress, @PermanantAddress)
                      
                      INSERT [dbo].[States]([Id], [State])
                      VALUES (@Id, @State)
                      
                      INSERT [dbo].[Cities]([Id], [City])
                      VALUES (@Id, @City)
                      
                      SELECT t0.[Id]
                      FROM [dbo].[Customers] AS t0
                      WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id"
            );
            
            AlterStoredProcedure(
                "dbo.CustomerVM_Update",
                p => new
                    {
                        Id = p.Int(),
                        Name = p.String(),
                        Email = p.String(),
                        CurrentAddress = p.String(),
                        PermanantAddress = p.String(),
                        State = p.Int(),
                        City = p.Int(),
                    },
                body:
                    @"UPDATE [dbo].[Customers]
                      SET [Name] = @Name, [Email] = @Email
                      WHERE ([Id] = @Id)
                      
                      UPDATE [dbo].[Address]
                      SET [CurrentAddress] = @CurrentAddress, [PermanantAddress] = @PermanantAddress
                      WHERE ([Id] = @Id)
                      AND @@ROWCOUNT > 0
                      
                      UPDATE [dbo].[States]
                      SET [State] = @State
                      WHERE ([Id] = @Id)
                      AND @@ROWCOUNT > 0
                      
                      UPDATE [dbo].[Cities]
                      SET [City] = @City
                      WHERE ([Id] = @Id)
                      AND @@ROWCOUNT > 0"
            );
            
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cities", "City", c => c.String());
            AlterColumn("dbo.States", "State", c => c.String());
            DropTable("dbo.States1");
            DropTable("dbo.Cities1");
            throw new NotSupportedException("Scaffolding create or alter procedure operations is not supported in down methods.");
        }
    }
}
