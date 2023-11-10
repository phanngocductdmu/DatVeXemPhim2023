using System;
using System.Collections.Generic;

namespace DatVeXemPhim2023.Models;

public partial class TRapChieuPhim
{
    public int IdrapChieuPhim { get; set; }

    public string? TenRap { get; set; }

    public string? DiaChi { get; set; }

    public virtual ICollection<TDoVat> TDoVats { get; set; } = new List<TDoVat>();

    public virtual ICollection<TPhongChieu> TPhongChieus { get; set; } = new List<TPhongChieu>();

    public virtual ICollection<TSuatChieu> TSuatChieus { get; set; } = new List<TSuatChieu>();
}
