﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PEC.ContextProceduress
{
    public partial class ADSCentralContext : DbContext
    {
        public ADSCentralContext()
        {
        }

        public ADSCentralContext(DbContextOptions<ADSCentralContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Especialidade> Especialidade { get; set; }
        public virtual DbSet<VwEspecialidadeConvenioChat> VwEspecialidadeConvenioChat { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Especialidade>(entity =>
            {
                entity.HasKey(e => e.IdEspecialidade)
                    .HasName("PK_Especialidade_1");

                entity.HasIndex(e => e.NmEspecialidade)
                    .HasName("IX_Especialidade");

                entity.Property(e => e.Cbo).IsUnicode(false);

                entity.Property(e => e.CdAmb).IsUnicode(false);

                entity.Property(e => e.CodigoReceita).IsUnicode(false);

                entity.Property(e => e.NmEspecialidade).IsUnicode(false);
            });

            modelBuilder.Entity<VwEspecialidadeConvenioChat>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Especialidade_ConvenioChat");

                entity.Property(e => e.Especialidade).IsUnicode(false);

                entity.Property(e => e.NmConvenio).IsUnicode(false);

                entity.Property(e => e.NmEspecialidade).IsUnicode(false);
            });

            OnModelCreatingGeneratedProcedures(modelBuilder);
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}