using System;
using System.Collections.Generic;

namespace DatVeXemPhim2023.Models;

public partial class TComBo
{
    public int IdcomBo { get; set; }

    public string? TenCombo { get; set; }

    public int? Gia { get; set; }

    public virtual ICollection<TDoVat> TDoVats { get; set; } = new List<TDoVat>();
}
