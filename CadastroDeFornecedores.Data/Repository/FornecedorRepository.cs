using CadastroDeFornecedores.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroDeFornecedores.Data.Repository
{
    public class FornecedorRepository : IFornecedorRepository
    {
        public readonly CadastroDeFornecedoresContext _context;

        public FornecedorRepository(CadastroDeFornecedoresContext context)
        {
            this._context = context;
        }
    }
}
