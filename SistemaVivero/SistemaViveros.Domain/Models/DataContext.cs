using SistemaViveros.Common.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaViveros.Domain.Models
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
            
        }

        public DbSet<Wall> Walls { get; set; }
        public DbSet<Irrigation> Irrigations { get; set; }
        public DbSet<Weather> Weathers { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
