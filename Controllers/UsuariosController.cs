using Microsoft.AspNetCore.Mvc;
using TarijaReadApp.Interfaces;
using TarijaReadApp.Models;

namespace TarijaReadApp.Controllers;

public class UsuariosController : Controller
{
    private readonly IRepository<Usuario> _repository;

    public UsuariosController(IRepository<Usuario> repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> Index() => View(await _repository.GetAllAsync());

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Usuario usuario)
    {
        if (!ModelState.IsValid) return View(usuario);
        await _repository.AddAsync(usuario);
        await _repository.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario == null) return NotFound();
        return View(usuario);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Usuario usuario)
    {
        if (id != usuario.Id) return NotFound();
        if (!ModelState.IsValid) return View(usuario);
        _repository.Update(usuario);
        await _repository.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario == null) return NotFound();
        return View(usuario);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var usuario = await _repository.GetByIdAsync(id);
        if (usuario != null)
        {
            _repository.Remove(usuario);
            await _repository.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}