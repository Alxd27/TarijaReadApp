using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TarijaReadApp.Interfaces;
using TarijaReadApp.Models;

namespace TarijaReadApp.Controllers;

public class MultasController : Controller
{
    private readonly IMultaRepository _repository;
    private readonly IPrestamoRepository _prestamoRepository;

    public MultasController(IMultaRepository repository, IPrestamoRepository prestamoRepository)
    {
        _repository = repository;
        _prestamoRepository = prestamoRepository;
    }

    public async Task<IActionResult> Index() => View(await _repository.GetAllAsync());

    public async Task<IActionResult> Create()
    {
        await CargarPrestamos();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Multa multa)
    {
        if (!ModelState.IsValid)
        {
            await CargarPrestamos(multa.PrestamoId);
            return View(multa);
        }

        await _repository.AddAsync(multa);
        await _repository.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var multa = await _repository.GetByIdAsync(id);
        if (multa == null) return NotFound();
        await CargarPrestamos(multa.PrestamoId);
        return View(multa);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Multa multa)
    {
        if (id != multa.Id) return NotFound();
        if (!ModelState.IsValid)
        {
            await CargarPrestamos(multa.PrestamoId);
            return View(multa);
        }

        _repository.Update(multa);
        await _repository.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var multa = await _repository.GetByIdAsync(id);
        if (multa == null) return NotFound();
        return View(multa);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var multa = await _repository.GetByIdAsync(id);
        if (multa != null)
        {
            _repository.Remove(multa);
            await _repository.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private async Task CargarPrestamos(int? prestamoSel = null)
    {
        var prestamos = await _prestamoRepository.GetAllAsync();
        ViewBag.PrestamoId = new SelectList(prestamos, "Id", "Descripcion", prestamoSel);
    }
}