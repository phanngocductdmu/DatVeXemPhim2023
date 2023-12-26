using DatVeXemPhim2023.Models;

namespace DatVeXemPhim2023.ViewModels
{
    public class ListClass
    {
        public List<TComBo> LcomBos {  get; set; }
        public TComBo comBo { get; set; }
        public List<TDanhGia> LdanhGia { get; set; }
        public TDanhGia danhGia { get; set; }
        public List<TDoAn> LdoAns { get; set; }
        public TDoAn doAn { get; set; }
        public List<TDoUong> LdoUongs { get; set; }
        public TDoUong doUong { get; set; }
        public List<TGhe> Lghes { get; set; }
        public TGhe Ghes { get ; set; }
        public List<THoaDon> LhoaDons { get; set; }
        public THoaDon hoaDon { get; set; }
        public List<TPhim> Lphims { get; set; }
        public TPhim phims { get; set; }
        public List<TPhongChieu> LphongChieus { get; set; }
        public TPhongChieu phongChieu { get; set; }
        public List<TQuaLuuNiem> LquaLuuNiem { get; set; }
        public TQuaLuuNiem quaLuuNiem { get; set; }
        public List<TRapChieuPhim> LRapPhim { get; set;}
        public TRapChieuPhim rapChieuPhim { get; set; }
        public List<TSuatChieu> LsuatChieus { get; set; }
        public TSuatChieu suatChieu { get; set; }
        public List<TTaiKhoan> LtaiKhoans { get; set; }
        public TTaiKhoan taiKhoan { get; set; }
        public List<TThanhToan> LthanhToans { get; set; }
        public TTaiKhoan ThanhToan { get; set; }
        public List<TThongTinKhuyenMai> LthongTinKhuyenMais { get; set; }
        public TThongTinKhuyenMai thongTinKhuyenMai { get; set; }
        public List<TVe> Lves { get; set; }
        public TVe ve { get; set; }
    }
}
