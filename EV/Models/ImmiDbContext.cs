using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EV.Models
{
    public class ImmiDbContext : DbContext
    {
        public ImmiDbContext()
        {

        }
        public DbSet<Countries> Countries { get; set; }
        public DbSet<Immigrants> Immigrants { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}