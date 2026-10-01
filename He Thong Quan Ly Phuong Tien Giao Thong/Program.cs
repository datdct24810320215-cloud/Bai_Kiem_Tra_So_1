using System;
using System.Collections.Generic;
using AutoSpeedManager;

namespace AutoSpeedManager
{
    class Program
    {
        static void Main(string[] args)
        {
            // Đảm bảo hiển thị tiếng Việt không bị lỗi font
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            QuanLyPhuongTien ql = new QuanLyPhuongTien();
            int choice = 0;

            while (true)
            {
                Console.WriteLine("\n========= HỆ THỐNG QUẢN LÝ PHƯƠNG TIỆN AUTOSPEED =========");
                Console.WriteLine("1. Thêm Ô tô");
                Console.WriteLine("2. Thêm Xe máy");
                Console.WriteLine("3. Hiển thị danh sách phương tiện");
                Console.WriteLine("4. Tìm phương tiện có giá lăn bánh cao nhất");
                Console.WriteLine("5. Tìm kiếm theo tên hãng");
                Console.WriteLine("0. Thoát");
                Console.Write("Mời bạn chọn chức năng (0-5): ");

                // Đọc lựa chọn, nếu nhập sai thì mặc định là -1
                int.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:
                        NhapOTo(ql);
                        break;
                    case 2:
                        NhapXeMay(ql);
                        break;
                    case 3:
                        ql.DisplayAll();
                        break;
                    case 4:
                        TimMax(ql);
                        break;
                    case 5:
                        TimKiem(ql);
                        break;
                    case 0:
                        Console.WriteLine("Đã thoát chương trình. Tạm biệt!");
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại!");
                        break;
                }
            }
        }

        // --- HÀM NHẬP Ô TÔ ---
        static void NhapOTo(QuanLyPhuongTien ql)
        {
            Console.WriteLine("\n--- NHẬP THÔNG TIN Ô TÔ ---");
            try
            {
                Console.Write("Nhập Mã PT (bỏ trống sẽ lấy mặc định PT000): ");
                string ma = Console.ReadLine();

                Console.Write("Nhập Tên hãng: ");
                string ten = Console.ReadLine();

                Console.Write("Nhập Năm sản xuất: ");
                int nam = int.Parse(Console.ReadLine());

                Console.Write("Nhập Giá gốc (VNĐ): ");
                decimal gia = decimal.Parse(Console.ReadLine());

                Console.Write("Nhập Số chỗ ngồi: ");
                int soCho = int.Parse(Console.ReadLine());

                Console.Write("Nhập Dung tích động cơ (Lít): ");
                double dungTich = double.Parse(Console.ReadLine());

                // Khởi tạo đối tượng (Nếu lỗi Validation sẽ nhảy xuống catch)
                OTo oto = new OTo(ma, ten, nam, gia, soCho, dungTich);
                ql.AddPhuongTien(oto);
                Console.WriteLine("=> Thêm Ô tô thành công!");
            }
            catch (FormatException)
            {
                Console.WriteLine("=> LỖI: Bạn nhập sai định dạng số. Vui lòng nhập lại!");
            }
            catch (ArgumentException ex)
            {
                // Bắt lỗi Validation từ Properties (Năm SX, Tên hãng, Giá gốc...)
                Console.WriteLine("=> LỖI VALIDATION: " + ex.Message);
            }
        }

        // --- HÀM NHẬP XE MÁY ---
        static void NhapXeMay(QuanLyPhuongTien ql)
        {
            Console.WriteLine("\n--- NHẬP THÔNG TIN XE MÁY ---");
            try
            {
                Console.Write("Nhập Mã PT (bỏ trống sẽ lấy mặc định PT000): ");
                string ma = Console.ReadLine();

                Console.Write("Nhập Tên hãng: ");
                string ten = Console.ReadLine();

                Console.Write("Nhập Năm sản xuất: ");
                int nam = int.Parse(Console.ReadLine());

                Console.Write("Nhập Giá gốc (VNĐ): ");
                decimal gia = decimal.Parse(Console.ReadLine());

                Console.Write("Nhập Dung tích xy-lanh (cc): ");
                int dungTich = int.Parse(Console.ReadLine());

                XeMay xe = new XeMay(ma, ten, nam, gia, dungTich);
                ql.AddPhuongTien(xe);
                Console.WriteLine("=> Thêm Xe máy thành công!");
            }
            catch (FormatException)
            {
                Console.WriteLine("=> LỖI: Bạn nhập sai định dạng số. Vui lòng nhập lại!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("=> LỖI VALIDATION: " + ex.Message);
            }
        }

        // --- HÀM TÌM MAX ---
        static void TimMax(QuanLyPhuongTien ql)
        {
            PhuongTien maxPT = ql.FindMaxGiaLanBanh();
            if (maxPT == null)
            {
                Console.WriteLine("Danh sách trống, không có gì để tìm!");
            }
            else
            {
                Console.WriteLine("\n--- PHƯƠNG TIỆN CÓ GIÁ LĂN BÁNH CAO NHẤT ---");
                Console.WriteLine(maxPT.GetInfo());
                Console.WriteLine($"Giá lăn bánh: {maxPT.TinhGiaLanBanh():N0} VNĐ");
            }
        }

        // --- HÀM TÌM KIẾM ---
        static void TimKiem(QuanLyPhuongTien ql)
        {
            Console.Write("Nhập tên hãng cần tìm: ");
            string keyword = Console.ReadLine();
            var ketQua = ql.SearchByName(keyword);

            if (ketQua.Count == 0)
            {
                Console.WriteLine("Không tìm thấy phương tiện nào phù hợp.");
            }
            else
            {
                Console.WriteLine($"\n--- TÌM THẤY {ketQua.Count} PHƯƠNG TIỆN ---");
                foreach (var pt in ketQua)
                {
                    Console.WriteLine(pt.GetInfo() + $" | Giá lăn bánh: {pt.TinhGiaLanBanh():N0} VNĐ");
                }
            }
        }
    }
}