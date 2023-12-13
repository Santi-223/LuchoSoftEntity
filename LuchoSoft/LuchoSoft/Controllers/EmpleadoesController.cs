using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LuchoSoft.Models;
using Rotativa.AspNetCore;

namespace LuchoSoft.Controllers
{
    public class EmpleadoesController : Controller
    {
        private readonly LuchoSoftV1Context _context;

        public EmpleadoesController(LuchoSoftV1Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> GenerarPDF()
        {
            var luchoSoftV1Context = _context.Usuarios.Include(u => u.IdRolUsuariosNavigation);
            var listaUsuarios = await luchoSoftV1Context.ToListAsync();
            var empleadosContext = _context.Empleados;
            var listaEmpleados = await empleadosContext.ToListAsync();

            EmpleadosUsuarios viewModel = new EmpleadosUsuarios
            {
                ListaEmpleados = listaEmpleados,
                ListaUsuarios = listaUsuarios
            };
            return new ViewAsPdf("reportes", viewModel); // Reemplaza "NombreDeTuVista" con el nombre de tu vista
        }

        // GET: Empleadoes
        public async Task<IActionResult> Index()
        {
            var luchoSoftV1Context = _context.Usuarios.Include(u => u.IdRolUsuariosNavigation);
            var listaUsuarios = await luchoSoftV1Context.ToListAsync();
            var empleadosContext = _context.Empleados;
            var listaEmpleados = await empleadosContext.ToListAsync();

            EmpleadosUsuarios viewModel = new EmpleadosUsuarios
            {
                ListaEmpleados = listaEmpleados,
                ListaUsuarios = listaUsuarios
            };

            return View(viewModel);
        }

        // GET: Empleadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FirstOrDefaultAsync(m => m.IdEmpleado == id);

            if (empleado == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdRolUsuariosNavigation)
                .FirstOrDefaultAsync(m => m.NombreUsuario == empleado.NombreEmpleado);

            if (usuario == null)
            {
                return NotFound();
            }

            EmpleadosUsuarios viewModel = new EmpleadosUsuarios
            {
                ListaEmpleados = new List<Empleado> { empleado },
                ListaUsuarios = new List<Usuario> { usuario }
            };

            return View(viewModel);
        }


        // GET: Empleadoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEmpleado,ImagenEmpleado,NombreEmpleado,TelefonoEmpleado,DireccionEmpleado,EstadoEmpleado")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Usuarios", new {nombre = empleado.NombreEmpleado});
            }
            return View(empleado);
        }


        // GET: Empleadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            return View(empleado);
        }

        // POST: Empleadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEmpleado,ImagenEmpleado,NombreEmpleado,TelefonoEmpleado,DireccionEmpleado,EstadoEmpleado")] Empleado empleado)
        {
            if (id != empleado.IdEmpleado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.IdEmpleado))
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
            return View(empleado);
        }

        // GET: Empleadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleados
                .FirstOrDefaultAsync(m => m.IdEmpleado == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nombreEmpleado = "";
            if (_context.Empleados == null)
            {
                return Problem("Entity set 'LuchoSoftV1Context.Empleados'  is null.");
            }
            var empleado = await _context.Empleados.FindAsync(id);

            var usuario = await _context.Usuarios
                .Include(u => u.IdRolUsuariosNavigation)
                .FirstOrDefaultAsync(m => m.NombreUsuario == empleado.NombreEmpleado);

            if (empleado != null)
            {
                _context.Usuarios.Remove(usuario);
                _context.Empleados.Remove(empleado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
          return (_context.Empleados?.Any(e => e.IdEmpleado == id)).GetValueOrDefault();
        }
    }
}
