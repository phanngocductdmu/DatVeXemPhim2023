using Microsoft.AspNetCore.Mvc;

namespace DatVeXemPhim2023.ViewModels
{
    public class HoaDonViewModel
    {
        public string? IdHoaDon {  get; set; }
        public int? IdPhim { get; set; }
       
        public int? IdGhe {  get; set; }
        public string? TenGhe { get; set; }
        public float? GiaGhe { get; set; }


        public int? IdFood { get; set; }
        public string? TenFood { get; set; }
        public float? GiaFood { get; set; }

        public int? IdUser { get; set; }
        public string? EmailUser { get; set; }
        public string? PhoneUser { get; set; }

        public DateTime? NgayDatGhe { get; set; }

        public int? IdRapPhim { get; set; }
        public int? IdSuatChieu { get; set; }



    }
}
