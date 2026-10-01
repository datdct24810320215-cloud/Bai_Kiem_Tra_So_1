using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoSpeedManager
{
    public class QuanLyPhuongTien
    {
        private List<PhuongTien> _danhSachPT = new List<PhuongTien>();

        public void AddPhuongTien(PhuongTien pt)
        {
            _danhSachPT.Add(pt);
        }

        public void DisplayAll()
        {
            Console.WriteLine("--- DANH SÁCH PHƯƠNG TIỆN ---");
            foreach (var pt in _danhSachPT)
            {
                Console.WriteLine(pt.GetInfo() + $" | Giá lăn bánh: {pt.TinhGiaLanBanh():N0} VNĐ");
            }
        }

        public PhuongTien FindMaxGiaLanBanh()
        {
            if (_danhSachPT.Count == 0) return null;
            // Sử dụng LINQ để tìm max
            return _danhSachPT.OrderByDescending(pt => pt.TinhGiaLanBanh()).FirstOrDefault();
        }

        public List<PhuongTien> SearchByName(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return new List<PhuongTien>();
            // Tìm kiếm không phân biệt hoa thường
            return _danhSachPT.Where(pt => pt.TenHang.ToLower().Contains(keyword.ToLower())).ToList();
        }
    }
}