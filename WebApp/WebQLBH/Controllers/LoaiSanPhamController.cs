
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebQLBH.Models;
using WebQLBH.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

public class LoaiSanPhamController : Controller
{
    private readonly AppDbContext _context;

    public LoaiSanPhamController(AppDbContext context)
    {
        _context = context;
    }

    // GET: LOAISANPHAMS
    public async Task<IActionResult> Index()    
    {
        return View(_context.LoaiSanPham_DS());
    }

    // GET: LOAISANPHAMS/Details/5
    public async Task<IActionResult> Details(short malsp)
    {
        return View(_context.LoaiSanPham_CT(malsp));
    }

    // GET: LOAISANPHAMS/Create
    public IActionResult Create()
    {
        ViewBag.NhomSPs = new SelectList(_context.NhomSanPham_DS(), "MaNSP", "TenNSP");
        return View();
    }

    // POST: LOAISANPHAMS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("TenLSP,MaNSP")] LoaiSanPham loaisanpham)
    {
        if (ModelState.IsValid)
        {
            _context.LoaiSanPham_Them(loaisanpham);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.NhomSPs = new SelectList(_context.NhomSanPham_DS(), "MaNSP", "TenNSP");
        return View(loaisanpham);
    }

    // GET: LOAISANPHAMS/Edit/5
    public async Task<IActionResult> Edit(short malsp)
    {
        ModelState.Clear();
        ViewBag.NhomSPs = new SelectList(_context.NhomSanPham_DS(), "MaNSP", "TenNSP");
        return View(_context.LoaiSanPham_CT(malsp));
    }

    // POST: LOAISANPHAMS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(short? malsp, [Bind("MaLSP,TenLSP,MaNSP")] LoaiSanPham loaisanpham)
    {
        if (malsp != loaisanpham.MaLSP)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.LoaiSanPham_Sua(loaisanpham);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.NhomSPs = new SelectList(_context.NhomSanPham_DS(), "MaNSP", "TenNSP");
        return View(loaisanpham);
    }

    // GET: LOAISANPHAMS/Delete/5
    public async Task<IActionResult> Delete(short malsp)
    {
        return View(_context.LoaiSanPham_CT(malsp));
    }

    // POST: LOAISANPHAMS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(LoaiSanPham lsp)
    {
        try
        {
            _context.LoaiSanPham_Xoa(lsp);
            return RedirectToAction(nameof(Index));
        }
        catch (Microsoft.Data.SqlClient.SqlException ex)
        {
            if (ex.Number == 547)
                TempData["ErrorMessage"] = "Không thể xóa loại sản phẩm này vì đang có sản phẩm sử dụng";
            else
                TempData["ErrorMessage"] = "Lỗi: " + ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    private bool LoaiSanPhamExists(short? malsp)
    {
        return _context.LoaiSanPhams.Any(e => e.MaLSP == malsp);
    }
}
