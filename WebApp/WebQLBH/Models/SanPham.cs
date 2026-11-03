using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebQLBH.Models
{
    public class SanPham
    {
        [Key]
        [Display(Name = "Mã sản phẩm")]
        public string MaSP { get; set; } = string.Empty;

        [Display(Name = "Tên sản phẩm")]
        public string TenSP { get; set; } = string.Empty;

        [Display(Name = "Đơn giá")]
        [Column(TypeName = "money")]
        public decimal DonGia { get; set; }

        [Display(Name = "Ảnh sản phẩm")]
        public string? AnhSP { get; set; }

        [Display(Name = "Mô tả tóm tắt")]
        public string MoTaTT { get; set; } = string.Empty;

        [Display(Name = "Thông tin chi tiết")]
        public string? ThongTinCT { get; set; }

        [Display(Name = "Mã loại sản phẩm")]
        public short MaLSP { get; set; }

        [ForeignKey("MaLSP")]
        public virtual LoaiSanPham? LoaiSanPham { get; set; }

        [Display(Name = "Mã đơn vị tính")]
        public byte MaDVT { get; set; }

        [ForeignKey("MaDVT")]
        public virtual DonViTinh? DonViTinh { get; set; }
    }
}