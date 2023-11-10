using System;
using System.Collections.Generic;

namespace DatVeXemPhim2023.Models;

public partial class TDoVat
{
    public int IddoVat { get; set; }

    public int IdrapChieuPhim { get; set; }

    public int IddoUong { get; set; }

    public int IddoAn { get; set; }

    public int IdquaLuuNiem { get; set; }

    public int IdcomBo { get; set; }

    public virtual TComBo IdcomBoNavigation { get; set; } = null!;

    public virtual TDoAn IddoAnNavigation { get; set; } = null!;

    public virtual TDoUong IddoUongNavigation { get; set; } = null!;

    public virtual TQuaLuuNiem IdquaLuuNiemNavigation { get; set; } = null!;

    public virtual TRapChieuPhim IdrapChieuPhimNavigation { get; set; } = null!;
}
