using System.ComponentModel.DataAnnotations;

namespace WebQLBH.Models
{
    public class Tinh
    {
        [Key]
        [Display (Name = "Mã tỉnh")]
        public string? MaTinh { get; set; }

        [Display (Name = "Tên tỉnh")]
        public string TenTinh { get; set; }

    }
}
