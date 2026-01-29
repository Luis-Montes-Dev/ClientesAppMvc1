using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClientesAppMvc.Data;
using ClientesAppMvc.Models;

namespace ClientesAppMvc.Controllers;

public class ClientesController : Controller
{
    private readonly ApplicationDbContext _context;
    public ClientesController(ApplicationDbContext context)
    {
        _context = context;
    }
        
    public async Task< IActionResult> Index()
    {
        var clientesList = await _context.Clientes.ToListAsync();
        
        return View(clientesList);
    }
        
    public async Task<IActionResult> Details(int id)
    {
        // 1. Verificar que el id no sea cero
        if (id == 0)
        {
            return NotFound();
        }
        // 2. Buscar el cliente en la base de datos
        var cliente = await _context.Clientes.FindAsync(id);
        // 3. Verificar que el cliente exista
        if (cliente == null)
        {
            return NotFound();
        }
        return View(cliente);
    }
        
    public ActionResult Create()
    {
        return View();
    }
        
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Cliente cliente)
    {
        if (!ModelState.IsValid)
        {
            return View(cliente);
        }
        try
        {
            await _context.AddAsync(cliente);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Cliente creado exitosamente.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Error al crear el cliente.";
            return View(cliente);
        }
    }

    
    public async Task<IActionResult> Edit(int id)
    {
        // 1. Verificar que el id no sea cero
        if (id == 0)
        {
            return NotFound();
        }
        
        // 2. Buscar el cliente en la base de datos
        var cliente = await _context.Clientes.FindAsync(id);
        
        // 3. Verificar que el cliente exista
        if (cliente == null)
        {
            return NotFound();
        }
        return View(cliente);
    }
        
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Cliente cliente)
    {
        // 1. Verificar que el modelo sea válido
        if (!ModelState.IsValid)
        {
            return View(cliente);
        }
        try
        {
            // 2. Actualizar el cliente en la base de datos
            _context.Update(cliente);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Cliente actualizado exitosamente.";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "Error al actualizar el cliente.";
            return View(cliente);
        }
    }
        
    public async Task<IActionResult> Delete(int id)
    {
        // 1. Verificar que el id no sea cero
        if (id == 0)
        {
            return NotFound();
        }
        // 2. Buscar el cliente en la base de datos
        var cliente = await _context.Clientes.FindAsync(id);
        // 3. Verificar que el cliente exista
        if (cliente == null)
        {
            return NotFound();
        }
        return View(cliente);
    }
        
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Cliente cliente)
    {
        try
        {
            // 1. Buscar el cliente en la base de datos
            var clienteInDb = await _context.Clientes.FindAsync(cliente.Id);
            if (clienteInDb == null)
            {
                return NotFound();
            }
            //2. Eliminación lógica del cliente (Activo = false)
            clienteInDb.Activo = false;
            _context.Update(clienteInDb);                
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Cliente desactivado exitosamente.";
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            TempData["ErrorMessage"] = "Error al eliminar el cliente.";
            return View(cliente);
        }
    }
}
