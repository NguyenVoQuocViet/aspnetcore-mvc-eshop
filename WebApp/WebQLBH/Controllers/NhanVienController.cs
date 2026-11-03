using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebQLBH.Models;
using WebQLBH.Data;

public class NhanVienController : Controller
{
    private readonly AppDbContext _context;

    public NhanVienController(AppDbContext context)
    {
        _context = context;
    }

    // GET: NHANVIENS
    public async Task<IActionResult> Index()    
    {
        return View(_context.NhanVien_DS());
    }

    // GET: NHANVIENS/Details/5
    public async Task<IActionResult> Details(byte manv)
    {
        return View(_context.NhanVien_CT(manv));
    }

    // GET: NHANVIENS/Create
    public IActionResult Create()
    {
        ViewBag.PhongBans = new SelectList(_context.PhongBan_DS(), "MaPB", "TenPB");
        return View();
    }

    // POST: NHANVIENS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("HoNV,TenNV,DienThoai,Email,TenDN,AnhNV,MaPB")] NhanVien nhanvien)
    {
        if (ModelState.IsValid)
        {
            _context.NhanVien_Them(nhanvien);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.PhongBans = new SelectList(_context.PhongBan_DS(), "MaPB", "TenPB");
        return View(nhanvien);
    }

    // GET: NHANVIENS/Edit/5
    public async Task<IActionResult> Edit(byte manv)
    {
        ModelState.Clear();
        ViewBag.PhongBans = new SelectList(_context.PhongBan_DS(), "MaPB", "TenPB");
        return View(_context.NhanVien_CT(manv));
    }

    // POST: NHANVIENS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(byte? manv, [Bind("MaNV,HoNV,TenNV,DienThoai,Email,TenDN,AnhNV,MaPB")] NhanVien nhanvien)
    {
        if (manv != nhanvien.MaNV)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.NhanVien_Sua(nhanvien);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.PhongBans = new SelectList(_context.PhongBan_DS(), "MaPB", "TenPB");
        return View(nhanvien);
    }

    // GET: NHANVIENS/Delete/5
    public async Task<IActionResult> Delete(byte manv)
    {
        return View(_context.NhanVien_CT(manv));
    }

    // POST: NHANVIENS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(NhanVien nv)
    {
        _context.NhanVien_Xoa(nv.MaNV);
        return RedirectToAction(nameof(Index));
    }

    private bool NhanVienExists(byte? manv)
    {
        return _context.NhanViens.Any(e => e.MaNV == manv);
    }
}
