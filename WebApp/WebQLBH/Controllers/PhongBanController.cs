using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebQLBH.Models;
using WebQLBH.Data;

public class PhongBanController : Controller
{
    private readonly AppDbContext _context;

    public PhongBanController(AppDbContext context)
    {
        _context = context;
    }

    // GET: PHONGBANS
    public async Task<IActionResult> Index()    
    {
        return View(_context.PhongBan_DS());
    }

    // GET: PHONGBANS/Details/5
    public async Task<IActionResult> Details(byte mapb)
    {
        return View(_context.PhongBan_CT(mapb));
    }

    // GET: PHONGBANS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PHONGBANS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("TenPB")] PhongBan phongban)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _context.PhongBan_Them(phongban);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Lỗi: " + ex.Message);
            }
        }
        return View(phongban);
    }

    // GET: PHONGBANS/Edit/5
    public async Task<IActionResult> Edit(byte mapb)
    {
        ModelState.Clear();
        return View(_context.PhongBan_CT(mapb));
    }

    // POST: PHONGBANS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(byte? mapb, [Bind("MaPB,TenPB")] PhongBan phongban)
    {
        if (mapb != phongban.MaPB)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.PhongBan_Sua(phongban);
            return RedirectToAction(nameof(Index));
        }
        return View(phongban);
    }

    // GET: PHONGBANS/Delete/5
    public async Task<IActionResult> Delete(byte mapb)
    {
        return View(_context.PhongBan_CT(mapb));
    }

    // POST: PHONGBANS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(byte mapb)
    {
        try
        {
            _context.PhongBan_Xoa(mapb);
            return RedirectToAction(nameof(Index));
        }
        catch (Microsoft.Data.SqlClient.SqlException ex)
        {
            if (ex.Number == 547)
                TempData["ErrorMessage"] = "Không thể xóa phòng ban này vì đang có bảng sử dụng";
            else
                TempData["ErrorMessage"] = "Lỗi: " + ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    private bool PhongBanExists(byte? mapb)
    {
        return _context.PhongBans.Any(e => e.MaPB == mapb);
    }
}
