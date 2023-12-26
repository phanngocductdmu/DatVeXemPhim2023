using System;
using System.Collections.Generic;

namespace DatVeXemPhim2023.Models;

public partial class BangDatFood
{
    public int Id { get; set; }

    public int? IdUser { get; set; }

    public int? IdFood { get; set; }
}
