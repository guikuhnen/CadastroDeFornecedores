using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CadastroDeFornecedores.Domain.Models;
using CadastroDeFornecedores.Application.Services;

namespace CadastroDeFornecedores.UI.Controllers
{
    public class EmpresasController : Controller
    {
        private readonly IEmpresaService _empresaService;

        public EmpresasController(IEmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        // GET: Empresas
        public async Task<IActionResult> Index()
        {
            return View(await _empresaService.GetAllAsync());
        }

        // GET: Empresas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empresas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NomeFantasia,CNPJ,UF")] Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                await _empresaService.CreateAsync(empresa);                
                return RedirectToAction(nameof(Index));
            }
            return View(empresa);
        }

        // GET: Empresas/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            if (id.Equals(0))
                return NotFound();

            var empresa = await _empresaService.GetAsync(id);

            if (empresa == null)
                return NotFound();

            return View(empresa);
        }

        // POST: Empresas/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NomeFantasia,CNPJ,UF")] Empresa empresa)
        {
            if (id != empresa.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _empresaService.UpdateAsync(empresa);                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpresaExists(empresa.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(empresa);
        }

        // GET: Empresas/Delete/{id}
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empresa = await _context.Empresas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (empresa == null)
            {
                return NotFound();
            }

            return View(empresa);
        }

        // POST: Empresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empresa = await _context.Empresas.FindAsync(id);
            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpresaExists(int id)
        {
            return _context.Empresas.Any(e => e.Id == id);
        }
    }
}
