
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebQLBH.Models;
using WebQLBH.Data;

public class NhomSanPhamController : Controller
{
    private readonly AppDbContext _context;

    public NhomSanPhamController(AppDbContext context)
    {
        _context = context;
    }

    // GET: NHOMSANPHAMS
    public async Task<IActionResult> Index()    
    {
        return View(_context.NhomSanPham_DS());
    }

    // GET: NHOMSANPHAMS/Details/5
    public async Task<IActionResult> Details(byte MaNSP)
    {
        return View(_context.NhomSanPham_CT(MaNSP));
    }

    // GET: NHOMSANPHAMS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: NHOMSANPHAMS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("TenNSP")] NhomSanPham nhomsanpham)
    {
        if (ModelState.IsValid)
        {
            _context.NhomSanPham_Them(nhomsanpham);
            return RedirectToAction(nameof(Index));
        }
        return View(nhomsanpham);
    }

    // GET: NHOMSANPHAMS/Edit/5
    public async Task<IActionResult> Edit(byte MaNSP)
    {
        ModelState.Clear();
        return View(_context.NhomSanPham_CT(MaNSP));
    }

    // POST: NHOMSANPHAMS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(byte? mansp, [Bind("MaNSP,TenNSP")] NhomSanPham nhomsanpham)
    {
        if (mansp != nhomsanpham.MaNSP)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            _context.NhomSanPham_Sua(nhomsanpham);
            return RedirectToAction(nameof(Index));
        }
        return View(nhomsanpham);
    }

    // GET: NHOMSANPHAMS/Delete/5
    public async Task<IActionResult> Delete(byte mansp)
    {
        return View(_context.NhomSanPham_CT(mansp));
    }

    // POST: NHOMSANPHAMS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(byte? mansp)
    {
        try
        {
            _context.NhomSanPham_Xoa(mansp);
            return RedirectToAction(nameof(Index));
        }
        catch (Microsoft.Data.SqlClient.SqlException ex)
        {
            if (ex.Number == 547)
                TempData["ErrorMessage"] = "Không thể xóa nhóm sản phẩm này vì đang có sản phẩm sử dụng";
            else
                TempData["ErrorMessage"] = "Lỗi: " + ex.Message;
            return RedirectToAction(nameof(Index));
        }
    }

    private bool NhomSanPhamExists(byte? mansp)
    {
        return _context.NhomSanPhams.Any(e => e.MaNSP == mansp);
    }
}
