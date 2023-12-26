using System;
using System.Collections.Generic;

namespace DatVeXemPhim2023.Models;

public partial class THoaDon
{
    public int Id { get; set; }

    public string? IdHoaDon { get; set; }

    public int? IdFood { get; set; }

    public int? IdUser { get; set; }

    public int? IdPhim { get; set; }

    public int? IdGhe { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? NameGhe { get; set; }

    public double? GiaGhe { get; set; }

    public string? NameFood { get; set; }

    public double? GiaFood { get; set; }

    public int? IdRapPhim { get; set; }

    public int? IdSuatChieu { get; set; }

    public virtual TTaiKhoan? IdUserNavigation { get; set; }
}
