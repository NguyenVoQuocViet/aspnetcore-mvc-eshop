using System.ComponentModel.DataAnnotations;

namespace WebQLBH.Models
{
    public class NhomSanPham
    {
        [Key]
        [Display(Name = "Mã nhóm sản phẩm")]
        public byte MaNSP { get; set; }

        [Display(Name = "Tên nhóm sản phẩm")]
        [Required(ErrorMessage = "Vui lòng nhập tên nhóm sản phẩm")]
        public string TenNSP { get; set; } = string.Empty;
    }
}
