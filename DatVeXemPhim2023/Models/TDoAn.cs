using System;
using System.Collections.Generic;

namespace DatVeXemPhim2023.Models;

public partial class TDoAn
{
    public int IddoAn { get; set; }

    public string? TenDoAn { get; set; }

    public int? Gia { get; set; }

    public virtual ICollection<TDoVat> TDoVats { get; set; } = new List<TDoVat>();
}
