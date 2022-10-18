using Microsoft.EntityFrameworkCore;
using PEC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEC.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Usuarios> usuario { get; set; }
        public DbSet<Usuarios_Repres> usuario_Repres { get; set; }
    }
}
