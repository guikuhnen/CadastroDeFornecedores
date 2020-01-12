using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CadastroDeFornecedores.Domain.Models
{
    public class Fornecedor
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [MaxLength(14)]
        public string CPFouCNPJ { get; set; }

        public DateTime DataHoraCadastro { get; set; }

        [MaxLength(9)]
        public string RegistroGeralPF { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DataAniversarioPF { get; set; }
        
        [ForeignKey("EmpresaId")]
        public Empresa Empresa { get; set; }
        public int EmpresaId { get; set; }

        public List<FornecedorTelefones> Telefones { get; set; }
    }

    public class FornecedorTelefones
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Numero { get; set; }

        [ForeignKey("FornecedorId")]
        public Fornecedor Fornecedor { get; set; }
        public int FornecedorId { get; set; }
    }
}
