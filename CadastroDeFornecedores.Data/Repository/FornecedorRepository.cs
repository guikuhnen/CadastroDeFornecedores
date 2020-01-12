using CadastroDeFornecedores.Data.Context;
using CadastroDeFornecedores.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<List<Fornecedor>> GetAllAsync(string buscarNome, string buscarCPFouCNPJ, DateTime buscarData)
        {
            var fornecedores = _context.Fornecedores.AsQueryable();

            if (!String.IsNullOrEmpty(buscarNome))
                fornecedores = fornecedores.Where(f => f.Nome.Contains(buscarNome));

            if (!String.IsNullOrEmpty(buscarCPFouCNPJ))
                fornecedores = fornecedores.Where(f => f.CPFouCNPJ.Contains(buscarCPFouCNPJ));

            if (buscarData != DateTime.MinValue)
                fornecedores = fornecedores.Where(f => f.DataHoraCadastro.Date.Equals(buscarData));

            return await fornecedores
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
