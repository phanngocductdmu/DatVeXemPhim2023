using System;
using System.Collections.Generic;

namespace DatVeXemPhim2023.Models;

public partial class TDoUong
{
    public int IddoUong { get; set; }

    public string? TenDoUong { get; set; }

    public int? Gia { get; set; }

    public virtual ICollection<TDoVat> TDoVats { get; set; } = new List<TDoVat>();
}
