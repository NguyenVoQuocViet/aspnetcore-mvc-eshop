using Microsoft.AspNetCore.Mvc;
using WebQLBH.Data;
using WebQLBH.Models;
public class AccountController : Controller
{
    private readonly AppDbContext _context;
    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(string TenDN, string MatKhau, string returnUrl = null)
    {
        var MaNV = _context.NhanVien_DangNhap(TenDN, MatKhau);
        if (MaNV == 0)
        {
            ModelState.AddModelError(string.Empty, "Sai tên đăng nhập hoặc mật khẩu.");
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // đọc thông tin chi tiết nhân viên và Lưu vào session
        var nv = _context.NhanVien_CT(MaNV);
        HttpContext.Session.SetString("Username", nv.TenDN);
        HttpContext.Session.SetString("DisplayName", nv.HoNV + " " + nv.TenNV);
        HttpContext.Session.SetString("MaNV", nv.MaNV.ToString());

        // Redirect về returnUrl nếu an toàn
        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction("Index", "NhanVien");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Remove("Username");
        HttpContext.Session.Remove("DisplayName");
        HttpContext.Session.Remove("MaNV");
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult ChangePassword()
    {
        if (string.IsNullOrEmpty(HttpContext.Session.GetString("Username")))
            return RedirectToAction("Login");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ChangePassword(string MatKhauCu, string MatKhauMoi, string MatKhauXacNhan)
    {
        var username = HttpContext.Session.GetString("Username");
        if (string.IsNullOrEmpty(username))
            return RedirectToAction("Login");

        if (string.IsNullOrEmpty(MatKhauCu) || string.IsNullOrEmpty(MatKhauMoi))
        {
            ModelState.AddModelError("", "Vui lòng nhập mật khẩu.");
            return View();
        }

        if (MatKhauMoi != MatKhauXacNhan)
        {
            ModelState.AddModelError("", "Mật khẩu xác nhận không trùng khớp.");
            return View();
        }

        try
        {
            var result = _context.NhanVien_DoiMatKhau(username, MatKhauCu, MatKhauMoi);
            if (result > 0)
            {
                TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";
                return RedirectToAction("Logout");
            }
            else
            {
                ModelState.AddModelError("", "Mật khẩu cũ không chính xác.");
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Lỗi: " + ex.Message);
        }
        return View();
    }

    [HttpGet]
    public IActionResult ResetPassword()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ResetPassword(string TenDN)
    {
        if (string.IsNullOrEmpty(TenDN))
        {
            ModelState.AddModelError("", "Vui lòng nhập tên đăng nhập.");
            return View();
        }

        try
        {
            var result = _context.NhanVien_ResetMatKhau(TenDN);
            if (result > 0)
            {
                TempData["SuccessMessage"] = "Mật khẩu đã được reset về mặc định. Vui lòng liên hệ quản trị viên.";
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError("", "Tên đăng nhập không tồn tại.");
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Lỗi: " + ex.Message);
        }
        return View();
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}
