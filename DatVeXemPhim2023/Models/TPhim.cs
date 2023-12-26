using System;
using System.Collections.Generic;

namespace DatVeXemPhim2023.Models;

public partial class TPhim
{
    public int Idphim { get; set; }

    public string? TenPhim { get; set; }

    public string? TheLoai { get; set; }

    public string? DaoDien { get; set; }

    public string? DienVien { get; set; }

    public string? HinhAnh { get; set; }

    public string? MoTa { get; set; }

    public DateTime? NgayCongChieu { get; set; }

    public string? ThoiLuong { get; set; }

    public double? DanhGia { get; set; }

    public virtual ICollection<TDanhGia> TDanhGia { get; set; } = new List<TDanhGia>();

    public virtual ICollection<TSuatChieu> TSuatChieus { get; set; } = new List<TSuatChieu>();
   
}
