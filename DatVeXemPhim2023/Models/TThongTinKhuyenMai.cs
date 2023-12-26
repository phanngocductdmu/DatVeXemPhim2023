using System;
using System.Collections.Generic;

namespace DatVeXemPhim2023.Models;

public partial class TThongTinKhuyenMai
{
    public int IdkhuyenMai { get; set; }

    public int IdrapChieuPhim { get; set; }

    public string? TenUuDaiVaKhuyenMai { get; set; }

    public string? NdkhuyenMai1 { get; set; }

    public string? NdkhuyenMai2 { get; set; }

    public string? NdkhuyenMai3 { get; set; }

    public string? HinhAnh { get; set; }

    public DateTime? TimeBegin { get; set; }

    public DateTime? TimeEnd { get; set; }

    public virtual TRapChieuPhim IdrapChieuPhimNavigation { get; set; } = null!;
}
