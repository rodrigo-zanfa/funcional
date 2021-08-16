using BancoDigitalAPI.Data;
using BancoDigitalAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoDigitalAPI.GraphQL
{
    public class Mutation
    {
        private readonly BancoDigitalDataContext context;

        public Mutation(BancoDigitalDataContext context)
        {
            this.context = context;
        }

        public async Task<ContaCorrente> Depositar(OperacaoInput depositarInput)
        {
            var contaCorrente = await context.ContasCorrentes.FindAsync(depositarInput.Numero);
            //var contaCorrente = await context.ContasCorrentes.FirstOrDefaultAsync(p => p.Numero == depositarInput.Numero);

            if (contaCorrente == null)
            {
                throw new Exception("Conta não encontrada.");
            }

            contaCorrente.Saldo += depositarInput.Valor;

            context.Entry(contaCorrente).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.ContasCorrentes.Any(p => p.Numero == depositarInput.Numero))
                {
                    throw new Exception("Erro durante atualização.");
                }
                else
                {
                    throw;
                }
            }

            return contaCorrente;
        }

        public async Task<ContaCorrente> Sacar(OperacaoInput sacarInput)
        {
            var contaCorrente = await context.ContasCorrentes.FindAsync(sacarInput.Numero);
            //var contaCorrente = await context.ContasCorrentes.FirstOrDefaultAsync(p => p.Numero == sacarInput.Numero);

            if (contaCorrente == null)
            {
                throw new Exception("Conta não encontrada.");
            }

            if (sacarInput.Valor > contaCorrente.Saldo)
            {
                throw new Exception("Saldo insuficiente para Operação de Saque.");
            }
            else
            {
                contaCorrente.Saldo -= sacarInput.Valor;
            }

            context.Entry(contaCorrente).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!context.ContasCorrentes.Any(p => p.Numero == sacarInput.Numero))
                {
                    throw new Exception("Erro durante atualização.");
                }
                else
                {
                    throw;
                }
            }

            return contaCorrente;
        }
    }
}
