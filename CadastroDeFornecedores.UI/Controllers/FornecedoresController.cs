using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CadastroDeFornecedores.Domain.Models;
using CadastroDeFornecedores.Application.Services;
using System;

namespace CadastroDeFornecedores.UI.Controllers
{
    public class FornecedoresController : Controller
    {
        private readonly IFornecedorService _fornecedorService;
        private readonly IEmpresaService _empresaService;

        public FornecedoresController(IFornecedorService fornecedorService, IEmpresaService empresaService)
        {
            _fornecedorService = fornecedorService;
            _empresaService = empresaService;
        }

        // GET: Fornecedores
        public async Task<IActionResult> Index(string buscarNome, string buscarCPFouCNPJ, DateTime buscarData)
        {
            var fornecedores = await _fornecedorService.GetAllAsync(buscarNome, buscarCPFouCNPJ, buscarData);

            return View(fornecedores);
        }

        // GET: Fornecedores/Create
        public IActionResult Create()
        {
            LoadViewData();

            return View();
        }

        // POST: Fornecedores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,CPFouCNPJ,DataHoraCadastro,RegistroGeralPF,DataAniversarioPF,EmpresaId,Telefones")] Fornecedor fornecedor)
        {
            ValidarFornecedor(fornecedor);

            if (ModelState.IsValid)
            {
                // TODO REGRAS

                await _fornecedorService.CreateAsync(fornecedor);

                return RedirectToAction(nameof(Index));
            }

            LoadViewData();

            return View(fornecedor);
        }

        // GET: Fornecedores/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            if (id.Equals(0))
                return NotFound();

            var fornecedor = await _fornecedorService.GetAsync(id);

            if (fornecedor == null)
                return NotFound();

            LoadViewData();

            return View(fornecedor);
        }

        // POST: Fornecedores/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,CPFouCNPJ,DataHoraCadastro,RegistroGeralPF,DataAniversarioPF,EmpresaId,Telefones")] Fornecedor fornecedor)
        {
            if (id != fornecedor.Id)
                return NotFound();

            ValidarFornecedor(fornecedor);

            if (ModelState.IsValid)
            {
                // TODO REGRAS
                try
                {
                    await _fornecedorService.UpdateAsync(fornecedor);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FornecedorExists(fornecedor.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            LoadViewData();

            return View(fornecedor);
        }

        // GET: Fornecedores/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            if (id.Equals(0))
                return NotFound();

            var fornecedor = await _fornecedorService.GetAsync(id);

            if (fornecedor == null)
                return NotFound();

            return View(fornecedor);
        }

        // POST: Fornecedores/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _fornecedorService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private bool FornecedorExists(int id)
        {
            return _fornecedorService.FornecedorExists(id);
        }

        private void LoadViewData()
        {
            ViewData["Empresa"] = new SelectList(_empresaService.GetAll(), "Id", "NomeFantasia");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddTelefone([Bind("Telefones")] Fornecedor fornecedor)
        {
            fornecedor.Telefones.Add(new FornecedorTelefones());

            return PartialView("FornecedorTelefone", fornecedor);
        }

        private void ValidarFornecedor(Fornecedor fornecedor)
        {
            // Caso o fornecedor seja pessoa física, também é necessário cadastrar o RG e a data de nascimento
            if (fornecedor.CPFouCNPJ.Length.Equals(11))
            {
                if (String.IsNullOrEmpty(fornecedor.RegistroGeralPF))
                    ModelState.AddModelError("RegistroGeralPF", "O campo RG é obrigatório.");

                if (!fornecedor.DataAniversarioPF.HasValue)
                    ModelState.AddModelError("DataAniversarioPF", "O campo Data de Aniversário é obrigatório.");

                // Caso a empresa seja do Paraná, não permitir cadastrar um fornecedor pessoa física menor de idade
                var empresa = _empresaService.GetAsync(fornecedor.EmpresaId).Result;

                if (empresa?.UF == UnidadeFederacaoSigla.PR)
                    if (fornecedor.DataAniversarioPF.HasValue)
                        if (DateTime.Today.Year - fornecedor.DataAniversarioPF.Value.Year < 18)
                            ModelState.AddModelError("DataAniversarioPF", "Não é permitido cadastrar um fornecedor menor de idade");
            }
        }
    }
}
