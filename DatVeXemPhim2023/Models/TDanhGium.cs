﻿using System;
using System.Collections.Generic;

namespace DatVeXemPhim2023.Models;

public partial class TDanhGium
{
    public int IddanhGia { get; set; }

    public int? Idphim { get; set; }

    public double? DanhGia5sao { get; set; }

    public string? DanhGiaCmt { get; set; }

    public virtual TPhim? IdphimNavigation { get; set; }
}
