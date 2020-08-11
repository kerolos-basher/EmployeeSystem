using Idintitycorepro.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Idintitycorepro.Data
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions <ApplicationDbContext> option):base(option)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        //علشان الفى ال cascade
        // محدش يقدر يمسح الرول من غير ميمسح اليوزر الى فيها الاول
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var forignkey  in builder.Model.GetEntityTypes().SelectMany(e=>e.GetForeignKeys()))
            {
                forignkey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
