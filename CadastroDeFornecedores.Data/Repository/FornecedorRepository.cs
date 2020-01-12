using CadastroDeFornecedores.Data.Context;
using CadastroDeFornecedores.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroDeFornecedores.Data.Repository
{
    public class FornecedorRepository : IFornecedorRepository
    {
        public readonly CadastroDeFornecedoresContext _context;

        public FornecedorRepository(CadastroDeFornecedoresContext context)
        {
            _context = context;
        }

        public async Task<List<Fornecedor>> GetAllAsync()
        {
            return await _context.Fornecedores
                .Include(f => f.Empresa)
                .Include(f => f.Telefones)
                .ToListAsync();
        }

        public async Task CreateAsync(Fornecedor fornecedor)
        {
            _context.Add(fornecedor);

            await _context.SaveChangesAsync();
        }

        public async Task<Fornecedor> GetAsync(int id)
        {
            // TODO getFullAsync
            return await _context.Fornecedores
                .Include(f => f.Empresa)
                .Include(f => f.Telefones)
                // .AsNoTracking()
                .Where(f => f.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Fornecedor fornecedor)
        {
            _context.Update(fornecedor);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);

            _context.Fornecedores.Remove(fornecedor);

            await _context.SaveChangesAsync();
        }

        public bool FornecedorExists(int id)
        {
            return _context.Fornecedores.Any(e => e.Id == id);
        }
    }
}
