using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseApp.Data;
using WarehouseApp.Models;

namespace WarehouseApp.Controllers;

public class GudangController: Controller
{
    private readonly ILogger<GudangController> _logger;
    private readonly AppDbContext _context;

    public GudangController(ILogger<GudangController> Logger, AppDbContext context)
    {
        _logger = Logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Gudang> listGudang = await _context.Gudangs
            .Where(val => val.IsActive == true).ToListAsync();
        
        return View(listGudang);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] Gudang gudang)
    {
        if (!ModelState.IsValid)
        {
            return View(gudang);
        }

        _context.Gudangs.Add(gudang);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Successfully add gudang";

        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit([FromRoute] int? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }

        var gudang = await _context.Gudangs.FindAsync(id);
        return View(gudang);
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromForm] Gudang gudang)
    {
        if (!ModelState.IsValid)
        {
            return View(gudang);
        }

        var gudangExist = await _context.Gudangs.FindAsync(gudang.KodeGudang);

        if (gudangExist == null) return NotFound();

        gudangExist.NamaGudang = gudang.NamaGudang;
        gudangExist.UpdatedAt = DateTime.Now;

        _context.Gudangs.Update(gudangExist);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Successfully edit gudang";
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete([FromRoute] int? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }

        var gudangExists = await _context.Gudangs.FindAsync(id);

        if (gudangExists == null) return NotFound();

        gudangExists.IsActive = false;
        
        _context.Gudangs.Update(gudangExists);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Successfully delete gudang";
        
        return RedirectToAction(nameof(Index));
    }
}