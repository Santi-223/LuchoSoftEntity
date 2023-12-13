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
    public class UsuariosController : Controller
    {
        private readonly LuchoSoftV1Context _context;

        public UsuariosController(LuchoSoftV1Context context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var luchoSoftV1Context = _context.Usuarios.Include(u => u.IdRolUsuariosNavigation);
            return View(await luchoSoftV1Context.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdRolUsuariosNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create(string nombre)
        {
            var usuario = new Usuario { NombreUsuario = nombre };
            ViewData["IdRolUsuarios"] = new SelectList(_context.Roles, "IdRol", "IdRol");
            return View(usuario);
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdUsuario,NombreUsuario,Email,Contraseña,EstadoUsuario,IdRolUsuarios")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Empleadoes");
            }
            ViewData["IdRolUsuarios"] = new SelectList(_context.Roles, "IdRol", "IdRol", usuario.IdRolUsuarios);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id, string nombre)
        {
            if (id == null && nombre == null)
            {
                return NotFound(); // Ambos id y nombre no pueden ser nulos al mismo tiempo
            }

            Usuario usuario = null;

            if (id != null)
            {
                usuario = await _context.Usuarios.FindAsync(id);
            }
            else if (nombre != null)
            {
                usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario == nombre);
            }

            if (usuario == null)
            {
                return NotFound(); // Usuario no encontrado
            }

            ViewData["IdRolUsuarios"] = new SelectList(_context.Roles, "IdRol", "IdRol", usuario.IdRolUsuarios);
            return View(usuario);
        }


        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdUsuario,NombreUsuario,Email,Contraseña,EstadoUsuario,IdRolUsuarios")] Usuario usuario)
        {

            Console.WriteLine(id);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.IdUsuario))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Empleadoes");
            }
            ViewData["IdRolUsuarios"] = new SelectList(_context.Roles, "IdRol", "IdRol", usuario.IdRolUsuarios);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdRolUsuariosNavigation)
                .FirstOrDefaultAsync(m => m.IdUsuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'LuchoSoftV1Context.Usuarios'  is null.");
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool UsuarioExists(int id)
        {
          return (_context.Usuarios?.Any(e => e.IdUsuario == id)).GetValueOrDefault();
        }
    }
}
