using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BancoDigitalAPI.Models
{
    public class ContaCorrente
    {
        [Key]
        [Required(ErrorMessage = "Número da Conta é obrigatório.")]
        [Range(1, long.MaxValue, ErrorMessage = "Número da Conta deve ser maior que 0.")]
        public long Numero { get; set; }

        [Required(ErrorMessage = "Saldo da Conta é obrigatório.")]
        [Range(0, long.MaxValue, ErrorMessage = "Saldo da Conta deve ser maior ou igual a 0.")]
        public double Saldo { get; set; }

        [Required(ErrorMessage = "CPF do Correntista é obrigatório.")]
        [MinLength(11, ErrorMessage = "CPF do Correntista deve conter 11 caracteres.")]
        [MaxLength(11, ErrorMessage = "CPF do Correntista deve conter 11 caracteres.")]
        public string CorrentistaCpf { get; set; }

        // dados do Correntista da Conta Corrente
        public virtual Correntista Correntista { get; set; }
    }
}
