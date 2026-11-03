using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebQLBH.Models
{
    public class LoaiSanPham
    {
        [Key]
        [Display(Name = "Mã loại sản phẩm")]
        public short MaLSP { get; set; }

        [Display(Name = "Tên loại sản phẩm")]
        public string TenLSP { get; set; } = string.Empty;

        [Display(Name = "Mã nhóm sản phẩm")]
        public byte MaNSP { get; set; }
        [ForeignKey("MaNSP")]
        public virtual NhomSanPham? NhomSanPham { get; set; }

    }
}
