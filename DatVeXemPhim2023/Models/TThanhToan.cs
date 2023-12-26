using System;
using System.Collections.Generic;

namespace DatVeXemPhim2023.Models;

public partial class TThanhToan
{
    public int IdthanhToan { get; set; }

    public string? IdhoaDon { get; set; }

    public int? IdUser { get; set; }

    public string? Pttt { get; set; }

    public DateTime? NgayThanhToan { get; set; }

    public string? EmailKhachHang { get; set; }

    public string? PhoneKhachHang { get; set; }

    public DateTime? NgayDatGhe { get; set; }

    /*public int? Gia {  get; set; }*/
}
