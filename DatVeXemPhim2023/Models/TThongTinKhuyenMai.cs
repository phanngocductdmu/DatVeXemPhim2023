using System;
using System.Collections.Generic;

namespace DatVeXemPhim2023.Models;

public partial class TThongTinKhuyenMai
{
    public int IdkhuyenMai { get; set; }

    public int Iduser { get; set; }

    public string? TenKhuyenMai { get; set; }

    public string? NdkhuyenMai { get; set; }

    public DateTime? TimeBegin { get; set; }

    public DateTime? TimeEnd { get; set; }

    public virtual TTaiKhoan IduserNavigation { get; set; } = null!;
}
