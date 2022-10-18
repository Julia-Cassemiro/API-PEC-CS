using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PEC.Models;

namespace PEC.Data
{
    public class PECContext : DbContext
    {
        public PECContext (DbContextOptions<PECContext> options)
            : base(options)
        {
        }

        public DbSet<PEC.Models.Usuarios_Repres> Usuarios { get; set; }
        public IEnumerable<object> usuario { get; internal set; }
    }
}
