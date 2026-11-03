# WebQLBH – Ứng dụng web quản lý bán hàng

WebQLBH là ứng dụng web quản lý bán hàng xây dựng theo mô hình **ASP.NET Core MVC**, kết nối **SQL Server**. Hiện tại dự án tập trung vào phần **quản lý danh mục** và **đăng nhập nhân viên**.

## Công nghệ sử dụng

| Thành phần | Công nghệ |
|---|---|
| Nền tảng | .NET 10 (ASP.NET Core MVC) |
| Ngôn ngữ | C# |
| Truy cập dữ liệu | Entity Framework Core 10 + SQL Server (gọi thông qua stored procedure) |
| Giao diện | Razor Views, Bootstrap, jQuery (+ jQuery Validation) |
| Xác thực | Session (hết hạn sau 8 giờ không hoạt động) |

## Chức năng hiện có

- **Tài khoản:** đăng nhập, đăng xuất, đổi mật khẩu, đặt lại mật khẩu về mặc định.
- **Quản lý nhân viên** (kèm ảnh đại diện) và **phòng ban**.
- **Quản lý sản phẩm** (kèm ảnh sản phẩm), **loại sản phẩm**, **nhóm sản phẩm**, **đơn vị tính**.
- **Danh mục địa giới:** **tỉnh** và **xã/phường**.
- Mỗi danh mục đều có các thao tác cơ bản: xem danh sách, xem chi tiết, thêm, sửa, xóa.

## Cấu trúc thư mục

```
WebApp/
└── WebQLBH/
    ├── Controllers/     # Xử lý request cho từng chức năng (Account, SanPham, NhanVien, ...)
    ├── Models/          # Các lớp thực thể (SanPham, NhanVien, PhongBan, Tinh, ...)
    ├── Data/            # AppDbContext – nơi gọi các stored procedure
    ├── Filters/         # SessionAuthorizeAttribute – kiểm tra đăng nhập qua Session
    ├── Views/           # Giao diện Razor theo từng controller
    ├── wwwroot/         # CSS, JS, thư viện front-end, ảnh nhân viên / sản phẩm
    ├── script_database.sql   # Script tạo cơ sở dữ liệu
    ├── appsettings.json      # Cấu hình (chuỗi kết nối, logging)
    └── Program.cs            # Cấu hình dịch vụ và pipeline của ứng dụng
```

## Yêu cầu môi trường

- [.NET SDK 10](https://dotnet.microsoft.com/download)
- SQL Server (bản Express/LocalDB/Developer đều được)
- Visual Studio 2022 trở lên (khuyến nghị) hoặc `dotnet` CLI

## Hướng dẫn chạy

1. **Tạo cơ sở dữ liệu:** mở SQL Server Management Studio và chạy file `WebQLBH/script_database.sql`.
2. **Cấu hình chuỗi kết nối:** mở `WebQLBH/appsettings.json`, sửa `ConnectionStrings:DbQLBHConnection` cho đúng tên server và tên database của máy bạn, ví dụ:
   ```json
   "DbQLBHConnection": "Server=TEN_SERVER; Database=TEN_DATABASE; Trusted_Connection=True; TrustServerCertificate=True;"
   ```
3. **Chạy ứng dụng:**
   ```bash
   cd WebQLBH
   dotnet run
   ```
   Hoặc mở `WebQLBH.sln` bằng Visual Studio rồi nhấn **F5**.
4. Truy cập `http://localhost:5094` (hoặc `https://localhost:7147`).

## Kiến trúc & lưu ý

- Dữ liệu được truy xuất qua **stored procedure** (quy ước đặt tên `<Bảng>_DS`, `_CT`, `_Them`, `_Sua`, `_Xoa`), được bọc trong `AppDbContext`.
- Trang mặc định khi mở ứng dụng là danh sách **Nhân viên**.
- Mật khẩu nhân viên được xử lý ở phía cơ sở dữ liệu (đăng nhập, đổi và reset mật khẩu đều gọi stored procedure).

## Hướng phát triển

- Áp dụng `SessionAuthorize` cho các controller để yêu cầu đăng nhập trước khi truy cập.
- Bổ sung các nghiệp vụ bán hàng (khách hàng, đơn hàng, hóa đơn, ...).
- Phân quyền theo phòng ban / vai trò.