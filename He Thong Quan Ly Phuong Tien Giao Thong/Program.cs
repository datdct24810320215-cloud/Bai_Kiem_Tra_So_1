using System;
using AutoSpeedManager;

namespace AutoSpeedManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            QuanLyPhuongTien ql = new QuanLyPhuongTien();

            // TC01: Kiểm tra Validation Năm sản xuất
            Console.WriteLine("--- TC01: Kiểm tra Validation Năm sản xuất ---");
            try
            {
                OTo otoLoi = new OTo("PT001", "Toyota", 1850, 1000000000m, 5, 2.0);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Đã bắt lỗi: " + ex.Message); // Kỳ vọng: Năm sản xuất không hợp lệ!
            }

            // TC02: Kiểm tra Tính Giá Lăn Bánh Ô tô
            Console.WriteLine("\n--- TC02: Tính giá lăn bánh Ô tô 5 chỗ ---");
            OTo oto5Cho = new OTo("PT002", "Honda", 2023, 1000000000m, 5, 2.0);
            Console.WriteLine($"Giá lăn bánh: {oto5Cho.TinhGiaLanBanh():N0} VNĐ"); // Kỳ vọng: 1,420,000,000

            // TC03: Kiểm tra Tính Giá Lăn Bánh Xe máy
            Console.WriteLine("\n--- TC03: Tính giá lăn bánh Xe máy 150cc ---");
            XeMay xeMay = new XeMay("PT003", "Yamaha", 2023, 50000000m, 150);
            Console.WriteLine($"Giá lăn bánh: {xeMay.TinhGiaLanBanh():N0} VNĐ"); // Kỳ vọng: 51,000,000

            // TC04: Kiểm tra Đa hình List<PhuongTien>
            Console.WriteLine("\n--- TC04: Kiểm tra Đa hình ---");
            ql.AddPhuongTien(oto5Cho);
            ql.AddPhuongTien(xeMay);
            ql.DisplayAll();

            // TC05: Kiểm tra Tìm Giá Lăn Bánh Max
            Console.WriteLine("\n--- TC05: Tìm giá lăn bánh Max ---");
            PhuongTien maxPT = ql.FindMaxGiaLanBanh();
            if (maxPT != null)
            {
                Console.WriteLine($"Phương tiện có giá lăn bánh cao nhất: {maxPT.TenHang} - {maxPT.TinhGiaLanBanh():N0} VNĐ");
            }

            Console.ReadLine();
        }
    }
}