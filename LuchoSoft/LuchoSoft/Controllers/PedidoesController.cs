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
    public class PedidoesController : Controller
    {
        private readonly LuchoSoftV1Context _context;

        public PedidoesController(LuchoSoftV1Context context)
        {
            _context = context;
        }

        // GET: Pedidoes
        public async Task<IActionResult> Index()
        {
            var luchoSoftV1Context = _context.Pedidos.Include(p => p.IdClientePedidosNavigation).Include(p => p.IdEmpleadoPedidosNavigation);
            return View(await luchoSoftV1Context.ToListAsync());
        }

        // GET: Pedidoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.IdClientePedidosNavigation)
                .Include(p => p.IdEmpleadoPedidosNavigation)
                .FirstOrDefaultAsync(m => m.IdPedido == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidoes/Create
        public IActionResult Create()
        {
            ViewData["IdClientePedidos"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente");
            ViewData["IdEmpleadoPedidos"] = new SelectList(_context.Empleados, "IdEmpleado", "IdEmpleado");
            return View();
        }

        // POST: Pedidoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPedido,Observaciones,FechaVenta,FechaPedido,EstadoPedido,TotalVenta,TotalPedido,IdClientePedidos,IdEmpleadoPedidos")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdClientePedidos"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", pedido.IdClientePedidos);
            ViewData["IdEmpleadoPedidos"] = new SelectList(_context.Empleados, "IdEmpleado", "IdEmpleado", pedido.IdEmpleadoPedidos);
            return View(pedido);
        }

        // GET: Pedidoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["IdClientePedidos"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", pedido.IdClientePedidos);
            ViewData["IdEmpleadoPedidos"] = new SelectList(_context.Empleados, "IdEmpleado", "IdEmpleado", pedido.IdEmpleadoPedidos);
            return View(pedido);
        }

        // POST: Pedidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPedido,Observaciones,FechaVenta,FechaPedido,EstadoPedido,TotalVenta,TotalPedido,IdClientePedidos,IdEmpleadoPedidos")] Pedido pedido)
        {
            if (id != pedido.IdPedido)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.IdPedido))
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
            ViewData["IdClientePedidos"] = new SelectList(_context.Clientes, "IdCliente", "IdCliente", pedido.IdClientePedidos);
            ViewData["IdEmpleadoPedidos"] = new SelectList(_context.Empleados, "IdEmpleado", "IdEmpleado", pedido.IdEmpleadoPedidos);
            return View(pedido);
        }

        // GET: Pedidoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.IdClientePedidosNavigation)
                .Include(p => p.IdEmpleadoPedidosNavigation)
                .FirstOrDefaultAsync(m => m.IdPedido == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pedidos == null)
            {
                return Problem("Entity set 'LuchoSoftV1Context.Pedidos'  is null.");
            }
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
          return (_context.Pedidos?.Any(e => e.IdPedido == id)).GetValueOrDefault();
        }
    }
}
