// <auto-generated />
using BancoDigitalAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BancoDigitalAPI.Migrations
{
    [DbContext(typeof(BancoDigitalDataContext))]
    [Migration("20210816224952_CreateDb")]
    partial class CreateDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("BancoDigitalAPI.Models.ContaCorrente", b =>
                {
                    b.Property<long>("Numero")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("CorrentistaCpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)");

                    b.Property<double>("Saldo")
                        .HasColumnType("double");

                    b.HasKey("Numero");

                    b.HasIndex("CorrentistaCpf");

                    b.ToTable("ContasCorrentes");

                    b.HasData(
                        new
                        {
                            Numero = 1L,
                            CorrentistaCpf = "32516143800",
                            Saldo = 0.0
                        },
                        new
                        {
                            Numero = 2L,
                            CorrentistaCpf = "33713557802",
                            Saldo = 0.0
                        },
                        new
                        {
                            Numero = 3L,
                            CorrentistaCpf = "32516143800",
                            Saldo = 0.0
                        });
                });

            modelBuilder.Entity("BancoDigitalAPI.Models.Correntista", b =>
                {
                    b.Property<string>("Cpf")
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("varchar(80)");

                    b.HasKey("Cpf");

                    b.ToTable("Correntistas");

                    b.HasData(
                        new
                        {
                            Cpf = "32516143800",
                            Nome = "Rodrigo Zanferrari"
                        },
                        new
                        {
                            Cpf = "33713557802",
                            Nome = "Raphaela Goes"
                        });
                });

            modelBuilder.Entity("BancoDigitalAPI.Models.ContaCorrente", b =>
                {
                    b.HasOne("BancoDigitalAPI.Models.Correntista", "Correntista")
                        .WithMany("ContasCorrentes")
                        .HasForeignKey("CorrentistaCpf")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Correntista");
                });

            modelBuilder.Entity("BancoDigitalAPI.Models.Correntista", b =>
                {
                    b.Navigation("ContasCorrentes");
                });
#pragma warning restore 612, 618
        }
    }
}
