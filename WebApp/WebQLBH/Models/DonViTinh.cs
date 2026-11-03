using System.ComponentModel.DataAnnotations;

namespace WebQLBH.Models
{
    public class DonViTinh
    {
        [Key]
        [Display(Name = "Mã đơn vị tính")]
        public byte MaDVT { get; set; }

        [Display(Name = "Tên đơn vị tính")]
        [Required(ErrorMessage = "Vui lòng nhập tên đơn vị tính")]
        public string? TenDVT { get; set; }
    }
}
