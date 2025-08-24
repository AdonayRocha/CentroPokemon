using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data;
public class PokemonDbContext : DbContext
{
    public PokemonDbContext(DbContextOptions<PokemonDbContext> options) : base(options) { }
    public DbSet<PokemonManaged> Pokemons => Set<PokemonManaged>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PokemonManaged>(e =>
        {
            e.ToTable("POKEMONS");
            e.HasKey(x => x.Id);
            e.Property(x => x.Id).HasColumnName("ID");
            e.Property(x => x.ExternalId).HasColumnName("EXTERNAL_ID");
            e.Property(x => x.Name).HasColumnName("NAME").HasMaxLength(100).IsRequired();
            e.Property(x => x.TypesCsv).HasColumnName("TYPES_CSV").HasMaxLength(200);
            e.Property(x => x.BaseHp).HasColumnName("BASE_HP");
            e.Property(x => x.Health).HasColumnName("HEALTH");
            e.HasIndex(x => x.Name).IsUnique();
        });
    }
}