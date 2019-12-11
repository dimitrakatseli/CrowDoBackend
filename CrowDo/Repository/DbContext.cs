using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowDo.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrowDo
{
    class CrowDoDB : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Funding> Fundings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connString1 = "Server =localhost ; Database = CrowDo; Integrated Security = SSPI; Persist Security Info=False;";
            optionsBuilder.UseSqlServer(connString1);
        }

    }

}
