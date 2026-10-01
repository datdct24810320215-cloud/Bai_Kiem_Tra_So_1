using System;

namespace AutoSpeedManager
{
    public class OTo : PhuongTien
    {
        public int SoChoNgoi { get; set; }
        public double DungTichDongCo { get; set; }

        public OTo(string maPT, string tenHang, int namSanXuat, decimal giaGoc, int soChoNgoi, double dungTichDongCo)
            : base(maPT, tenHang, namSanXuat, giaGoc)
        {
            if (soChoNgoi <= 0) throw new ArgumentException("Số chỗ ngồi phải > 0");
            if (dungTichDongCo <= 0) throw new ArgumentException("Dung tích động cơ phải > 0");

            SoChoNgoi = soChoNgoi;
            DungTichDongCo = dungTichDongCo;
        }

        public override decimal TinhGiaLanBanh()
        {
            if (SoChoNgoi <= 9)
            {
                // 12% Trước bạ + 30% Tiêu thụ đặc biệt
                return GiaGoc + (0.12m * GiaGoc) + (0.30m * GiaGoc);
            }
            else
            {
                // 10% Trước bạ
                return GiaGoc + (0.10m * GiaGoc);
            }
        }

        public override string GetInfo()
        {
            return base.GetInfo() + $" | Số chỗ: {SoChoNgoi} | Dung tích: {DungTichDongCo}L";
        }
    }
}