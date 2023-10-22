using Desafio_Balta.Models;
using Microsoft.EntityFrameworkCore;

namespace Desafio_Balta.Data;

public partial class DadosIbgeContext : DbContext
{
    public DadosIbgeContext()
    {
    }

    public DadosIbgeContext(DbContextOptions<DadosIbgeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ibge> Ibges { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ibge>(entity =>
        {
            entity.ToTable("IBGE");

            entity.HasIndex(e => e.City, "IX_IBGE_City");

            entity.HasIndex(e => e.Id, "IX_IBGE_Id");

            entity.HasIndex(e => e.State, "IX_IBGE_State");

            entity.Property(e => e.Id)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.City).HasMaxLength(80);
            entity.Property(e => e.State)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
