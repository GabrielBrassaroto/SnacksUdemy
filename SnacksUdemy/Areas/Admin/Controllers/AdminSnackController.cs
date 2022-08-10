using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using SnacksUdemy;
using SnacksUdemy.Models;

namespace SnacksUdemy.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminSnackController : Controller
    {
        private readonly AppDbContext _context;

        public AdminSnackController(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Name")
        {
            var resultado = _context.Snacks.AsNoTracking()
                                      .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                resultado = resultado.Where(p => p.Name.Contains(filter));
            }

            var model = await PagingList.CreateAsync(resultado, 3, pageindex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(model);
        }



        // GET: Admin/AdminSnack/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Snacks == null)
            {
                return NotFound();
            }

            var snack = await _context.Snacks
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.SnackId == id);
            if (snack == null)
            {
                return NotFound();
            }

            return View(snack);
        }

        // GET: Admin/AdminSnack/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categorìes, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Admin/AdminSnack/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SnackId,Name,ShortDescripton,DetailedDescripton,Price,ImageUrl,ImageThumbnailUrl,IsFavoriteSnack,Stock,CategoryId")] Snack snack)
        {
            if (ModelState.IsValid)
            {
                _context.Add(snack);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categorìes, "CategoryId", "CategoryName", snack.CategoryId);
            return View(snack);
        }

        // GET: Admin/AdminSnack/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Snacks == null)
            {
                return NotFound();
            }

            var snack = await _context.Snacks.FindAsync(id);
            if (snack == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categorìes, "CategoryId", "CategoryName", snack.CategoryId);
            return View(snack);
        }

        // POST: Admin/AdminSnack/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SnackId,Name,ShortDescripton,DetailedDescripton,Price,ImageUrl,ImageThumbnailUrl,IsFavoriteSnack,Stock,CategoryId")] Snack snack)
        {
            if (id != snack.SnackId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(snack);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SnackExists(snack.SnackId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categorìes, "CategoryId", "CategoryName", snack.CategoryId);
            return View(snack);
        }

        // GET: Admin/AdminSnack/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Snacks == null)
            {
                return NotFound();
            }

            var snack = await _context.Snacks
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.SnackId == id);
            if (snack == null)
            {
                return NotFound();
            }

            return View(snack);
        }

        // POST: Admin/AdminSnack/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Snacks == null)
            {
                return Problem("Entity set 'AppDbContext.Snacks'  is null.");
            }
            var snack = await _context.Snacks.FindAsync(id);
            if (snack != null)
            {
                _context.Snacks.Remove(snack);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SnackExists(int id)
        {
          return _context.Snacks.Any(e => e.SnackId == id);
        }
    }
}
