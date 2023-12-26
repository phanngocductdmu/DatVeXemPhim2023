using System;
using System.Collections.Generic;

namespace DatVeXemPhim2023.Models;

public partial class TPhongChieu
{
    public string TenPhongChieu { get; set; } = null!;

    public int? IdrapChieuPhim { get; set; }

    public virtual TRapChieuPhim? IdrapChieuPhimNavigation { get; set; }
}
