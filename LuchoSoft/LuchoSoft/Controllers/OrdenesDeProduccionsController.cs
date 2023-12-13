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
    public class OrdenesDeProduccionsController : Controller
    {
        private readonly LuchoSoftV1Context _context;

        public OrdenesDeProduccionsController(LuchoSoftV1Context context)
        {
            _context = context;
        }



        // GET: OrdenesDeProduccions
        public async Task<IActionResult> Index()
        {
            var luchoSoftV1Context = _context.OrdenesDeProduccions.Include(o => o.IdEmpleadoOrdenesDeProduccionNavigation);
            return View(await luchoSoftV1Context.ToListAsync());
        }

        // GET: OrdenesDeProduccions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrdenesDeProduccions == null)
            {
                return NotFound();
            }

            var ordenesDeProduccion = await _context.OrdenesDeProduccions
                .Include(o => o.IdEmpleadoOrdenesDeProduccionNavigation)
                .FirstOrDefaultAsync(m => m.IdOrdenDeProduccion == id);
            if (ordenesDeProduccion == null)
            {
                return NotFound();
            }

            return View(ordenesDeProduccion);
        }

        // GET: OrdenesDeProduccions/Create
        public IActionResult Create()
        {
            ViewData["IdEmpleadoOrdenesDeProduccion"] = new SelectList(_context.Empleados, "IdEmpleado", "IdEmpleado");
            return View();
        }

        // POST: OrdenesDeProduccions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdOrdenDeProduccion,DescripcionOrden,FechaOrden,IdEmpleadoOrdenesDeProduccion")] OrdenesDeProduccion ordenesDeProduccion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordenesDeProduccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEmpleadoOrdenesDeProduccion"] = new SelectList(_context.Empleados, "IdEmpleado", "IdEmpleado", ordenesDeProduccion.IdEmpleadoOrdenesDeProduccion);
            return View(ordenesDeProduccion);
        }

        // GET: OrdenesDeProduccions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrdenesDeProduccions == null)
            {
                return NotFound();
            }

            var ordenesDeProduccion = await _context.OrdenesDeProduccions.FindAsync(id);
            if (ordenesDeProduccion == null)
            {
                return NotFound();
            }
            ViewData["IdEmpleadoOrdenesDeProduccion"] = new SelectList(_context.Empleados, "IdEmpleado", "IdEmpleado", ordenesDeProduccion.IdEmpleadoOrdenesDeProduccion);
            return View(ordenesDeProduccion);
        }

        // POST: OrdenesDeProduccions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdOrdenDeProduccion,DescripcionOrden,FechaOrden,IdEmpleadoOrdenesDeProduccion")] OrdenesDeProduccion ordenesDeProduccion)
        {
            if (id != ordenesDeProduccion.IdOrdenDeProduccion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordenesDeProduccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenesDeProduccionExists(ordenesDeProduccion.IdOrdenDeProduccion))
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
            ViewData["IdEmpleadoOrdenesDeProduccion"] = new SelectList(_context.Empleados, "IdEmpleado", "IdEmpleado", ordenesDeProduccion.IdEmpleadoOrdenesDeProduccion);
            return View(ordenesDeProduccion);
        }

        // GET: OrdenesDeProduccions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrdenesDeProduccions == null)
            {
                return NotFound();
            }

            var ordenesDeProduccion = await _context.OrdenesDeProduccions
                .Include(o => o.IdEmpleadoOrdenesDeProduccionNavigation)
                .FirstOrDefaultAsync(m => m.IdOrdenDeProduccion == id);
            if (ordenesDeProduccion == null)
            {
                return NotFound();
            }

            return View(ordenesDeProduccion);
        }

        // POST: OrdenesDeProduccions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrdenesDeProduccions == null)
            {
                return Problem("Entity set 'LuchoSoftV1Context.OrdenesDeProduccions'  is null.");
            }
            var ordenesDeProduccion = await _context.OrdenesDeProduccions.FindAsync(id);
            if (ordenesDeProduccion != null)
            {
                _context.OrdenesDeProduccions.Remove(ordenesDeProduccion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenesDeProduccionExists(int id)
        {
          return (_context.OrdenesDeProduccions?.Any(e => e.IdOrdenDeProduccion == id)).GetValueOrDefault();
        }
    }
}
