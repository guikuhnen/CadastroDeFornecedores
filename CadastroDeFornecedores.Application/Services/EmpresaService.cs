using CadastroDeFornecedores.Data.Repository;
using CadastroDeFornecedores.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroDeFornecedores.Application.Services
{
    public class EmpresaService : IEmpresaService
    {
        public readonly IEmpresaRepository _repository;

        public EmpresaService(IEmpresaRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Empresa>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task CreateAsync(Empresa empresa)
        {
            await _repository.CreateAsync(empresa);
        }

        public async Task<Empresa> GetAsync(int id)
        {
            return await _repository.GetAsync(id);
        }

        public async Task UpdateAsync(Empresa empresa)
        {
            await _repository.UpdateAsync(empresa);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public bool EmpresaExists(int id)
        {
            return _repository.EmpresaExists(id);
        }
    }
}
