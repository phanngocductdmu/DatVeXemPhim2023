using DatVeXemPhim2023.Models;
using DatVeXemPhim2023.Repository;
using Microsoft.AspNetCore.Mvc;
namespace DatVeXemPhim2023.ViewComponents
{
    public class DiaChiRapComponent : ViewComponent
    {
        private readonly DiaChiRap _diaChiRap;
        public DiaChiRapComponent(DiaChiRap DiaChiRepository)
        {
            _diaChiRap = DiaChiRepository;
        }
        public IViewComponentResult Invoke()
        {
            var diaChi = _diaChiRap.GetAllDiaChi().OrderBy(x=>x.IdrapChieuPhim);
            return View(diaChi);
        }
    }
}
