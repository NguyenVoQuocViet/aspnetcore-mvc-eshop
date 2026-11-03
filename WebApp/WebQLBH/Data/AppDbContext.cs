using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebQLBH.Models;

namespace WebQLBH.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

        // 1. Lập trình thao tác trên bảng DonViTinh
        public DbSet<DonViTinh> DonViTinhs { get; set; }
        public List<DonViTinh> DonViTinh_DS()
        {
            return DonViTinhs.FromSqlRaw("EXEC DonViTinh_DS").ToList();
        }

        public DonViTinh? DonViTinh_CT(byte MaDVT)
        {
            var p = new[]
            {
                new Microsoft.Data.SqlClient.SqlParameter("@MaDVT", MaDVT)
            };
            return DonViTinhs.FromSqlRaw("EXEC DonViTinh_CT @MaDVT", p).ToList().FirstOrDefault();
        }

        public int DonViTinh_Them(DonViTinh dvt)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "DonViTinh_Them";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@TenDVT", dvt.TenDVT));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }
        public int DonViTinh_Sua(DonViTinh dvt)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "DonViTinh_Sua";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaDVT", dvt.MaDVT));
            cmd.Parameters.Add(new SqlParameter("@TenDVT", dvt.TenDVT));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;

        }
        public int DonViTinh_Xoa(byte? MaDVT)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "DonViTinh_Xoa";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaDVT", (object?)MaDVT ?? DBNull.Value));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }

        // 2.Lập trình thao tác trên bảng NhomSanPham
        public DbSet<NhomSanPham> NhomSanPhams { get; set; }
        public List<NhomSanPham> NhomSanPham_DS()
        {
            return NhomSanPhams.FromSqlRaw("EXEC NhomSanPham_DS").ToList();
        }

        public NhomSanPham? NhomSanPham_CT(byte MaNSP)
        {
            var p = new[]
            {
                new Microsoft.Data.SqlClient.SqlParameter("@MaNSP", MaNSP)
            };
            return NhomSanPhams.FromSqlRaw("EXEC NhomSanPham_CT @MaNSP", p).ToList().FirstOrDefault();
        }

        public int NhomSanPham_Them(NhomSanPham nsp)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "NhomSanPham_Them";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("TenNSP", nsp.TenNSP));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }

        public int NhomSanPham_Sua(NhomSanPham nsp)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "NhomSanPham_Sua";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaNSP", nsp.MaNSP));
            cmd.Parameters.Add(new SqlParameter("@TenNSP", nsp.TenNSP));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }

        public int NhomSanPham_Xoa(byte? MaNSP)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "NhomSanPham_Xoa";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaNSP", (object?)MaNSP ?? DBNull.Value));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }

        // 3. Lập trình thao tác trên bảng LoaiSanPham
        public DbSet<LoaiSanPham> LoaiSanPhams { get; set; }
        public List<LoaiSanPham> LoaiSanPham_DS()
        {
            var dsLoaiSP = LoaiSanPhams.FromSqlRaw("EXEC LoaiSanPham_DS").ToList();

            foreach (var item in dsLoaiSP)
            {
                item.NhomSanPham = NhomSanPham_CT(item.MaNSP);
            }
            return dsLoaiSP;
        }

        public LoaiSanPham? LoaiSanPham_CT(short MaLSP)
        {
            var p = new[]
            {
                new Microsoft.Data.SqlClient.SqlParameter("@MaLSP", MaLSP)
            };
            var loaiSP = LoaiSanPhams.FromSqlRaw("EXEC LoaiSanPham_CT @MaLSP", p).ToList().FirstOrDefault();
            if (loaiSP == null) return null;
            loaiSP.NhomSanPham = NhomSanPham_CT(loaiSP.MaNSP);
            return loaiSP;
        }

        public int LoaiSanPham_Them(LoaiSanPham lsp)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "LoaiSanPham_Them";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@TenLSP", lsp.TenLSP));
            cmd.Parameters.Add(new SqlParameter("@MaNSP", lsp.MaNSP));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }

        public int LoaiSanPham_Sua(LoaiSanPham lsp)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "LoaiSanPham_Sua";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaLSP", lsp.MaLSP));
            cmd.Parameters.Add(new SqlParameter("@TenLSP", lsp.TenLSP));
            cmd.Parameters.Add(new SqlParameter("@MaNSP", lsp.MaNSP));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }

        public int LoaiSanPham_Xoa(LoaiSanPham lsp)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "LoaiSanPham_Xoa";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaLSP", (object)lsp.MaLSP ?? DBNull.Value));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }

        // 4. Lập trình thao tác trên bảng SanPham
        public DbSet<SanPham> SanPhams { get; set; }

        public List<SanPham> SanPham_DS()
        {
            var dsSanPham = SanPhams.FromSqlRaw("EXEC SanPham_DS").ToList();

            foreach (var item in dsSanPham)
            {
                item.LoaiSanPham = LoaiSanPham_CT(item.MaLSP);
                item.DonViTinh = DonViTinh_CT(item.MaDVT);
            }

            return dsSanPham;
        }

        public SanPham? SanPham_CT(string MaSP)
        {
            var p = new[]
            {
                new Microsoft.Data.SqlClient.SqlParameter("@MaSP", MaSP)
            };
            var sanPham = SanPhams.FromSqlRaw("EXEC SanPham_CT @MaSP", p).ToList().FirstOrDefault();
            if (sanPham == null) return null;
            sanPham.LoaiSanPham = LoaiSanPham_CT(sanPham.MaLSP);
            sanPham.DonViTinh = DonViTinh_CT(sanPham.MaDVT);
            return sanPham;
        }

        public int SanPham_Them(SanPham sp)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "SanPham_Them";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@TenSP", sp.TenSP));
            cmd.Parameters.Add(new SqlParameter("@DonGia", sp.DonGia));
            cmd.Parameters.Add(new SqlParameter("@AnhSP", (object?)sp.AnhSP ?? DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@MoTaTT", sp.MoTaTT));
            cmd.Parameters.Add(new SqlParameter("@ThongTinCT", (object?)sp.ThongTinCT ?? DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@MaLSP", sp.MaLSP));
            cmd.Parameters.Add(new SqlParameter("@MaDVT", sp.MaDVT));

            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }

        public int SanPham_Sua(SanPham sp)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "SanPham_Sua";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@MaSP", sp.MaSP));
            cmd.Parameters.Add(new SqlParameter("@TenSP", sp.TenSP));
            cmd.Parameters.Add(new SqlParameter("@DonGia", sp.DonGia));
            cmd.Parameters.Add(new SqlParameter("@AnhSP", (object?)sp.AnhSP ?? DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@MoTaTT", sp.MoTaTT));
            cmd.Parameters.Add(new SqlParameter("@ThongTinCT", (object?)sp.ThongTinCT ?? DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@MaLSP", sp.MaLSP));
            cmd.Parameters.Add(new SqlParameter("@MaDVT", sp.MaDVT));

            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }

        public int SanPham_Xoa(string? MaSP)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "SanPham_Xoa";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@MaSP", (object?)MaSP ?? DBNull.Value));

            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }

        // 5. Lập trình thao tác trên bảng Tinh
        public DbSet<Tinh> Tinhs { get; set; }
        public List<Tinh> Tinh_DS()
        {
            return Tinhs.FromSqlRaw("EXEC Tinh_DS").ToList();
        }

        public Tinh? Tinh_CT(string MaTinh)
        {
            var p = new[]
            {
                new Microsoft.Data.SqlClient.SqlParameter("@MaTinh", MaTinh)
            };
            return Tinhs.FromSqlRaw("EXEC Tinh_CT @MaTinh", p).ToList().FirstOrDefault();
        }

        public int Tinh_Them(Tinh t)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "Tinh_Them";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaTinh", t.MaTinh));
            cmd.Parameters.Add(new SqlParameter("@TenTinh", t.TenTinh));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }
        public int Tinh_Sua(Tinh t)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "Tinh_Sua";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaTinh", t.MaTinh));
            cmd.Parameters.Add(new SqlParameter("@TenTinh", t.TenTinh));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }
        public int Tinh_Xoa(string matinh)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "Tinh_Xoa";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaTinh", matinh));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }

        // 6. Lập trình thao tác trên bảng XaPhuong
        public DbSet<XaPhuong> XaPhuongs { get; set; }

        public List<XaPhuong> XaPhuong_DS()
        {
            var dsXP = XaPhuongs.FromSqlRaw("EXEC XaPhuong_DS").ToList();

            foreach (var item in dsXP)
            {
                item.Tinh = Tinh_CT(item.MaTinh);
            }

            return dsXP;
        }

        public XaPhuong? XaPhuong_CT(string MaXP)
        {
            var p = new[]
            {
                new Microsoft.Data.SqlClient.SqlParameter("@MaXP", MaXP)
            };
            var xp = XaPhuongs.FromSqlRaw("EXEC XaPhuong_CT @MaXP", p).ToList().FirstOrDefault();
            if (xp == null) return null;
            xp.Tinh = Tinh_CT(xp.MaTinh);
            return xp;
        }

        public int XaPhuong_Them(XaPhuong xp)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "XaPhuong_Them";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaXP", xp.MaXP));
            cmd.Parameters.Add(new SqlParameter("@TenXP", xp.TenXP));
            cmd.Parameters.Add(new SqlParameter("@MaTinh", xp.MaTinh));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }

        public int XaPhuong_Sua(XaPhuong xp)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "XaPhuong_Sua";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaXP", xp.MaXP));
            cmd.Parameters.Add(new SqlParameter("@TenXP", xp.TenXP));
            cmd.Parameters.Add(new SqlParameter("@MaTinh", xp.MaTinh));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }

        public int XaPhuong_Xoa(string? MaXP)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "XaPhuong_Xoa";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaXP", (object?)MaXP ?? DBNull.Value));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }

        // 7. Lập trình thao tác trên bảng PhongBan
        public DbSet<PhongBan> PhongBans { get; set; }

        public List<PhongBan> PhongBan_DS()
        {
            return PhongBans.FromSqlRaw("EXEC PhongBan_DS").ToList();
        }

        public PhongBan? PhongBan_CT(byte MaPB)
        {
            var p = new[]
            {
                new Microsoft.Data.SqlClient.SqlParameter("@MaPB", MaPB)
            };
            return PhongBans.FromSqlRaw("EXEC PhongBan_CT @MaPB", p).ToList().FirstOrDefault();
        }

        public int PhongBan_Them(PhongBan pb)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "PhongBan_Them";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@TenPB", pb.TenPB));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }

        public int PhongBan_Sua(PhongBan pb)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "PhongBan_Sua";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaPB", pb.MaPB));
            cmd.Parameters.Add(new SqlParameter("@TenPB", pb.TenPB));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }

        public int PhongBan_Xoa(byte MaPB)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "PhongBan_Xoa";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaPB", (object?)MaPB ?? DBNull.Value));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }

        // 8. Lập trình thao tác trên bảng NhanVien
        public DbSet<NhanVien> NhanViens { get; set; }
        public List<NhanVien> NhanVien_DS()
        {
            var dsNhanVien = NhanViens.FromSqlRaw("EXEC NhanVien_DS").ToList();

            foreach (var item in dsNhanVien)
            {
                item.PhongBan = PhongBan_CT(item.MaPB);
            }
            return dsNhanVien;
        }
        public NhanVien? NhanVien_CT(byte MaNV)
        {
            var p = new[]
            {
                new Microsoft.Data.SqlClient.SqlParameter("@MaNV", MaNV)
            };
            var nhanVien = NhanViens.FromSqlRaw("EXEC NhanVien_CT @MaNV", p).ToList().FirstOrDefault();
            if (nhanVien == null) return null;
            nhanVien.PhongBan = PhongBan_CT(nhanVien.MaPB);
            return nhanVien;
        }
        public int NhanVien_Them(NhanVien nv)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "NhanVien_Them";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@HoNV", nv.HoNV));
            cmd.Parameters.Add(new SqlParameter("@TenNV", nv.TenNV));
            cmd.Parameters.Add(new SqlParameter("@DienThoai", nv.DienThoai));
            cmd.Parameters.Add(new SqlParameter("@Email", nv.Email));
            cmd.Parameters.Add(new SqlParameter("@TenDN", nv.TenDN));
            cmd.Parameters.Add(new SqlParameter("@AnhNV", nv.AnhNV ?? (object)DBNull.Value)); 
            cmd.Parameters.Add(new SqlParameter("@MaPB", nv.MaPB));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }
        public int NhanVien_Sua(NhanVien nv)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "NhanVien_Sua";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaNV", nv.MaNV));
            cmd.Parameters.Add(new SqlParameter("@HoNV", nv.HoNV));
            cmd.Parameters.Add(new SqlParameter("@TenNV", nv.TenNV));
            cmd.Parameters.Add(new SqlParameter("@DienThoai", nv.DienThoai));
            cmd.Parameters.Add(new SqlParameter("@Email", nv.Email));
            cmd.Parameters.Add(new SqlParameter("@TenDN", nv.TenDN));
            cmd.Parameters.Add(new SqlParameter("@AnhNV", nv.AnhNV ?? (object)DBNull.Value));
            cmd.Parameters.Add(new SqlParameter("@MaPB", nv.MaPB));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }
        public int NhanVien_Xoa(byte MaNV)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "NhanVien_Xoa";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@MaNV", MaNV));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }
        public byte NhanVien_DangNhap(string TenDN, string MatKhau) //Trả về Mã nhân viên nếu đăng nhập thành công
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "NhanVien_DangNhap";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@TenDN", TenDN));
            cmd.Parameters.Add(new SqlParameter("@MatKhau", MatKhau));
            Database.OpenConnection();
            var reader = cmd.ExecuteReader();
            byte maNV = 0;
            if (reader.Read())//Nếu có dữ liệu để đọc
            {
                maNV = byte.Parse(reader["MaNV"].ToString());
            }
            Database.CloseConnection();
            return maNV;
        }
        public int NhanVien_DoiMatKhau(string TenDN, string MatKhauCu, string MatKhauMoi)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "NhanVien_DoiMatKhau";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@TenDN", TenDN));
            cmd.Parameters.Add(new SqlParameter("@MatKhauCu", MatKhauCu));
            cmd.Parameters.Add(new SqlParameter("@MatKhauMoi", MatKhauMoi));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }
        public int NhanVien_ResetMatKhau(string TenDN)
        {
            var cmd = Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "NhanVien_ResetMatKhau";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@TenDN", TenDN));
            Database.OpenConnection();
            var kq = cmd.ExecuteNonQuery();
            Database.CloseConnection();
            return kq;
        }
    }
}
