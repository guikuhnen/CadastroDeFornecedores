using CadastroDeFornecedores.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CadastroDeFornecedores.Application.Services
{
    public interface IEmpresaService
    {
        Task<List<Empresa>> GetAllAsync();
        List<Empresa> GetAll();
        Task CreateAsync(Empresa empresa);
        Task<Empresa> GetAsync(int id);
        Task UpdateAsync(Empresa empresa);
        Task DeleteAsync(int id);
        bool EmpresaExists(int id);
    }
}
