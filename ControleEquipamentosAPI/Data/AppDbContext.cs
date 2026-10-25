
using ControleEquipamentosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleEquipamentosAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Equipamento> Equipamentos =>
        Set<Equipamento>();

    public DbSet<Manutencao> Manutencoes =>
        Set<Manutencao>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equipamento>()
            .HasMany(e => e.Manutencoes)
            .WithOne(m => m.Equipamento)
            .HasForeignKey(m => m.EquipamentoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
