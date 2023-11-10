using System;
using System.Collections.Generic;

namespace DatVeXemPhim2023.Models;

public partial class TGhe
{
    public int Idghe { get; set; }

    public int? IdphongChieuPhim { get; set; }

    public int? TenGhe { get; set; }

    public bool? TrangThai { get; set; }

    public virtual TPhongChieu? IdphongChieuPhimNavigation { get; set; }
}
