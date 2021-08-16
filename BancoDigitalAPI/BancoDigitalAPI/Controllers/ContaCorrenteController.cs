using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BancoDigitalAPI.Data;
using BancoDigitalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BancoDigitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContaCorrenteController : ControllerBase
    {
        // GET: api/ContaCorrente
        /// <summary>
        /// Retornar uma lista de todas as Contas Correntes cadastradas.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <response code="200">A lista de Contas Correntes foi obtida com sucesso.</response>
        /// <response code="404">A lista de Contas Correntes não foi encontrada.</response>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<ContaCorrente>>> GetContasCorrentes([FromServices] BancoDigitalDataContext context)
        {
            var contasCorrentes = await context.ContasCorrentes.Include(p => p.Correntista).ToListAsync();

            if (contasCorrentes.Count == 0)
            {
                return NotFound();
            }

            return contasCorrentes;
        }

        // GET: api/ContaCorrente/cpf/32516143800
        /// <summary>
        /// Retornar uma lista de todas as Contas Correntes cadastradas para um CPF.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cpf">CPF do Correntista</param>
        /// <returns></returns>
        /// <response code="200">A lista de Contas Correntes para um CPF foi obtida com sucesso.</response>
        /// <response code="404">A lista de Contas Correntes para um CPF não foi encontrada.</response>
        [HttpGet]
        [Route("cpf/{cpf}")]
        public async Task<ActionResult<List<ContaCorrente>>> GetContasCorrentesCpf([FromServices] BancoDigitalDataContext context, string cpf)
        {
            var contasCorrentes = await context.ContasCorrentes.Where(p => p.CorrentistaCpf == cpf).Include(p => p.Correntista).ToListAsync();

            if (contasCorrentes.Count == 0)
            {
                return NotFound();
            }

            return contasCorrentes;
        }
    }
}