using CadastroDeFornecedores.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroDeFornecedores.Application.Services
{
    public interface IFornecedorService
    {
        Task<List<Fornecedor>> GetAllAsync(string buscarNome, string buscarCPFouCNPJ, DateTime buscarData);
        Task CreateAsync(Fornecedor fornecedor);
        Task<Fornecedor> GetAsync(int id);
        Task UpdateAsync(Fornecedor fornecedor);
        Task DeleteAsync(int id);
        bool FornecedorExists(int id);
    }
}
