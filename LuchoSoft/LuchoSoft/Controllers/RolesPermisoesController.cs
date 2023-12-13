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
    public class RolesPermisoesController : Controller
    {
        private readonly LuchoSoftV1Context _context;

        public RolesPermisoesController(LuchoSoftV1Context context)
        {
            _context = context;
        }

        // GET: RolesPermisoes
        public async Task<IActionResult> Index()
        {
            var luchoSoftV1Context = _context.RolesPermisos.Include(r => r.IdPermisoRolesPermisosNavigation).Include(r => r.IdRolRolesPermisosNavigation);
            return View(await luchoSoftV1Context.ToListAsync());
        }

        // GET: RolesPermisoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RolesPermisos == null)
            {
                return NotFound();
            }

            var rolesPermiso = await _context.RolesPermisos
                .Include(r => r.IdPermisoRolesPermisosNavigation)
                .Include(r => r.IdRolRolesPermisosNavigation)
                .FirstOrDefaultAsync(m => m.IdRolesPermisos == id);
            if (rolesPermiso == null)
            {
                return NotFound();
            }

            return View(rolesPermiso);
        }

        // GET: RolesPermisoes/Create
        public IActionResult Create()
        {
            ViewData["IdPermisoRolesPermisos"] = new SelectList(_context.Permisos, "IdPermiso", "IdPermiso");
            ViewData["IdRolRolesPermisos"] = new SelectList(_context.Roles, "IdRol", "IdRol");
            return View();
        }

        // POST: RolesPermisoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRolesPermisos,FechaRolesPermisos,IdRolRolesPermisos,IdPermisoRolesPermisos")] RolesPermiso rolesPermiso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rolesPermiso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPermisoRolesPermisos"] = new SelectList(_context.Permisos, "IdPermiso", "IdPermiso", rolesPermiso.IdPermisoRolesPermisos);
            ViewData["IdRolRolesPermisos"] = new SelectList(_context.Roles, "IdRol", "IdRol", rolesPermiso.IdRolRolesPermisos);
            return View(rolesPermiso);
        }

        // GET: RolesPermisoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RolesPermisos == null)
            {
                return NotFound();
            }

            var rolesPermiso = await _context.RolesPermisos.FindAsync(id);
            if (rolesPermiso == null)
            {
                return NotFound();
            }
            ViewData["IdPermisoRolesPermisos"] = new SelectList(_context.Permisos, "IdPermiso", "IdPermiso", rolesPermiso.IdPermisoRolesPermisos);
            ViewData["IdRolRolesPermisos"] = new SelectList(_context.Roles, "IdRol", "IdRol", rolesPermiso.IdRolRolesPermisos);
            return View(rolesPermiso);
        }

        // POST: RolesPermisoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRolesPermisos,FechaRolesPermisos,IdRolRolesPermisos,IdPermisoRolesPermisos")] RolesPermiso rolesPermiso)
        {
            if (id != rolesPermiso.IdRolesPermisos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rolesPermiso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolesPermisoExists(rolesPermiso.IdRolesPermisos))
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
            ViewData["IdPermisoRolesPermisos"] = new SelectList(_context.Permisos, "IdPermiso", "IdPermiso", rolesPermiso.IdPermisoRolesPermisos);
            ViewData["IdRolRolesPermisos"] = new SelectList(_context.Roles, "IdRol", "IdRol", rolesPermiso.IdRolRolesPermisos);
            return View(rolesPermiso);
        }

        // GET: RolesPermisoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RolesPermisos == null)
            {
                return NotFound();
            }

            var rolesPermiso = await _context.RolesPermisos
                .Include(r => r.IdPermisoRolesPermisosNavigation)
                .Include(r => r.IdRolRolesPermisosNavigation)
                .FirstOrDefaultAsync(m => m.IdRolesPermisos == id);
            if (rolesPermiso == null)
            {
                return NotFound();
            }

            return View(rolesPermiso);
        }

        // POST: RolesPermisoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RolesPermisos == null)
            {
                return Problem("Entity set 'LuchoSoftV1Context.RolesPermisos'  is null.");
            }
            var rolesPermiso = await _context.RolesPermisos.FindAsync(id);
            if (rolesPermiso != null)
            {
                _context.RolesPermisos.Remove(rolesPermiso);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RolesPermisoExists(int id)
        {
          return (_context.RolesPermisos?.Any(e => e.IdRolesPermisos == id)).GetValueOrDefault();
        }
    }
}
