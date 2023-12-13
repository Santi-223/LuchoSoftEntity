using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LuchoSoft.Models;

namespace LuchoSoft.Controllers
{
    public class OrdenInsumoesController : Controller
    {
        private readonly LuchoSoftV1Context _context;

        public OrdenInsumoesController(LuchoSoftV1Context context)
        {
            _context = context;
        }

        // GET: OrdenInsumoes
        public async Task<IActionResult> Index()
        {
            var luchoSoftV1Context = _context.OrdenInsumos.Include(o => o.IdInsumoOrdenInsumosNavigation).Include(o => o.IdOrdenDeProduccionOrdenInsumosNavigation);
            return View(await luchoSoftV1Context.ToListAsync());
        }

        // GET: OrdenInsumoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrdenInsumos == null)
            {
                return NotFound();
            }

            var ordenInsumo = await _context.OrdenInsumos
                .Include(o => o.IdInsumoOrdenInsumosNavigation)
                .Include(o => o.IdOrdenDeProduccionOrdenInsumosNavigation)
                .FirstOrDefaultAsync(m => m.IdOrdenInsumos == id);
            if (ordenInsumo == null)
            {
                return NotFound();
            }

            return View(ordenInsumo);
        }

        // GET: OrdenInsumoes/Create
        public IActionResult Create()
        {
            ViewData["IdInsumoOrdenInsumos"] = new SelectList(_context.Insumos, "IdInsumo", "IdInsumo");
            ViewData["IdOrdenDeProduccionOrdenInsumos"] = new SelectList(_context.OrdenesDeProduccions, "IdOrdenDeProduccion", "IdOrdenDeProduccion");
            return View();
        }

        // POST: OrdenInsumoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOrdenInsumos,DescripcionOrdenInsumos,CantidadInsumoOrdenInsumos,IdOrdenDeProduccionOrdenInsumos,IdInsumoOrdenInsumos")] OrdenInsumo ordenInsumo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordenInsumo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdInsumoOrdenInsumos"] = new SelectList(_context.Insumos, "IdInsumo", "IdInsumo", ordenInsumo.IdInsumoOrdenInsumos);
            ViewData["IdOrdenDeProduccionOrdenInsumos"] = new SelectList(_context.OrdenesDeProduccions, "IdOrdenDeProduccion", "IdOrdenDeProduccion", ordenInsumo.IdOrdenDeProduccionOrdenInsumos);
            return View(ordenInsumo);
        }

        // GET: OrdenInsumoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrdenInsumos == null)
            {
                return NotFound();
            }

            var ordenInsumo = await _context.OrdenInsumos.FindAsync(id);
            if (ordenInsumo == null)
            {
                return NotFound();
            }
            ViewData["IdInsumoOrdenInsumos"] = new SelectList(_context.Insumos, "IdInsumo", "IdInsumo", ordenInsumo.IdInsumoOrdenInsumos);
            ViewData["IdOrdenDeProduccionOrdenInsumos"] = new SelectList(_context.OrdenesDeProduccions, "IdOrdenDeProduccion", "IdOrdenDeProduccion", ordenInsumo.IdOrdenDeProduccionOrdenInsumos);
            return View(ordenInsumo);
        }

        // POST: OrdenInsumoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOrdenInsumos,DescripcionOrdenInsumos,CantidadInsumoOrdenInsumos,IdOrdenDeProduccionOrdenInsumos,IdInsumoOrdenInsumos")] OrdenInsumo ordenInsumo)
        {
            if (id != ordenInsumo.IdOrdenInsumos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordenInsumo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenInsumoExists(ordenInsumo.IdOrdenInsumos))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdInsumoOrdenInsumos"] = new SelectList(_context.Insumos, "IdInsumo", "IdInsumo", ordenInsumo.IdInsumoOrdenInsumos);
            ViewData["IdOrdenDeProduccionOrdenInsumos"] = new SelectList(_context.OrdenesDeProduccions, "IdOrdenDeProduccion", "IdOrdenDeProduccion", ordenInsumo.IdOrdenDeProduccionOrdenInsumos);
            return View(ordenInsumo);
        }

        // GET: OrdenInsumoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrdenInsumos == null)
            {
                return NotFound();
            }

            var ordenInsumo = await _context.OrdenInsumos
                .Include(o => o.IdInsumoOrdenInsumosNavigation)
                .Include(o => o.IdOrdenDeProduccionOrdenInsumosNavigation)
                .FirstOrDefaultAsync(m => m.IdOrdenInsumos == id);
            if (ordenInsumo == null)
            {
                return NotFound();
            }

            return View(ordenInsumo);
        }

        // POST: OrdenInsumoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrdenInsumos == null)
            {
                return Problem("Entity set 'LuchoSoftV1Context.OrdenInsumos'  is null.");
            }
            var ordenInsumo = await _context.OrdenInsumos.FindAsync(id);
            if (ordenInsumo != null)
            {
                _context.OrdenInsumos.Remove(ordenInsumo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenInsumoExists(int id)
        {
          return (_context.OrdenInsumos?.Any(e => e.IdOrdenInsumos == id)).GetValueOrDefault();
        }
    }
}
