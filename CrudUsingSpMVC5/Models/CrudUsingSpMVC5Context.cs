using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CrudUsingSpMVC5.Models
{
    public class CrudUsingSpMVC5Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public CrudUsingSpMVC5Context() : base("name=MyConnection")
        {
            Database.Log = Console.WriteLine;
        }
        //public DbSet<Customer> Customers { get; set; }
        //public DbSet<Address> Addresses { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //  modelBuilder.Entity<CustomerViewModel>().MapToStoredProcedures();
            modelBuilder.Entity<CustomerVM>().Map(e =>
            {
                e.Properties(p => new { p.Name, p.Email });
                e.ToTable("Customers");
            }).Map(e =>
            {
                e.Properties(p => new { p.CurrentAddress, p.PermanantAddress });
                e.ToTable("Address");
            }).Map(e =>
            {
                e.Properties(p => new { p.State });
                e.ToTable("States");
            }).Map(e =>
            {
                e.Properties(p => new { p.City });
                e.ToTable("Cities");
            }).MapToStoredProcedures();
        }

        public System.Data.Entity.DbSet<CrudUsingSpMVC5.Models.CustomerVM> CustomerVMs { get; set; }
    }
}
