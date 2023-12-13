using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LuchoSoft.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using LuchoSoft.Models.ViewModels;

namespace LuchoSoft.Controllers
{
    public class ComprasController : Controller
    {
        private readonly LuchoSoftV1Context _context;

        public ComprasController(LuchoSoftV1Context context)
        {
            _context = context;
        }



        // GET: Compras
        public async Task<IActionResult> Index()
        {
            var luchoSoftV1Context = _context.Compras.Include(c => c.IdProveedorComprasNavigation);
            return View(await luchoSoftV1Context.ToListAsync());
        }

        public IActionResult GeneratePDF()
        {
            var compras = _context.Compras.Include(c => c.IdProveedorComprasNavigation).ToList();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter pdfWriter = new PdfWriter(memoryStream);
                PdfDocument pdfDocument = new PdfDocument(pdfWriter);
                Document document = new Document(pdfDocument);

                // Crear tabla para mostrar las compras
                Table table = new Table(6); // Ajusta el número de columnas según tus datos
                table.SetWidth(UnitValue.CreatePercentValue(100)); // Ancho de la tabla (100% del ancho del documento)

                // Encabezados de la tabla
                table.AddHeaderCell("ID Compra");
                table.AddHeaderCell("Nombre");
                table.AddHeaderCell("Fecha");
                table.AddHeaderCell("Estado");
                table.AddHeaderCell("Total");
                table.AddHeaderCell("Proveedor");

                // Datos de las compras
                foreach (var compra in compras)
                {
                    table.AddCell(compra.IdCompra.ToString());
                    table.AddCell(compra.NombreCompra);
                    table.AddCell(compra.FechaCompra.ToString());
                    table.AddCell(compra.EstadoCompra.ToString());
                    table.AddCell(compra.TotalCompra.ToString());
                    table.AddCell(compra.IdProveedorComprasNavigation?.NombreProveedor ?? "-");
                }

                document.Add(table); // Agregar la tabla al documento

                document.Close();
                return File(memoryStream.ToArray(), "application/pdf", "Compras.pdf");
            }
        }


        // GET: Compras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.IdProveedorComprasNavigation)
                .FirstOrDefaultAsync(m => m.IdCompra == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // GET: Compras/Create
        public IActionResult Create()
        {
            ViewData["IdProveedorCompras"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor");
            return View();
        }


        // POST: Compras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCompra,NombreCompra,FechaCompra,EstadoCompra,TotalCompra,IdProveedorCompras")] Compra compra)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compra);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProveedorCompras"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor", compra.IdProveedorCompras);
            return View(compra);
        }

        // GET: Compras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras.FindAsync(id);
            if (compra == null)
            {
                return NotFound();
            }
            ViewData["IdProveedorCompras"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor", compra.IdProveedorCompras);
            return View(compra);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCompra,NombreCompra,FechaCompra,EstadoCompra,TotalCompra,IdProveedorCompras")] Compra compra)
        {
            if (id != compra.IdCompra)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compra);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraExists(compra.IdCompra))
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
            ViewData["IdProveedorCompras"] = new SelectList(_context.Proveedores, "IdProveedor", "IdProveedor", compra.IdProveedorCompras);
            return View(compra);
        }

        // GET: Compras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Compras == null)
            {
                return NotFound();
            }

            var compra = await _context.Compras
                .Include(c => c.IdProveedorComprasNavigation)
                .FirstOrDefaultAsync(m => m.IdCompra == id);
            if (compra == null)
            {
                return NotFound();
            }

            return View(compra);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Compras == null)
            {
                return Problem("Entity set 'LuchoSoftV1Context.Compras'  is null.");
            }
            var compra = await _context.Compras.FindAsync(id);
            if (compra != null)
            {
                _context.Compras.Remove(compra);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompraExists(int id)
        {
          return (_context.Compras?.Any(e => e.IdCompra == id)).GetValueOrDefault();
        }
    }
}
