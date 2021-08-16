using BancoDigitalAPI.Data;
using BancoDigitalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BancoDigitalAPI.Business
{
    public class CorrentistaService
    {
        private BancoDigitalDataContext _context;

        public CorrentistaService(BancoDigitalDataContext context)
        {
            _context = context;
        }

        private Resultado ValidarDados(Correntista model)
        {
            var resultado = new Resultado();

            if (model == null)
            {
                resultado.Inconsistencias.Add("É necessário informar os dados do Correntista.");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(model.Cpf) || model.Cpf.Length != 11)
                {
                    resultado.Inconsistencias.Add("CPF do Correntista deve conter 11 caracteres.");
                }

                if (string.IsNullOrWhiteSpace(model.Nome) || (model.Nome.Length < 10 || model.Nome.Length > 80))
                {
                    resultado.Inconsistencias.Add("Nome do Correntista deve conter entre 10 e 80 caracteres.");
                }
            }

            return resultado;
        }

        public Resultado Incluir(Correntista model)
        {
            var resultado = ValidarDados(model);

            //resultado.Acao = "Inclusão de Correntista";

            if (resultado.Inconsistencias.Count == 0 &&
                _context.Correntistas.Where(p => p.Cpf == model.Cpf).Count() > 0)
            {
                resultado.Inconsistencias.Add("CPF do Correntista já cadastrado.");
            }

            if (resultado.Inconsistencias.Count == 0)
            {
                _context.Correntistas.Add(model);
                _context.SaveChanges();
            }

            return resultado;
        }
    }
}
