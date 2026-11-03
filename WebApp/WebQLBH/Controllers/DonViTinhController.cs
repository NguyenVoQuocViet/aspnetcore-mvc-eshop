
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebQLBH.Models;
using WebQLBH.Data;

public class DonViTinhController : Controller
{
    private readonly AppDbContext _context;

    public DonViTinhController(AppDbContext context)
    {
        _context = context;
    }

    // GET: DONVITINHS
    public async Task<IActionResult> Index()    
    {
        return View(_context.DonViTinh_DS());
    }

    // GET: DONVITINHS/Details/5
    public async Task<IActionResult> Details(DonViTinh dvt)
    {
        return View(_context.DonViTinh_CT(dvt.MaDVT));
    }

    // GET: DONVITINHS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: DONVITINHS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("TenDVT")] DonViTinh donvitinh)
    {
        if (ModelState.IsValid)
        {
            _context.DonViTinh_Them(donvitinh);
            return RedirectToAction(nameof(Index));
        }
        return View(donvitinh);
    }

    // GET: DONVITINHS/Edit/5
    public async Task<IActionResult> Edit(DonViTinh dvt)
    {
        ModelState.Clear();
        return View(_context.DonViTinh_CT(dvt.MaDVT));
    }

    // POST: DONVITINHS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(byte? madvt, [Bind("MaDVT,TenDVT")] DonViTinh donvitinh)
    {
        if (madvt != donvitinh.MaDVT)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.DonViTinh_Sua(donvitinh);
            return RedirectToAction(nameof(Index));
        }
        return View(donvitinh);
    }

    // GET: DONVITINHS/Delete/5
    public async Task<IActionResult> Delete(DonViTinh dvt)
    {
        return View(_context.DonViTinh_CT(dvt.MaDVT));
    }

    // POST: DONVITINHS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(byte? madvt)
    {
        try
        {
            _context.DonViTinh_Xoa(madvt);
            return RedirectToAction(nameof(Index));
        }
        catch (Microsoft.Data.SqlClient.SqlException ex)
        {
            if (ex.Number == 547)
                TempData["ErrorMessage"] = "Không thể xóa đơn vị tính này vì đang có sản phẩm sử dụng";
            else
                TempData["ErrorMessage"] = "Lỗi: " + ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    private bool DonViTinhExists(byte? madvt)
    {
        return _context.DonViTinhs.Any(e => e.MaDVT == madvt);
    }
}
