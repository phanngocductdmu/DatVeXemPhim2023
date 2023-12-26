using System;
using System.Collections.Generic;

namespace DatVeXemPhim2023.Models;

public partial class TDoAn
{
    public int IddoAn { get; set; }

    public int? IdRapChieuPhim { get; set; }

    public string? Ten { get; set; }

    public int? Gia { get; set; }

    public string? HinhAnh { get; set; }

    public virtual ICollection<TDoVat> TDoVats { get; set; } = new List<TDoVat>();
}
