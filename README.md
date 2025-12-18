# 📂 FileShareApp - Hệ Thống Chia Sẻ Tập Tin

![Build Status](https://img.shields.io/badge/Build-Passing-brightgreen)
![Platform](https://img.shields.io/badge/Platform-ASP.NET_Core_8.0-purple)
![Database](https://img.shields.io/badge/Database-MySQL-orange)
![License](https://img.shields.io/badge/License-MIT-blue)

**FileShareApp** là một ứng dụng web lưu trữ và chia sẻ tập tin an toàn, được xây dựng trên nền tảng **ASP.NET Core MVC**. Dự án tập trung giải quyết bài toán tải lên các tập tin kích thước lớn (**Large File Upload > 10GB**) mà không gây quá tải bộ nhớ RAM máy chủ bằng kỹ thuật **Streaming**.

---

## 🚀 Tính Năng Nổi Bật

### 1. ⚡ Streaming Upload (Tính năng cốt lõi)
Khác với cơ chế `IFormFile` truyền thống (lưu file vào RAM/Temp trước), FileShareApp sử dụng cơ chế **Multipart Reader** để đọc luồng dữ liệu (Stream) trực tiếp từ Request và ghi thẳng xuống ổ cứng.
- **Ưu điểm:** Upload file với ung lượng lớn nhưng không làm tốn nhiều cacche.
- Hỗ trợ thanh tiến trình (Progress Bar) thực tế.
- Tự động kiểm tra định dạng file (Magic numbers/Extensions) để bảo mật.

### 2. 🔐 Quản Lý & Bảo Mật
- **Hệ thống xác thực:** Đăng ký/Đăng nhập an toàn với Password Hashing (SHA256) và Cookie Authentication.
- **Chia sẻ an toàn:** Tạo đường dẫn công khai duy nhất (Unique Token) dạng `domain.com/d/{token}`.
- **Download bảo mật:** Giấu đường dẫn vật lý thực tế của file, trả về file qua luồng (PhysicalFileResult).

### 3. 🎨 Giao Diện Người Dùng (UI/UX)
- Giao diện hiện đại với **Bootstrap 5**.
- **Dashboard cá nhân:** Quản lý danh sách file, dung lượng, ngày tải.
- **Trải nghiệm chia sẻ:** Tích hợp Modal lấy link nhanh và nút "Copy to Clipboard".
- **Trang báo lỗi tùy chỉnh:** Giao diện 404 thân thiện khi link hỏng hoặc file bị xóa.

---

## 🛠 Công Nghệ Sử Dụng

| Thành phần | Công nghệ |
| :--- | :--- |
| **Backend** | ASP.NET Core MVC (.NET 8) |
| **Database** | MySQL (InnoDB Engine) |
| **ORM** | Entity Framework Core (Code First/DB First) |
| **Frontend** | Razor Views, Bootstrap 5, Vanilla JS (AJAX) |
| **Server** | Kestrel (Custom Configuration) |

---

## ⚙️ Cài Đặt & Chạy Dự Án

### Yêu cầu
- .NET SDK 6.0 hoặc 8.0 trở lên.
- MySQL Server (XAMPP/WAMP hoặc Docker).
- Visual Studio 2022 hoặc VS Code.

### Bước 1: Clone dự án
Mở terminal/cmd và chạy lệnh:
```bash
git clone https://github.com/Kyle-854/TH_MaNguonMo.git
cd FileShareApp
```

### Bước 2: Cấu hình Kết nối Database
Đổi tên file `appsettings.Example.json` thành `appsettings.json` trong thư mục gốc của dự án. Cập nhật thông tin `ConnectionStrings` để trỏ tới MySQL Server của bạn.

Thay thế các giá trị `YOUR_SERVER`, `YOUR_USER`, `YOUR_PASSWORD` bằng thông tin thật:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "server=YOUR_SERVER;database=FileShareDB;user=YOUR_USER;password=YOUR_PASSWORD"
  },
  "AllowedHosts": "*"
}
```

### Bước 3: Khởi tạo Database (Migrations)
Dự án sử dụng Entity Framework Core Migrations. Bạn không cần chạy script SQL thủ công. Hãy mở Terminal tại thư mục dự án và chạy lệnh sau để tự động tạo Database và các Bảng:

```bash
# (Tùy chọn) Cài đặt công cụ EF Core nếu máy bạn chưa có
dotnet tool install --global dotnet-ef --version 8.0.0

# Cập nhật Database dựa trên Migrations có sẵn
dotnet ef database update
```

### Bước 4: Chạy ứng dụng
Sau khi Database đã được tạo thành công, dùng Visual Studio mở và chạy project hoặc có thể chạy các lệnh sau để khởi động web:

```bash
# 1. Khôi phục các thư viện cần thiết
dotnet restore

# 2. Chạy ứng dụng
dotnet run
```
