using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BancoDigitalAPI.Business;
using BancoDigitalAPI.Data;
using BancoDigitalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BancoDigitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorrentistaController : ControllerBase
    {
        // GET: api/Correntista
        /// <summary>
        /// Retornar uma lista de todos os Correntistas cadastrados.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        /// <response code="200">A lista de Correntistas foi obtida com sucesso.</response>
        /// <response code="404">A lista de Correntistas não foi encontrada.</response>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Correntista>>> GetCorrentistas([FromServices] BancoDigitalDataContext context)
        {
            var correntistas = await context.Correntistas.ToListAsync();

            if (correntistas.Count == 0)
            {
                return NotFound();
            }

            return correntistas;
        }

        // GET: api/Correntista/32516143800
        /// <summary>
        /// Retornar um Correntista pelo CPF.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cpf">CPF do Correntista</param>
        /// <returns></returns>
        /// <response code="200">O Correntista foi obtido com sucesso.</response>
        /// <response code="404">O Correntista não foi encontrado.</response>
        [HttpGet]
        [Route("{cpf}")]
        public async Task<ActionResult<Correntista>> GetCorrentistaCpf([FromServices] BancoDigitalDataContext context, string cpf)
        {
            //var correntista = await context.Correntistas.FindAsync(cpf);
            var correntista = await context.Correntistas.FirstOrDefaultAsync(p => p.Cpf == cpf);

            if (correntista == null)
            {
                return NotFound();
            }

            return correntista;
        }

        // POST: api/Correntista
        /// <summary>
        /// Inserir um Correntista, validando caso o CPF ainda não tenha sido cadastrado anteriormente.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="model">Modelo (JSON) do Correntista</param>
        /// <returns></returns>
        /// <response code="200">O Correntista foi inserido com sucesso.</response>
        /// <response code="400">O modelo (JSON) do Correntista está inválido.</response>
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Correntista>> PostCorrentista([FromServices] BancoDigitalDataContext context, [FromBody] Correntista model)
        {
            if (ModelState.IsValid)
            {
                if (context.Correntistas.Where(p => p.Cpf == model.Cpf).Count() > 0)
                {
                    return Problem(statusCode: (int)HttpStatusCode.BadRequest, title: "CPF do Correntista já cadastrado.", detail: "CPF do Correntista já cadastrado.");
                }

                context.Correntistas.Add(model);
                await context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetCorrentistaCpf), new { context = context, cpf = model.Cpf }, model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT: api/Correntista/32516143800
        /// <summary>
        /// Atualizar os dados de um Correntista cadastrado anteriormente.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cpf">CPF do Correntista</param>
        /// <param name="model">Modelo (JSON) do Correntista</param>
        /// <returns></returns>
        /// <response code="200">O Correntista foi atualizado com sucesso.</response>
        /// <response code="400">O modelo (JSON) do Correntista está inválido.</response>
        /// <response code="404">O Correntista não foi encontrado.</response>
        [HttpPut]
        [Route("{cpf}")]
        public async Task<ActionResult<Correntista>> PutCorrentista([FromServices] BancoDigitalDataContext context, string cpf, [FromBody] Correntista model)
        {
            if (ModelState.IsValid)
            {
                if (cpf != model.Cpf)
                {
                    return BadRequest();
                }

                context.Entry(model).State = EntityState.Modified;

                try
                {
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!context.Correntistas.Any(p => p.Cpf == cpf))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtAction(nameof(GetCorrentistaCpf), new { context = context, cpf = model.Cpf }, model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //[HttpPost]
        //[Route("")]
        //public Resultado Post([FromServices] BancoDigitalDataContext context, [FromBody] Correntista model)
        //{
        //    var service = new CorrentistaService(context);

        //    return service.Incluir(model);
        //}
    }
}