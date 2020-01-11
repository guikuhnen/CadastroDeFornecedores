using CadastroDeFornecedores.Data.Context;
using CadastroDeFornecedores.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CadastroDeFornecedores.Data.Repository
{
    public class EmpresaRepository : IEmpresaRepository
    {
        public readonly CadastroDeFornecedoresContext _context;

        public EmpresaRepository(CadastroDeFornecedoresContext context)
        {
            _context = context;
        }

        public async Task<List<Empresa>> GetAllAsync()
        {
            return await _context.Empresas.ToListAsync();
        }

        public async Task CreateAsync(Empresa empresa)
        {
            _context.Add(empresa);
            await _context.SaveChangesAsync();
        }

        public async Task<Empresa> GetAsync(int id)
        {
            return await _context.Empresas.FindAsync(id);
        }

        public async Task UpdateAsync(Empresa empresa)
        {
            _context.Update(empresa);
            await _context.SaveChangesAsync();
        }
    }
}
