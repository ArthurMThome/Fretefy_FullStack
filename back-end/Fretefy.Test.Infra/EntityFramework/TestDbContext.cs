using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Infra.EntityFramework.Mappings;
using Microsoft.EntityFrameworkCore;
using System;

namespace Fretefy.Test.Infra.EntityFramework
{
    public class TestDbContext : DbContext
    {
        public TestDbContext()
        {

        }

        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RegiaoMap());
            modelBuilder.ApplyConfiguration(new CidadeMap());

            modelBuilder.Entity<Cidade>()
            .HasOne(c => c.Regiao)
            .WithMany(r => r.Cidades)
            .HasForeignKey(c => c.RegiaoId);

            var result = modelBuilder.Entity<Regiao>().HasData(
                new Regiao { Id = new Guid("afc12ff5-eacd-420a-835a-69e9b3b78111"), Nome = "Sul" }
                );        

            modelBuilder.Entity<Cidade>().HasData(
                new Cidade { Id = new Guid("afc12ff5-eacd-420a-835a-69e9b3b78222"), Nome = "Curitiba", UF = "PR", RegiaoId = new Guid("afc12ff5-eacd-420a-835a-69e9b3b78111") }
                );
        }
    }
}
