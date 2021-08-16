using BancoDigitalAPI.Data;
using BancoDigitalAPI.Models;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoDigitalAPI.GraphQL
{
    public class Query
    {
        private readonly BancoDigitalDataContext context;

        public Query(BancoDigitalDataContext context)
        {
            this.context = context;
        }

        [UseFiltering]
        public IQueryable<Correntista> Correntistas => context.Correntistas;

        [UseFiltering]
        public IQueryable<ContaCorrente> ContasCorrentes => context.ContasCorrentes.Include(p => p.Correntista);
    }
}
