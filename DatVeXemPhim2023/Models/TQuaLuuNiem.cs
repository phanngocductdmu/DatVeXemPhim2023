using System;
using System.Collections.Generic;

namespace DatVeXemPhim2023.Models;

public partial class TQuaLuuNiem
{
    public int IdquaLuuNiem { get; set; }

    public string? TenQuaLuuNiem { get; set; }

    public int? Gia { get; set; }

    public virtual ICollection<TDoVat> TDoVats { get; set; } = new List<TDoVat>();
}
