using System;
using System.Collections.Generic;

namespace DatVeXemPhim2023.Models;

public partial class THoaDon
{
    public int IdhoaDon { get; set; }

    public int IddoVat { get; set; }

    public int? Iduser { get; set; }

    public int Idve { get; set; }

    public DateTime? NgayLap { get; set; }

    public int? TongTien { get; set; }

    public string? TrangThai { get; set; }

    public virtual TTaiKhoan? IduserNavigation { get; set; }

    public virtual ICollection<TThanhToan> TThanhToans { get; set; } = new List<TThanhToan>();
}
