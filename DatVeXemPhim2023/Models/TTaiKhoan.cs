using System;
using System.Collections.Generic;

namespace DatVeXemPhim2023.Models;

public partial class TTaiKhoan
{
    public int Iduser { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? TypeUser { get; set; }

    public string? Sdt { get; set; }

    public string? DiaChi { get; set; }

    public string? HoTen { get; set; }

    public virtual ICollection<THoaDon> THoaDons { get; set; } = new List<THoaDon>();

    public virtual ICollection<TThongTinKhuyenMai> TThongTinKhuyenMais { get; set; } = new List<TThongTinKhuyenMai>();
}
