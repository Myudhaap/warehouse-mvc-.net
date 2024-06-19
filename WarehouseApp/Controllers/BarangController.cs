using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseApp.Data;
using WarehouseApp.Models;

namespace WarehouseApp.Controllers;

public class BarangController: Controller
{
    private readonly ILogger<BarangController> _logger;
    private readonly AppDbContext _context;

    public BarangController(ILogger<BarangController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var barang = await _context.Barangs
            .Include(i => i.Gudang)
            .Where(val => val.IsActive == true).ToListAsync();
        
        return View(barang);
    }
    
    
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var gudang = await _context.Gudangs.Where(val => val.IsActive == true).ToListAsync();
        ViewData["gudang"] = gudang;
            
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] Barang barang)
    {
        if (!ModelState.IsValid)
        {
            var gudang = await _context.Gudangs.Where(val => val.IsActive == true).ToListAsync();
            ViewData["gudang"] = gudang;
            
            return View(barang);
        }

        _context.Barangs.Add(barang);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Successfully add barang";

        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public async Task<IActionResult> Edit([FromRoute] int? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }
        
        var gudang = await _context.Gudangs.Where(val => val.IsActive == true).ToListAsync();
        ViewData["gudang"] = gudang;

        var barang = await _context.Barangs.FindAsync(id);
        return View(barang);
    }

    [HttpPost]
    public async Task<IActionResult> Edit([FromForm] Barang barang)
    {
        if (!ModelState.IsValid)
        {
            var gudang = await _context.Gudangs.Where(val => val.IsActive == true).ToListAsync();
            ViewData["gudang"] = gudang;
            
            return View(barang);
        }

        var barangExist = await _context.Barangs.FindAsync(barang.KodeBarang);

        if (barangExist == null) return NotFound();

        barangExist.NamaBarang = barang.NamaBarang;
        barangExist.KodeBarang = barang.KodeBarang;
        barangExist.HargaBarang = barang.HargaBarang;
        barangExist.JumlahBarang = barang.JumlahBarang;
        barangExist.UpdatedAt = DateTime.Now;

        _context.Barangs.Update(barangExist);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Successfully edit barang";
        
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete([FromRoute] int? id)
    {
        if (!id.HasValue)
        {
            return NotFound();
        }

        var barangExists = await _context.Barangs.FindAsync(id);

        if (barangExists == null) return NotFound();

        barangExists.IsActive = false;
        
        _context.Barangs.Update(barangExists);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Successfully delete barang";
        
        return RedirectToAction(nameof(Index));
    }
}