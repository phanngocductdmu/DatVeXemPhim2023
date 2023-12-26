using System;
using System.Collections.Generic;

namespace DatVeXemPhim2023.Models
{
    public class TTheoDoi
    {
        public int IdTheoDoi { get; set; }

        public int? IdUser { get; set; }

        public int? IdPhim { get; set; }

        public virtual TTaiKhoan IdtaiKhoanNavigation { get; set; } = null!;
    }
}
