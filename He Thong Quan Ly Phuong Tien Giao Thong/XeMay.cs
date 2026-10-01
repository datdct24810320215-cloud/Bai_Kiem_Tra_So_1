using System;

namespace AutoSpeedManager
{
    public class XeMay : PhuongTien
    {
        public int DungTichXylanh { get; set; } // cc

        public XeMay(string maPT, string tenHang, int namSanXuat, decimal giaGoc, int dungTichXylanh)
            : base(maPT, tenHang, namSanXuat, giaGoc)
        {
            if (dungTichXylanh <= 0) throw new ArgumentException("Dung tích xy-lanh phải > 0");
            DungTichXylanh = dungTichXylanh;
        }

        public override decimal TinhGiaLanBanh()
        {
            if (DungTichXylanh < 175)
            {
                // 2% Trước bạ
                return GiaGoc + (0.02m * GiaGoc);
            }
            else
            {
                // 5% Trước bạ
                return GiaGoc + (0.05m * GiaGoc);
            }
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $" | Dung tích xy-lanh: {DungTichXylanh} cc";
        }
    }
}