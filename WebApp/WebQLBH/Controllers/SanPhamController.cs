
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebQLBH.Data;
using WebQLBH.Models;

public class SanPhamController : Controller
{
    private readonly AppDbContext _context;

    public SanPhamController(AppDbContext context)
    {
        _context = context;
    }

    // GET: SANPHAMS
    public async Task<IActionResult> Index()    
    {
        return View(_context.SanPham_DS());
    }

    // GET: SANPHAMS/Details/5
    public async Task<IActionResult> Details(string masp)
    {
        return View(_context.SanPham_CT(masp));
    }

    // GET: SANPHAMS/Create
    public IActionResult Create()
    {
        ViewBag.LoaiSPs = new SelectList(_context.LoaiSanPham_DS(), "MaLSP", "TenLSP");
        ViewBag.DonViTinhs = new SelectList(_context.DonViTinh_DS(), "MaDVT", "TenDVT");
        return View();
    }

    // POST: SANPHAMS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("TenSP,DonGia,AnhSP,MoTaTT,ThongTinCT,MaLSP,LoaiSanPham,MaDVT,DonViTinh")] SanPham sanpham)
    {
        if (ModelState.IsValid)
        {
            _context.SanPham_Them(sanpham);
            return RedirectToAction(nameof(Index));
        }

        ViewBag.LoaiSPs = new SelectList(_context.LoaiSanPham_DS(), "MaLSP", "TenLSP");
        ViewBag.DonViTinhs = new SelectList(_context.DonViTinh_DS(), "MaDVT", "TenDVT");
        return View(sanpham);
    }

    // GET: SANPHAMS/Edit/5
    public async Task<IActionResult> Edit(string masp)
    {
        ModelState.Clear();
        ViewBag.LoaiSPs = new SelectList(_context.LoaiSanPham_DS(), "MaLSP", "TenLSP");
        ViewBag.DonViTinhs = new SelectList(_context.DonViTinh_DS(), "MaDVT", "TenDVT");
        return View(_context.SanPham_CT(masp));
    }

    // POST: SANPHAMS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string? masp, [Bind("MaSP,TenSP,DonGia,AnhSP,MoTaTT,ThongTinCT,MaLSP,LoaiSanPham,MaDVT,DonViTinh")] SanPham sanpham)
    {
        if (masp != sanpham.MaSP)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.SanPham_Sua(sanpham);
            return RedirectToAction(nameof(Index));
        }

        ViewBag.LoaiSPs = new SelectList(_context.LoaiSanPham_DS(), "MaLSP", "TenLSP");
        ViewBag.DonViTinhs = new SelectList(_context.DonViTinh_DS(), "MaDVT", "TenDVT");
        return View(sanpham);
    }

    // GET: SANPHAMS/Delete/5
    public async Task<IActionResult> Delete(string masp)
    {
        return View(_context.SanPham_CT(masp));
    }

    // POST: SANPHAMS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(SanPham sp)
    {
        _context.SanPham_Xoa(sp.MaSP);
        return RedirectToAction(nameof(Index));
    }

    private bool SanPhamExists(string? masp)
    {
        return _context.SanPhams.Any(e => e.MaSP == masp);
    }
}
