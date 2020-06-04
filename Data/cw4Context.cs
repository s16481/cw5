using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using cw4.Models;

namespace cw4.Data
{
    public class cw4Context : DbContext
    {
        public cw4Context (DbContextOptions<cw4Context> options)
            : base(options)
        {
        }

        public DbSet<cw4.Models.Student> Student { get; set; }

        public DbSet<cw4.Models.Enrollment> Enrollment { get; set; }
        public DbSet<cw4.Models.Studies> Studies { get; set; }
    }
}
