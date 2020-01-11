using CadastroDeFornecedores.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroDeFornecedores.Application.Services
{
    public class FornecedorService : IFornecedorService
    {
        public readonly IFornecedorRepository _repository;

        public FornecedorService(IFornecedorRepository repository)
        {
            this._repository = repository;
        }
    }
}
