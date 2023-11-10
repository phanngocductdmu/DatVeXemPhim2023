using System;
using System.Collections.Generic;

namespace DatVeXemPhim2023.Models;

public partial class TPhongChieu
{
    public int IdphongChieu { get; set; }

    public int? IdrapChieuPhim { get; set; }

    public string? TenPhongChieu { get; set; }

    public virtual TRapChieuPhim? IdrapChieuPhimNavigation { get; set; }

    public virtual ICollection<TGhe> TGhes { get; set; } = new List<TGhe>();
}
