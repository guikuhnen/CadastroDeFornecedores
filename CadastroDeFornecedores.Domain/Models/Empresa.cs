using System.ComponentModel.DataAnnotations;

namespace CadastroDeFornecedores.Domain.Models
{
    public class Empresa
    {
        public int Id { get; set; }

        [Required]
        public string NomeFantasia { get; set; }

        [Required]
        [MaxLength(14)]
        public string CNPJ { get; set; }

        public UnidadeFederacaoSigla UF { get; set; }
    }    
}
