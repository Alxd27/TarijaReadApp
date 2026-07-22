using Microsoft.AspNetCore.Mvc;
using TarijaReadApp.Interfaces;

namespace TarijaReadApp.Controllers;

public class LibrosController : Controller
{
    private readonly ILibroRepository _repository;

    public LibrosController(ILibroRepository repository)
    {
        _repository = repository;
    }

    // GET: Libros
    public async Task<IActionResult> Index()
    {
        var libros = await _repository.GetAllAsync(); // ya trae Categoria incluida
        return View(libros);
    }
}