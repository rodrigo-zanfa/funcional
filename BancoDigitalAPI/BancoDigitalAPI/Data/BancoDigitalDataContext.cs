using BancoDigitalAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoDigitalAPI.Data
{
    public class BancoDigitalDataContext : DbContext
    {
        public BancoDigitalDataContext(DbContextOptions<BancoDigitalDataContext> options)
            : base(options)
        {

        }

        public DbSet<Correntista> Correntistas { get; set; }
        public DbSet<ContaCorrente> ContasCorrentes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Correntista>()
                .HasMany(t => t.ContasCorrentes)
                .WithOne(t => t.Correntista)
                .HasForeignKey(t => t.CorrentistaCpf);

            modelBuilder.Entity<Correntista>()
                .HasData(
                    new Correntista { Cpf = "32516143800", Nome = "Rodrigo Zanferrari" },
                    new Correntista { Cpf = "33713557802", Nome = "Raphaela Goes" }
            );

            modelBuilder.Entity<ContaCorrente>()
                .HasData(
                    new ContaCorrente { Numero = 1, Saldo = 0.00, CorrentistaCpf = "32516143800" },
                    new ContaCorrente { Numero = 2, Saldo = 0.00, CorrentistaCpf = "33713557802" },
                    new ContaCorrente { Numero = 3, Saldo = 0.00, CorrentistaCpf = "32516143800" }
                );
        }
    }
}
