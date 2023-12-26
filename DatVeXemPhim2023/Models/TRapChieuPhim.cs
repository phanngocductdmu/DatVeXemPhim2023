using System;
using System.Collections.Generic;

namespace DatVeXemPhim2023.Models;

public partial class TRapChieuPhim
{
    public int IdrapChieuPhim { get; set; }

    public int? Iduser { get; set; }

    public string? TenRap { get; set; }

    public string? DiaChi { get; set; }

    public virtual ICollection<TDoVat> TDoVats { get; set; } = new List<TDoVat>();

    public virtual ICollection<TGhe> TGhes { get; set; } = new List<TGhe>();

    public virtual ICollection<TPhongChieu> TPhongChieus { get; set; } = new List<TPhongChieu>();

    public virtual ICollection<TSuatChieu> TSuatChieus { get; set; } = new List<TSuatChieu>();

    public virtual ICollection<TThongTinKhuyenMai> TThongTinKhuyenMais { get; set; } = new List<TThongTinKhuyenMai>();
}
