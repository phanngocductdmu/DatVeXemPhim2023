using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DatVeXemPhim2023.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;

namespace DatVeXemPhim2023.Controllers
{
    public class HomeController : Controller
    {
        QldatVeXemPhimContext db = new QldatVeXemPhimContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int? page)
        {
            int pageSize = 9;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listSanPham = db.TPhims.AsNoTracking().OrderBy(x => x.TenPhim);
            PagedList<TPhim> lst = new PagedList<TPhim>(listSanPham, pageNumber, pageSize);

            /*var a = HttpContext.Session.GetString("Username");
            ViewBag.Username = a;*/

            return View(lst);
        }

        public IActionResult ChiTietPhim(int IdPhim)
        {
            var phim = db.TPhims.SingleOrDefault(x=>x.Idphim == IdPhim);
            var DanhGiaPhim = db.TDanhGia.Where(x => x.Idphim == IdPhim);
            double cuont = 0;
            double tongDanhGia = 0;
            foreach (var item in DanhGiaPhim)
            {
                tongDanhGia = (double)(tongDanhGia + item.DanhGia5sao);
                cuont++;
            }
            double b = cuont;
            double a = tongDanhGia;
            float c = (float)(tongDanhGia / (float)b);
            if (c > 0)
            {
                ViewBag.DanhGia = c;
            }
            else
            {
                ViewBag.DanhGia = "Chưa có lượt Đánh giá";
            }
            return View(phim);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}