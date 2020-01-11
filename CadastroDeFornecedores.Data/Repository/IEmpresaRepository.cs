using CadastroDeFornecedores.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CadastroDeFornecedores.Data.Repository
{
    public interface IEmpresaRepository
    {
        Task<List<Empresa>> GetAllAsync();
        Task CreateAsync(Empresa empresa);
        Task<Empresa> GetAsync(int id);
        Task UpdateAsync(Empresa empresa);
    }
}
