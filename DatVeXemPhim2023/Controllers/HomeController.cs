using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DatVeXemPhim2023.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;
using DatVeXemPhim2023.ViewModels;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Http;

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
            var listPhim = db.TPhims.AsNoTracking().OrderBy(x => x.TenPhim);
            PagedList<TPhim> lst = new PagedList<TPhim>(listPhim, pageNumber, pageSize);

            var a = HttpContext.Session.GetString("Username");

            if (a != null)
            {
                ViewBag.Login = "ĐĂNG XUẤT";
            }
            else
            {
                ViewBag.Login = "ĐĂNG NHẬP";
            }
            return View(lst);
        }

        public IActionResult ChiTietPhim(int? IdPhim, string? cmt)
        {
            var aa = HttpContext.Session.GetString("Username");
            ViewBag.UserName = aa;
            if (aa != null)
            {
                ViewBag.Login = "ĐĂNG XUẤT";
            }
            else
            {
                ViewBag.Login = "ĐĂNG NHẬP";
            }
            //Danh Gia Phim
            var DanhGiaPhim = db.TDanhGia.Where(x => x.Idphim == IdPhim);
            double cuont = 0;
            double tongDanhGia = 0;

            foreach (var item in DanhGiaPhim)
            {
                if (item.DanhGia5sao.HasValue)
                {
                    tongDanhGia = (double)(tongDanhGia + item.DanhGia5sao);
                    cuont++;
                }
            }

            double b = cuont;
            double a = tongDanhGia;

            float c = (float)(tongDanhGia / (float)b);

            if (cuont > 0)
            {
                float roundedC = (float)Math.Round(c, 1);

                ViewBag.DanhGia = roundedC;
            }
            else
            {
                ViewBag.DanhGia = "Chưa có lượt Đánh giá";
            }

            var ListClass = new ListClass
            {
                phims = db.TPhims.SingleOrDefault(x => x.Idphim == IdPhim),
                LdanhGia = db.TDanhGia.Where(x => x.Idphim == 1).ToList()
            };
            return View(ListClass);
        }
        public IActionResult DatChoNgoi(int? IdPhim, int? IdRapPhim)
        {
            var tenPhongChieu = HttpContext.Request.Cookies["TenPhongChieu"];
            var idSuatChieu = HttpContext.Request.Cookies["idSuatChieu"];
            var idUser = HttpContext.Session.GetString("Iduser");
            var Lghes = (from sc in db.TSuatChieus
                         join rc in db.TRapChieuPhims on sc.IdrapChieuPhim equals rc.IdrapChieuPhim
                         join g in db.TGhes on sc.TenPhongChieu equals g.TenPhongChieu
                         where sc.IdsuatChieu == Convert.ToInt32(idSuatChieu)
                         select g).ToList();

            var ListClass = new ListClass
            {
                rapChieuPhim = db.TRapChieuPhims.SingleOrDefault(x => x.IdrapChieuPhim == IdRapPhim),
                phims = db.TPhims.SingleOrDefault(x => x.Idphim == IdPhim),
                Lghes = Lghes,
                LhoaDons = db.THoaDons.Where(h => h.IdRapPhim == IdRapPhim && h.IdSuatChieu == Convert.ToInt32(idSuatChieu)).ToList()
            };
            var a = HttpContext.Session.GetString("Username");

            if (a != null)
            {
                ViewBag.Login = "ĐĂNG XUẤT";
            }
            else
            {
                ViewBag.Login = "ĐĂNG NHẬP";
            }
            return View(ListClass);
        }


        [HttpGet]
        public IActionResult Search(string movieName, int? page)
        {
            int pageSize = 9;
            int pageNumber = page ?? 1;

            var query = db.TPhims
                    .AsNoTracking()
                    .Where(x => EF.Functions.Like(x.TenPhim, "%" + movieName + "%") || EF.Functions.Like(x.TheLoai, "%" + movieName + "%"));

            PagedList<TPhim> searchResult = new PagedList<TPhim>(query, pageNumber, pageSize);

            return View("index", searchResult);
        }

        public IActionResult MuaHang(int IdPhim, int IdRapPhim)
        {
            var ListClass = new ListClass
            {
                rapChieuPhim = db.TRapChieuPhims.SingleOrDefault(x => x.IdrapChieuPhim == IdRapPhim),
                phims = db.TPhims.SingleOrDefault(x => x.Idphim == IdPhim),
                LdoAns = db.TDoAns.ToList(),
                LdoUongs = db.TDoUongs.ToList(),
                LcomBos = db.TComBos.ToList(),
            };
            var a = HttpContext.Session.GetString("Username");

            if (a != null)
            {
                ViewBag.Login = "ĐĂNG XUẤT";
            }
            else
            {
                ViewBag.Login = "ĐĂNG NHẬP";
            }
            return View(ListClass);
        }
        public IActionResult CheckHoaDon(int IdPhim)
        {
            var listClass = new ListClass
            {
                phims = db.TPhims.SingleOrDefault(x => x.Idphim == IdPhim),
            };
            var a = HttpContext.Session.GetString("Username");

            if (a != null)
            {
                ViewBag.Login = "ĐĂNG XUẤT";
            }
            else
            {
                ViewBag.Login = "ĐĂNG NHẬP";
            }
            var userId = HttpContext.Session.GetString("Iduser");
            if (userId != null)
            {
                ViewBag.UserId = userId;
            }
            return View(listClass);
        }
        [HttpPost]
        public IActionResult LuuHoaDon(HoaDonViewModel hoaDonViewModel)
        {
            var userId = HttpContext.Session.GetString("Iduser");
            //
            THoaDon hoaDon = new THoaDon()
            {
                IdHoaDon = hoaDonViewModel?.IdHoaDon,
                IdUser = Convert.ToInt32(userId),
                IdFood = hoaDonViewModel?.IdFood,
                NameFood = hoaDonViewModel?.TenFood,
                GiaFood = hoaDonViewModel?.GiaFood,
                IdGhe = hoaDonViewModel?.IdGhe,
                NameGhe = hoaDonViewModel?.TenGhe,
                GiaGhe = hoaDonViewModel?.GiaGhe,
                IdPhim = hoaDonViewModel?.IdPhim,
                IdRapPhim = hoaDonViewModel?.IdRapPhim,
                IdSuatChieu = hoaDonViewModel?.IdSuatChieu,
                CreatedAt = DateTime.Now
            };
            db.THoaDons.Add(hoaDon);
            db.SaveChanges();

            HttpContext.Response.Cookies.Delete("tenPhongChieu");
            HttpContext.Response.Cookies.Delete("selectedTime");
            HttpContext.Response.Cookies.Delete("chosePlaces");
            HttpContext.Response.Cookies.Delete("carts");
            HttpContext.Response.Cookies.Delete("selectedTime");


            return RedirectToAction("CheckHoaDon", "Home");
        }
        [HttpPost]
        public IActionResult CreateThanhToan(HoaDonViewModel hoaDonViewModel)
        {
            var idUser = HttpContext.Session.GetString("Iduser")!;
            TThanhToan thanhToan = db.TThanhToans.FirstOrDefault(tt => tt.IdhoaDon == hoaDonViewModel!.IdHoaDon);
            if (thanhToan is null)
            {
                TThanhToan newThanhToan = new TThanhToan()
                {
                    IdhoaDon = hoaDonViewModel.IdHoaDon,
                    IdUser = Convert.ToInt32(idUser),
                    EmailKhachHang = hoaDonViewModel?.EmailUser,
                    PhoneKhachHang = hoaDonViewModel?.PhoneUser,
                    NgayThanhToan = DateTime.Now,
                    NgayDatGhe = hoaDonViewModel?.NgayDatGhe
                };
                db.TThanhToans.Add(newThanhToan);
                db.SaveChanges();
                var thanhtoan = db.TThanhToans.FirstOrDefault(tt => tt.IdhoaDon == hoaDonViewModel!.IdHoaDon);
                if(thanhtoan != null)
                {
                    return Json(thanhtoan?.IdthanhToan);
                }
            }
            return Json(false);
        }

        public IActionResult TheoDoi(int? IdUser)
        {
            var a = HttpContext.Session.GetString("Username");
            IdUser = db.TTaiKhoans
                .Where(x => x.Username == a)
                .Select(x => x.Iduser)
                .FirstOrDefault();
            if (a != null)
            {
                ViewBag.Login = "ĐĂNG XUẤT";
                var ListPhim = from tk in db.TTaiKhoans
                               join td in db.TTheoDois
                               on tk.Iduser equals td.IdUser
                               join
                               p in db.TPhims on td.IdPhim equals p.Idphim
                               where tk.Iduser == IdUser
                               select p;
                List<TPhim> lst = new List<TPhim>(ListPhim);
                return View(lst);
            }
            else
            {
                ViewBag.Login = "ĐĂNG NHẬP";
                return RedirectToAction("Login", "Access");
            }
        }
        public IActionResult ThemTheoDoi(int? IdPhim)
        {
            var a = HttpContext.Session.GetString("Username");

            if (a != null)
            {
                int IdUser = db.TTaiKhoans.Where(x => x.Username == a).Select(x => x.Iduser).FirstOrDefault();
                bool alreadyExists = db.TTheoDois.Any(td => td.IdPhim == IdPhim && td.IdUser == IdUser);

                if (!alreadyExists)
                {
                    var TheoDois = new TTheoDoi
                    {
                        IdPhim = IdPhim,
                        IdUser = IdUser,
                    };

                    db.TTheoDois.Add(TheoDois);
                    db.SaveChanges();
                }
                return RedirectToAction("index", "home");
            }
            else
            {
                ViewBag.Login = "ĐĂNG NHẬP";
                return RedirectToAction("Login", "Access");
            }
        }

        public IActionResult XoaTheoDoi(int? IdPhim)
        {
            var theoDoi = db.TTheoDois.FirstOrDefault(x => x.IdPhim == IdPhim);
            int idTheoDoi = theoDoi.IdTheoDoi;
            db.Remove(db.TTheoDois.Find(idTheoDoi));
            db.SaveChanges();
            return RedirectToAction("TheoDoi", "home");
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