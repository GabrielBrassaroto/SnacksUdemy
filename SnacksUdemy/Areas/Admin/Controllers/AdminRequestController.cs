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
using SnacksUdemy.ViewModels;

namespace SnacksUdemy.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminRequestController : Controller
    {
        private readonly AppDbContext _context;

        public AdminRequestController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AdminRequest
        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Name")
        {
            var resultado = _context.Requests.AsNoTracking()
                                      .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                resultado = resultado.Where(p => p.Name.Contains(filter));
            }

            var model = await PagingList.CreateAsync(resultado, 3, pageindex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(model);
        }



        public IActionResult RequestSnack(int? id)
        {
            var reuqest = _context.Requests
                .Include(pd => pd.RequestItens)
                .ThenInclude(s => s.Snack) //include snack for reuqest
                .FirstOrDefault(p => p.RequestId == id);

            if (reuqest == null)
            {
                Response.StatusCode = 404;
                return View("RequestNotFound", id.Value);

            }

            RequestSnackViewModel requestSnacks = new RequestSnackViewModel()
            {
                Request = reuqest,
                OrderDetails = reuqest.RequestItens
            };

            return View(requestSnacks); 

        }


        // GET: Admin/AdminRequest/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Requests == null)
            {
                return NotFound();
            }

            var request = await _context.Requests
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // GET: Admin/AdminRequest/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AdminRequest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequestId,Name,LastName,Adress,Complement,Cep,State,City,Phone,Email,DateRequest,OrderShippingDate")] Request request)
        {
            if (ModelState.IsValid)
            {
                _context.Add(request);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        // GET: Admin/AdminRequest/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Requests == null)
            {
                return NotFound();
            }

            var request = await _context.Requests.FindAsync(id);
            if (request == null)
            {
                return NotFound();
            }
            return View(request);
        }

        // POST: Admin/AdminRequest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequestId,Name,LastName,Adress,Complement,Cep,State,City,Phone,Email,DateRequest,OrderShippingDate")] Request request)
        {
            if (id != request.RequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(request);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RequestExists(request.RequestId))
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
            return View(request);
        }

        // GET: Admin/AdminRequest/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Requests == null)
            {
                return NotFound();
            }

            var request = await _context.Requests
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        // POST: Admin/AdminRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Requests == null)
            {
                return Problem("Entity set 'AppDbContext.Requests'  is null.");
            }
            var request = await _context.Requests.FindAsync(id);
            if (request != null)
            {
                _context.Requests.Remove(request);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RequestExists(int id)
        {
            return _context.Requests.Any(e => e.RequestId == id);
        }
    }
}
