using CadastroDeFornecedores.Data.Repository;
using CadastroDeFornecedores.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroDeFornecedores.Application.Services
{
    public class FornecedorService : IFornecedorService
    {
        public readonly IFornecedorRepository _repository;

        public FornecedorService(IFornecedorRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Fornecedor>> GetAllAsync(string buscarNome, string buscarCPFouCNPJ, DateTime buscarData)
        {
            return await _repository.GetAllAsync(buscarNome, buscarCPFouCNPJ, buscarData);
        }

        public async Task CreateAsync(Fornecedor fornecedor)
        {
            fornecedor.DataHoraCadastro = DateTime.Now;

            await _repository.CreateAsync(fornecedor);
        }

        public async Task<Fornecedor> GetAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task UpdateAsync(Fornecedor fornecedor)
        {
            await _repository.UpdateAsync(fornecedor);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public bool FornecedorExists(int id)
        {
            return _repository.FornecedorExists(id);
        }
    }
}
