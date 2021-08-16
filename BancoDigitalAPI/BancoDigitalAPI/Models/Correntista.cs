using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BancoDigitalAPI.Models
{
    public class Correntista
    {
        [Key]
        [Required(ErrorMessage = "CPF do Correntista é obrigatório.")]
        [MinLength(11, ErrorMessage = "CPF do Correntista deve conter 11 caracteres.")]
        [MaxLength(11, ErrorMessage = "CPF do Correntista deve conter 11 caracteres.")]
        // criar validação de CPF
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Nome do Correntista é obrigatório.")]
        [MinLength(10, ErrorMessage = "Nome do Correntista deve conter entre 10 e 80 caracteres.")]
        [MaxLength(80, ErrorMessage = "Nome do Correntista deve conter entre 10 e 80 caracteres.")]
        public string Nome { get; set; }

        // lista das Contas Correntes do Correntista
        [JsonIgnore]
        public virtual ICollection<ContaCorrente> ContasCorrentes { get; set; }
    }
}
