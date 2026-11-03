using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebQLBH.Models
{
    public class NhanVien
    {
        [Key]
        [Display(Name = "Mã nhân viên")]
        public byte MaNV { get; set; }

        [Display(Name = "Họ")]
        public string HoNV { get; set; }

        [Display(Name = "Tên")]
        public string TenNV { get; set; }

        [Display(Name = "Điện thoại")]
        public string DienThoai { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Tên đăng nhập")]
        public string TenDN { get; set; }

        [Display(Name = "Mật khẩu")]
        public byte[]? MatKhau { get; set; }

        [Display(Name = "Ảnh nhân viên")]
        public string? AnhNV { get; set; }

        [Display(Name = "Mã phòng ban")]
        public byte MaPB { get; set; }
        [ForeignKey("MaPB")]
        public virtual PhongBan? PhongBan { get; set; }
    }
}
