using System;
using System.Collections.Generic;

namespace DatVeXemPhim2023.Models;

public partial class TThanhToan
{
    public int IdthanhToan { get; set; }

    public int IdhoaDon { get; set; }

    public string? Pttt { get; set; }

    public string? NgayThanhToan { get; set; }

    public virtual THoaDon IdhoaDonNavigation { get; set; } = null!;

    public virtual TVe IdthanhToanNavigation { get; set; } = null!;
}
