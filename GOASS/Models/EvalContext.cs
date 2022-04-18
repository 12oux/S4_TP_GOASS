using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GOASS.Models
{
    public class EvalContext : DbContext
    {
        public EvalContext(DbContextOptions<EvalContext> options) : base(options) { }

        public DbSet<Eval> Eval { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
