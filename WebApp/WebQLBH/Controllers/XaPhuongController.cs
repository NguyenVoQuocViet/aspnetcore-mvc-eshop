
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebQLBH.Models;
using WebQLBH.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

public class XaPhuongController : Controller
{
    private readonly AppDbContext _context;

    public XaPhuongController(AppDbContext context)
    {
        _context = context;
    }

    // GET: XAPHUONGS
    public async Task<IActionResult> Index()    
    {
        return View(_context.XaPhuong_DS());
    }

    // GET: XAPHUONGS/Details/5
    public async Task<IActionResult> Details(string maxp)
    {
        return View(_context.XaPhuong_CT(maxp));
    }

    // GET: XAPHUONGS/Create
    public IActionResult Create()
    {
        ViewBag.Tinhs = new SelectList(_context.Tinh_DS(), "MaTinh", "TenTinh");
        return View();
    }

    // POST: XAPHUONGS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MaXP,TenXP,MaTinh")] XaPhuong xaphuong)
    {
        if (ModelState.IsValid)
        {
            _context.XaPhuong_Them(xaphuong);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Tinhs = new SelectList(_context.Tinh_DS(), "MaTinh", "TenTinh");
        return View(xaphuong);
    }

    // GET: XAPHUONGS/Edit/5
    public async Task<IActionResult> Edit(string maxp)
    {
        ModelState.Clear();
        ViewBag.Tinhs = new SelectList(_context.Tinh_DS(), "MaTinh", "TenTinh");
        return View(_context.XaPhuong_CT(maxp));
    }

    // POST: XAPHUONGS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string? maxp, [Bind("MaXP,TenXP,MaTinh")] XaPhuong xaphuong)
    {
        if (maxp != xaphuong.MaXP)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.XaPhuong_Sua(xaphuong);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Tinhs = new SelectList(_context.Tinh_DS(), "MaTinh", "TenTinh");
        return View(xaphuong);
    }

    // GET: XAPHUONGS/Delete/5
    public async Task<IActionResult> Delete(string maxp)
    {
        return View(_context.XaPhuong_CT(maxp));
    }

    // POST: XAPHUONGS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(XaPhuong xp)
    {
        try
        {
            _context.XaPhuong_Xoa(xp.MaXP);
            return RedirectToAction(nameof(Index));
        }
        catch (Microsoft.Data.SqlClient.SqlException ex)
        {
            if (ex.Number == 547)
                TempData["ErrorMessage"] = "Không thể xóa xã phường này vì đang có dữ liệu sử dụng";
            else
                TempData["ErrorMessage"] = "Lỗi: " + ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    private bool XaPhuongExists(string? maxp)
    {
        return _context.XaPhuongs.Any(e => e.MaXP == maxp);
    }
}
