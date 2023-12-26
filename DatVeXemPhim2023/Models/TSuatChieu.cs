using System;
using System.Collections.Generic;

namespace DatVeXemPhim2023.Models;

public partial class TSuatChieu
{
    public int IdsuatChieu { get; set; }

    public int Idphim { get; set; }

    public int IdrapChieuPhim { get; set; }

    public string? TenPhongChieu { get; set; }

    public DateTime? TgbatDau { get; set; }

    public DateTime? TgketThuc { get; set; }

    public int? GiaVe { get; set; }

    public string? GheTrong { get; set; }

    public virtual TPhim IdphimNavigation { get; set; } = null!;

    public virtual TRapChieuPhim IdrapChieuPhimNavigation { get; set; } = null!;

    public virtual ICollection<TVe> TVes { get; set; } = new List<TVe>();
}
