
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebQLBH.Models;
using WebQLBH.Data;

public class TinhController : Controller
{
    private readonly AppDbContext _context;

    public TinhController(AppDbContext context)
    {
        _context = context;
    }

    // GET: TINHS
    public async Task<IActionResult> Index()    
    {
        return View(_context.Tinh_DS());
    }

    // GET: TINHS/Details/5
    public async Task<IActionResult> Details(string matinh)
    {

        return View(_context.Tinh_CT(matinh));
    }

    // GET: TINHS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: TINHS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MaTinh, TenTinh")] Tinh tinh)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _context.Tinh_Them(tinh);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi: " + ex.Message);
            }
        }
        return View(tinh);
    }

    // GET: TINHS/Edit/5
    public async Task<IActionResult> Edit(string matinh)
    {
        ModelState.Clear();
        return View(_context.Tinh_CT(matinh));
    }

    // POST: TINHS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string? matinh, [Bind("MaTinh,TenTinh")] Tinh tinh)
    {
        if (matinh != tinh.MaTinh)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.Tinh_Sua(tinh);
            return RedirectToAction(nameof(Index));
        }
        return View(tinh);
    }

    // GET: TINHS/Delete/5
    public async Task<IActionResult> Delete(string matinh)
    {
        return View(_context.Tinh_CT(matinh));
    }

    // POST: TINHS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string matinh)
    {
        try
        {
            _context.Tinh_Xoa(matinh);
            return RedirectToAction(nameof(Index));
        }
        catch (Microsoft.Data.SqlClient.SqlException ex)
        {
            if (ex.Number == 547)
                TempData["ErrorMessage"] = "Không thể xóa tỉnh này vì đang có bảng sử dụng";
            else
                TempData["ErrorMessage"] = "Lỗi: " + ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    private bool TinhExists(string? matinh)
    {
        return _context.Tinhs.Any(e => e.MaTinh == matinh);
    }
}
