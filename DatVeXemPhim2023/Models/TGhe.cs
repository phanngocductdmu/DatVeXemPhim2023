using System;
using System.Collections.Generic;

namespace DatVeXemPhim2023.Models;

public partial class TGhe
{
    public int Idghe { get; set; }

    public int? IdrapChieuPhim { get; set; }

    public string? TenPhongChieu { get; set; }

    public string? HangGhe { get; set; }

    public string? TenGhe { get; set; }

    public bool? TrangThai { get; set; }

    public int? GiaGhe { get; set; }

    public virtual TRapChieuPhim? IdrapChieuPhimNavigation { get; set; }
}
