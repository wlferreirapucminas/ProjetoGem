using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using Gem.API.Domain.Models;

namespace Gem.API.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Tipo> Tipos { get; set; }
        public DbSet<Entidade> Entidade { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<Tipo>().ToTable("Tipos");
            builder.Entity<Tipo>().HasKey(p => p.Id);
            builder.Entity<Tipo>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd().HasValueGenerator<InMemoryIntegerValueGenerator<int>>();
            builder.Entity<Tipo>().Property(p => p.Nome).IsRequired().HasMaxLength(50);
            builder.Entity<Tipo>().HasMany(p => p.Entidade).WithOne(p => p.Tipo).HasForeignKey(p => p.TipoId);

            builder.Entity<Tipo>().HasData
            (
                new Tipo { Id = 100, Nome = "Igreja" }, // Id setado manualmente por causa do banco na memória
                new Tipo { Id = 101, Nome = "Membro" }
            );

            builder.Entity<Entidade>().ToTable("Entidade");
            builder.Entity<Entidade>().HasKey(p => p.Id);
            builder.Entity<Entidade>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Entidade>().Property(p => p.Nome).IsRequired().HasMaxLength(50);
            builder.Entity<Entidade>().Property(p => p.Endereco).IsRequired().HasMaxLength(50);
            builder.Entity<Entidade>().Property(p => p.Cargo).IsRequired().HasMaxLength(50);
            builder.Entity<Entidade>().Property(p => p.Status).IsRequired();
            builder.Entity<Entidade>().Property(p => p.Email).IsRequired().HasMaxLength(50);

            builder.Entity<Entidade>().HasData
            (
                new Entidade
                {
                    Id = 100,
                    Nome = "Matriz",
                    Endereco = "Rua Principal, 123",
                    Cargo = "Igreja",
                    Status = EStatus.Ativo,
                    Email = "matriz@igreja.br",
                    TipoId = 100
                },
                new Entidade
                {
                    Id = 101,
                    Nome = "Matheus",
                    Endereco = "Rua de baixo, 123456",
                    Cargo = "Membro",
                    Status = EStatus.Ativo,
                    Email = "matheus@pucminas.br",
                    TipoId = 101,
                }
            );
        }
    }
}