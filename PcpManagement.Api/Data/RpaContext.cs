using Microsoft.EntityFrameworkCore;
using PcpManagement.Core.Models;

namespace PcpManagement.Api.Data;

public partial class RpaContext(DbContextOptions<RpaContext> options) : DbContext(options)
{
    
    public virtual DbSet<Legados> Legados { get; set; } = null!;

    public virtual DbSet<Robo> Robos { get; set; } = null!;

    public virtual DbSet<RpaVm> RpaVms { get; set; } = null!;

    public virtual DbSet<RpaVmsLegado> RpaVmsLegados { get; set; } = null!;
    public virtual DbSet<Vm> Vms { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Legados>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__legados__3213E83F68B18CC4");
        });

        modelBuilder.Entity<Robo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__robos__3213E83F31A00093");
        });

        modelBuilder.Entity<RpaVm>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__rpaVms__3213E83F4C0523CD");

            entity.HasOne(d => d.IdProjetoFkNavigation).WithMany(p => p.RpaVms).HasConstraintName("FK__rpaVms__idProjet__44FF419A");

            entity.HasOne(d => d.IdVmfkNavigation).WithMany(p => p.RpaVms).HasConstraintName("FK__rpaVms__idVMFK__45F365D3");
        });

        modelBuilder.Entity<RpaVmsLegado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__rpaVmsLe__3213E83F3530858C");

            entity.HasOne(d => d.IdLegadoFkNavigation).WithMany(p => p.RpaVmsLegados).HasConstraintName("FK__rpaVmsLeg__idLeg__46E78A0C");

            entity.HasOne(d => d.IdRpaVmFkNavigation).WithMany(p => p.RpaVmsLegados).HasConstraintName("FK__rpaVmsLeg__idRpa__47DBAE45");
        });

        modelBuilder.Entity<Vm>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__vms__3213E83F1E54293A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
