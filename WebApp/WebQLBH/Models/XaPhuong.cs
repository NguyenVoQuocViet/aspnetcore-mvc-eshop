using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebQLBH.Models
{
    public class XaPhuong
    {
        [Key]
        [Display(Name = "Mã xã phường")]
        public string? MaXP { get; set; }

        [Display(Name = "Tên xã phường")]
        public string TenXP { get; set; }

        [Display(Name = "Mã tỉnh")]
        public string MaTinh { get; set; }
        [ForeignKey("MaTinh")]
        public virtual Tinh? Tinh { get; set; }
    }
}
