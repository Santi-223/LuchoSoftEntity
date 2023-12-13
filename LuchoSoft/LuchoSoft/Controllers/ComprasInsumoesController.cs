using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LuchoSoft.Models;
using iText.Kernel.Pdf;
using iText.Layout.Properties;
using iText.Layout;
using iText.Layout.Element;

namespace LuchoSoft.Controllers
{
    public class ComprasInsumoesController : Controller
    {
        private readonly LuchoSoftV1Context _context;

        public ComprasInsumoesController(LuchoSoftV1Context context)
        {
            _context = context;
        }

        // GET: ComprasInsumoes
        public async Task<IActionResult> Index()
        {
            var luchoSoftV1Context = _context.ComprasInsumos.Include(c => c.IdCompraComprasInsumosNavigation).Include(c => c.IdInsumoComprasInsumosNavigation);
            return View(await luchoSoftV1Context.ToListAsync());
        }

        // GET: ComprasInsumoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ComprasInsumos == null)
            {
                return NotFound();
            }

            var comprasInsumo = await _context.ComprasInsumos
                .Include(c => c.IdCompraComprasInsumosNavigation)
                .Include(c => c.IdInsumoComprasInsumosNavigation)
                .FirstOrDefaultAsync(m => m.IdComprasInsumos == id);
            if (comprasInsumo == null)
            {
                return NotFound();
            }

            return View(comprasInsumo);
        }

        public IActionResult GeneratePDF()
        {
            var comprasInsumos = _context.ComprasInsumos
                .Include(ci => ci.IdCompraComprasInsumosNavigation)
                .Include(ci => ci.IdInsumoComprasInsumosNavigation)
                .ToList();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter pdfWriter = new PdfWriter(memoryStream);
                PdfDocument pdfDocument = new PdfDocument(pdfWriter);
                Document document = new Document(pdfDocument);

                // Crear tabla para mostrar las compras de insumos
                Table table = new Table(5); // Ajusta el número de columnas según tus datos
                table.SetWidth(UnitValue.CreatePercentValue(100)); // Ancho de la tabla (100% del ancho del documento)

                // Encabezados de la tabla
                table.AddHeaderCell("ID Compra Insumo");
                table.AddHeaderCell("Cantidad");
                table.AddHeaderCell("Precio");
                table.AddHeaderCell("Compra");
                table.AddHeaderCell("Insumo");

                // Datos de las compras de insumos
                foreach (var compraInsumo in comprasInsumos)
                {
                    table.AddCell(compraInsumo.IdComprasInsumos.ToString());
                    table.AddCell(compraInsumo.CantidadInsumoComprasInsumos.ToString());
                    table.AddCell(compraInsumo.PrecioInsumoComprasInsumos.ToString());
                    table.AddCell(compraInsumo.IdCompraComprasInsumosNavigation?.NombreCompra ?? "-");
                    table.AddCell(compraInsumo.IdInsumoComprasInsumosNavigation?.NombreInsumo ?? "-");
                }

                document.Add(table); // Agregar la tabla al documento

                document.Close();
                return File(memoryStream.ToArray(), "application/pdf", "ComprasInsumos.pdf");
            }
        }


        // GET: ComprasInsumoes/Create
        public IActionResult Create()
        {
            ViewData["IdCompraComprasInsumos"] = new SelectList(_context.Compras, "IdCompra", "IdCompra");
            ViewData["IdInsumoComprasInsumos"] = new SelectList(_context.Insumos, "IdInsumo", "IdInsumo");
            return View();
        }

        // POST: ComprasInsumoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdComprasInsumos,CantidadInsumoComprasInsumos,PrecioInsumoComprasInsumos,IdCompraComprasInsumos,IdInsumoComprasInsumos")] ComprasInsumo comprasInsumo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comprasInsumo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCompraComprasInsumos"] = new SelectList(_context.Compras, "IdCompra", "IdCompra", comprasInsumo.IdCompraComprasInsumos);
            ViewData["IdInsumoComprasInsumos"] = new SelectList(_context.Insumos, "IdInsumo", "IdInsumo", comprasInsumo.IdInsumoComprasInsumos);
            return View(comprasInsumo);
        }

        // GET: ComprasInsumoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ComprasInsumos == null)
            {
                return NotFound();
            }

            var comprasInsumo = await _context.ComprasInsumos.FindAsync(id);
            if (comprasInsumo == null)
            {
                return NotFound();
            }
            ViewData["IdCompraComprasInsumos"] = new SelectList(_context.Compras, "IdCompra", "IdCompra", comprasInsumo.IdCompraComprasInsumos);
            ViewData["IdInsumoComprasInsumos"] = new SelectList(_context.Insumos, "IdInsumo", "IdInsumo", comprasInsumo.IdInsumoComprasInsumos);
            return View(comprasInsumo);
        }

        // POST: ComprasInsumoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdComprasInsumos,CantidadInsumoComprasInsumos,PrecioInsumoComprasInsumos,IdCompraComprasInsumos,IdInsumoComprasInsumos")] ComprasInsumo comprasInsumo)
        {
            if (id != comprasInsumo.IdComprasInsumos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comprasInsumo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComprasInsumoExists(comprasInsumo.IdComprasInsumos))
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
            ViewData["IdCompraComprasInsumos"] = new SelectList(_context.Compras, "IdCompra", "IdCompra", comprasInsumo.IdCompraComprasInsumos);
            ViewData["IdInsumoComprasInsumos"] = new SelectList(_context.Insumos, "IdInsumo", "IdInsumo", comprasInsumo.IdInsumoComprasInsumos);
            return View(comprasInsumo);
        }

        // GET: ComprasInsumoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ComprasInsumos == null)
            {
                return NotFound();
            }

            var comprasInsumo = await _context.ComprasInsumos
                .Include(c => c.IdCompraComprasInsumosNavigation)
                .Include(c => c.IdInsumoComprasInsumosNavigation)
                .FirstOrDefaultAsync(m => m.IdComprasInsumos == id);
            if (comprasInsumo == null)
            {
                return NotFound();
            }

            return View(comprasInsumo);
        }

        // POST: ComprasInsumoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ComprasInsumos == null)
            {
                return Problem("Entity set 'LuchoSoftV1Context.ComprasInsumos'  is null.");
            }
            var comprasInsumo = await _context.ComprasInsumos.FindAsync(id);
            if (comprasInsumo != null)
            {
                _context.ComprasInsumos.Remove(comprasInsumo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComprasInsumoExists(int id)
        {
          return (_context.ComprasInsumos?.Any(e => e.IdComprasInsumos == id)).GetValueOrDefault();
        }
    }
}
