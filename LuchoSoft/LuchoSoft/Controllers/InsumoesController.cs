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
    public class InsumoesController : Controller
    {
        private readonly LuchoSoftV1Context _context;

        public InsumoesController(LuchoSoftV1Context context)
        {
            _context = context;
        }

        // GET: Insumoes
        public async Task<IActionResult> Index()
        {
            var luchoSoftV1Context = _context.Insumos.Include(i => i.IdCategoriaInsumoInsumosNavigation);
            return View(await luchoSoftV1Context.ToListAsync());
        }

        // GET: Insumoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Insumos == null)
            {
                return NotFound();
            }

            var insumo = await _context.Insumos
                .Include(i => i.IdCategoriaInsumoInsumosNavigation)
                .FirstOrDefaultAsync(m => m.IdInsumo == id);
            if (insumo == null)
            {
                return NotFound();
            }

            return View(insumo);
        }

        public IActionResult GeneratePDF()
        {
            var insumos = _context.Insumos.Include(i => i.IdCategoriaInsumoInsumosNavigation).ToList();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter pdfWriter = new PdfWriter(memoryStream);
                PdfDocument pdfDocument = new PdfDocument(pdfWriter);
                Document document = new Document(pdfDocument);

                // Crear tabla para mostrar los insumos
                Table table = new Table(6); // Ajusta el número de columnas según tus datos
                table.SetWidth(UnitValue.CreatePercentValue(100)); // Ancho de la tabla (100% del ancho del documento)

                // Encabezados de la tabla
                table.AddHeaderCell("ID Insumo");
                table.AddHeaderCell("Nombre");
                table.AddHeaderCell("Unidades de Medida");
                table.AddHeaderCell("Stock");
                table.AddHeaderCell("Estado");
                table.AddHeaderCell("Categoría");

                // Datos de los insumos
                foreach (var insumo in insumos)
                {
                    table.AddCell(insumo.IdInsumo.ToString());
                    table.AddCell(insumo.NombreInsumo);
                    table.AddCell(insumo.UnidadesDeMedidaInsumo.ToString());
                    table.AddCell(insumo.StockInsumo.ToString());
                    table.AddCell(insumo.EstadoInsumo.ToString());
                    table.AddCell(insumo.IdCategoriaInsumoInsumosNavigation?.NombreCategoriaInsumos ?? "-");
                }

                document.Add(table); // Agregar la tabla al documento

                document.Close();
                return File(memoryStream.ToArray(), "application/pdf", "Insumos.pdf");
            }
        }


        // GET: Insumoes/Create
        public IActionResult Create()
        {
            ViewData["IdCategoriaInsumoInsumos"] = new SelectList(_context.CategoriaInsumos, "IdCategoriaInsumos", "IdCategoriaInsumos");
            return View();
        }

        // POST: Insumoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInsumo,ImagenInsumo,NombreInsumo,UnidadesDeMedidaInsumo,StockInsumo,EstadoInsumo,IdCategoriaInsumoInsumos")] Insumo insumo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insumo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoriaInsumoInsumos"] = new SelectList(_context.CategoriaInsumos, "IdCategoriaInsumos", "IdCategoriaInsumos", insumo.IdCategoriaInsumoInsumos);
            return View(insumo);
        }

        // GET: Insumoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Insumos == null)
            {
                return NotFound();
            }

            var insumo = await _context.Insumos.FindAsync(id);
            if (insumo == null)
            {
                return NotFound();
            }
            ViewData["IdCategoriaInsumoInsumos"] = new SelectList(_context.CategoriaInsumos, "IdCategoriaInsumos", "IdCategoriaInsumos", insumo.IdCategoriaInsumoInsumos);
            return View(insumo);
        }

        // POST: Insumoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInsumo,ImagenInsumo,NombreInsumo,UnidadesDeMedidaInsumo,StockInsumo,EstadoInsumo,IdCategoriaInsumoInsumos")] Insumo insumo)
        {
            if (id != insumo.IdInsumo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insumo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsumoExists(insumo.IdInsumo))
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
            ViewData["IdCategoriaInsumoInsumos"] = new SelectList(_context.CategoriaInsumos, "IdCategoriaInsumos", "IdCategoriaInsumos", insumo.IdCategoriaInsumoInsumos);
            return View(insumo);
        }

        // GET: Insumoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Insumos == null)
            {
                return NotFound();
            }

            var insumo = await _context.Insumos
                .Include(i => i.IdCategoriaInsumoInsumosNavigation)
                .FirstOrDefaultAsync(m => m.IdInsumo == id);
            if (insumo == null)
            {
                return NotFound();
            }

            return View(insumo);
        }

        // POST: Insumoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Insumos == null)
            {
                return Problem("Entity set 'LuchoSoftV1Context.Insumos'  is null.");
            }
            var insumo = await _context.Insumos.FindAsync(id);
            if (insumo != null)
            {
                _context.Insumos.Remove(insumo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsumoExists(int id)
        {
          return (_context.Insumos?.Any(e => e.IdInsumo == id)).GetValueOrDefault();
        }
    }
}
