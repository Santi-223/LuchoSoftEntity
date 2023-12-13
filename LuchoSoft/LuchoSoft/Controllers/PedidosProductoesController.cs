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
    public class PedidosProductoesController : Controller
    {
        private readonly LuchoSoftV1Context _context;

        public PedidosProductoesController(LuchoSoftV1Context context)
        {
            _context = context;
        }

        // GET: PedidosProductoes
        public async Task<IActionResult> Index()
        {
            var luchoSoftV1Context = _context.PedidosProductos.Include(p => p.IdPedidoPedidosProductosNavigation).Include(p => p.IdProductoPedidosProductosNavigation);
            return View(await luchoSoftV1Context.ToListAsync());
        }

        // GET: PedidosProductoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PedidosProductos == null)
            {
                return NotFound();
            }

            var pedidosProducto = await _context.PedidosProductos
                .Include(p => p.IdPedidoPedidosProductosNavigation)
                .Include(p => p.IdProductoPedidosProductosNavigation)
                .FirstOrDefaultAsync(m => m.IdPedidosProductos == id);
            if (pedidosProducto == null)
            {
                return NotFound();
            }

            return View(pedidosProducto);
        }

        // GET: PedidosProductoes/Create
        public IActionResult Create()
        {
            ViewData["IdPedidoPedidosProductos"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido");
            ViewData["IdProductoPedidosProductos"] = new SelectList(_context.Productos, "IdProducto", "IdProducto");
            return View();
        }

        // POST: PedidosProductoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPedidosProductos,FechaPedidoProducto,CantidadProducto,Subtotal,IdProductoPedidosProductos,IdPedidoPedidosProductos")] PedidosProducto pedidosProducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedidosProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPedidoPedidosProductos"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", pedidosProducto.IdPedidoPedidosProductos);
            ViewData["IdProductoPedidosProductos"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", pedidosProducto.IdProductoPedidosProductos);
            return View(pedidosProducto);
        }

        // GET: PedidosProductoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PedidosProductos == null)
            {
                return NotFound();
            }

            var pedidosProducto = await _context.PedidosProductos.FindAsync(id);
            if (pedidosProducto == null)
            {
                return NotFound();
            }
            ViewData["IdPedidoPedidosProductos"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", pedidosProducto.IdPedidoPedidosProductos);
            ViewData["IdProductoPedidosProductos"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", pedidosProducto.IdProductoPedidosProductos);
            return View(pedidosProducto);
        }

        // POST: PedidosProductoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPedidosProductos,FechaPedidoProducto,CantidadProducto,Subtotal,IdProductoPedidosProductos,IdPedidoPedidosProductos")] PedidosProducto pedidosProducto)
        {
            if (id != pedidosProducto.IdPedidosProductos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedidosProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidosProductoExists(pedidosProducto.IdPedidosProductos))
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
            ViewData["IdPedidoPedidosProductos"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", pedidosProducto.IdPedidoPedidosProductos);
            ViewData["IdProductoPedidosProductos"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", pedidosProducto.IdProductoPedidosProductos);
            return View(pedidosProducto);
        }

        // GET: PedidosProductoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PedidosProductos == null)
            {
                return NotFound();
            }

            var pedidosProducto = await _context.PedidosProductos
                .Include(p => p.IdPedidoPedidosProductosNavigation)
                .Include(p => p.IdProductoPedidosProductosNavigation)
                .FirstOrDefaultAsync(m => m.IdPedidosProductos == id);
            if (pedidosProducto == null)
            {
                return NotFound();
            }

            return View(pedidosProducto);
        }

        // POST: PedidosProductoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PedidosProductos == null)
            {
                return Problem("Entity set 'LuchoSoftV1Context.PedidosProductos'  is null.");
            }
            var pedidosProducto = await _context.PedidosProductos.FindAsync(id);
            if (pedidosProducto != null)
            {
                _context.PedidosProductos.Remove(pedidosProducto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidosProductoExists(int id)
        {
          return (_context.PedidosProductos?.Any(e => e.IdPedidosProductos == id)).GetValueOrDefault();
        }
    }
}
