using System.ComponentModel.DataAnnotations;

namespace WebQLBH.Models
{
    public class PhongBan
    {
        [Key]
        [Display(Name = "Mã phòng ban")]
        public byte MaPB { get; set; }

        [Display(Name = "Tên phòng ban")]
        public string TenPB { get; set; }
    }
}
