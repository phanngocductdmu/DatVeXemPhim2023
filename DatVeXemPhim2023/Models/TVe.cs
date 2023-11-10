using System;
using System.Collections.Generic;

namespace DatVeXemPhim2023.Models;

public partial class TVe
{
    public int Idve { get; set; }

    public int IdthanhToan { get; set; }

    public int IdsuatChieu { get; set; }

    public int? Idghe { get; set; }

    public DateTime? TgdatVe { get; set; }

    public string? TrangThai { get; set; }

    public virtual TSuatChieu IdsuatChieuNavigation { get; set; } = null!;

    public virtual ICollection<TThanhToan> TThanhToans { get; set; } = new List<TThanhToan>();
}
