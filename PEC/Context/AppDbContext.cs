using Microsoft.EntityFrameworkCore;
using PEC.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PEC.Context
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Usuarios> usuario { get; set; }
        public DbSet<Usuarios_Repres> usuario_Repres { get; set; }

        public DbSet<Menu> menu { get; set; }

        public DbSet<Menu_Usuario> menu_usuario { get; set; }

        public virtual DbSet<VwPecMenuUsuarios> VwPecMenuUsuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VwPecMenuUsuarios>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_PEC_Menu_Usuarios", "SIA");

                entity.Property(e => e.NmDescricao).IsUnicode(false);
            });

            modelBuilder.HasSequence<int>("Movimentacao_ContabilCounter", "SIA").StartsAt(0);

            modelBuilder.HasSequence("MovMateDsDoctoSeq", "SIA").StartsAt(200001);

            modelBuilder.HasSequence("Segreg_NR_DC_REC", "SIA");

            modelBuilder.HasSequence("SegregNrDcRecSeq", "SIA");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
