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
    public class CategoriaInsumoesController : Controller
    {
        private readonly LuchoSoftV1Context _context;

        public CategoriaInsumoesController(LuchoSoftV1Context context)
        {
            _context = context;
        }

        // GET: CategoriaInsumoes
        public async Task<IActionResult> Index()
        {
              return _context.CategoriaInsumos != null ? 
                          View(await _context.CategoriaInsumos.ToListAsync()) :
                          Problem("Entity set 'LuchoSoftV1Context.CategoriaInsumos'  is null.");
        }

        // GET: CategoriaInsumoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CategoriaInsumos == null)
            {
                return NotFound();
            }

            var categoriaInsumo = await _context.CategoriaInsumos
                .FirstOrDefaultAsync(m => m.IdCategoriaInsumos == id);
            if (categoriaInsumo == null)
            {
                return NotFound();
            }

            return View(categoriaInsumo);
        }

        // GET: CategoriaInsumoes/Create
        public IActionResult Create()
        {
            return View();
        }



        // POST: CategoriaInsumoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCategoriaInsumos,NombreCategoriaInsumos,EstadoCategoriaInsumos")] CategoriaInsumo categoriaInsumo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoriaInsumo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoriaInsumo);
        }

        public IActionResult GeneratePDF()
        {
            var categoriasInsumos = _context.CategoriaInsumos.ToList();

            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter pdfWriter = new PdfWriter(memoryStream);
                PdfDocument pdfDocument = new PdfDocument(pdfWriter);
                Document document = new Document(pdfDocument);

                // Crear tabla para mostrar las categorías de insumos
                Table table = new Table(3); // Ajusta el número de columnas según tus datos
                table.SetWidth(UnitValue.CreatePercentValue(100)); // Ancho de la tabla (100% del ancho del documento)

                // Encabezados de la tabla
                table.AddHeaderCell("ID Categoría");
                table.AddHeaderCell("Nombre");
                table.AddHeaderCell("Estado");

                // Datos de las categorías de insumos
                foreach (var categoriaInsumo in categoriasInsumos)
                {
                    table.AddCell(categoriaInsumo.IdCategoriaInsumos.ToString());
                    table.AddCell(categoriaInsumo.NombreCategoriaInsumos);
                    table.AddCell(categoriaInsumo.EstadoCategoriaInsumos.ToString());
                }

                document.Add(table); // Agregar la tabla al documento

                document.Close();
                return File(memoryStream.ToArray(), "application/pdf", "CategoriasInsumos.pdf");
            }
        }


        // GET: CategoriaInsumoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CategoriaInsumos == null)
            {
                return NotFound();
            }

            var categoriaInsumo = await _context.CategoriaInsumos.FindAsync(id);
            if (categoriaInsumo == null)
            {
                return NotFound();
            }
            return View(categoriaInsumo);
        }

        // POST: CategoriaInsumoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCategoriaInsumos,NombreCategoriaInsumos,EstadoCategoriaInsumos")] CategoriaInsumo categoriaInsumo)
        {
            if (id != categoriaInsumo.IdCategoriaInsumos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoriaInsumo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaInsumoExists(categoriaInsumo.IdCategoriaInsumos))
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
            return View(categoriaInsumo);
        }

        // GET: CategoriaInsumoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CategoriaInsumos == null)
            {
                return NotFound();
            }

            var categoriaInsumo = await _context.CategoriaInsumos
                .FirstOrDefaultAsync(m => m.IdCategoriaInsumos == id);
            if (categoriaInsumo == null)
            {
                return NotFound();
            }

            return View(categoriaInsumo);
        }

        // POST: CategoriaInsumoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CategoriaInsumos == null)
            {
                return Problem("Entity set 'LuchoSoftV1Context.CategoriaInsumos'  is null.");
            }
            var categoriaInsumo = await _context.CategoriaInsumos.FindAsync(id);
            if (categoriaInsumo != null)
            {
                _context.CategoriaInsumos.Remove(categoriaInsumo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaInsumoExists(int id)
        {
          return (_context.CategoriaInsumos?.Any(e => e.IdCategoriaInsumos == id)).GetValueOrDefault();
        }
    }
}
