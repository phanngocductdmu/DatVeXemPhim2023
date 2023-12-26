using DatVeXemPhim2023.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace DatVeXemPhim2023.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/suatchieuadmin")]
    public class SuatChieuAdminController : Controller
    {
        QldatVeXemPhimContext db = new QldatVeXemPhimContext();

        [Route("DanhSachSuatChieu")]
        [HttpGet]
        public IActionResult DanhSachSuatChieu(int? page, int? IdUser, int? IdPhim)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            var a = HttpContext.Session.GetString("Iduser");
            ViewBag.IdUser = a;
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.IdPhim = IdPhim;
            ViewBag.IdRapChieu = db.TRapChieuPhims.FirstOrDefault(x=>x.Iduser == Convert.ToInt32(a));
            int pageSize = 10;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;

            var query = from tk in db.TTaiKhoans
                        join rc in db.TRapChieuPhims on tk.Iduser equals rc.Iduser
                        join sc in db.TSuatChieus on rc.IdrapChieuPhim equals sc.IdrapChieuPhim
                        where tk.Iduser == IdUser && sc.Idphim == IdPhim
                        orderby sc.TgbatDau
                        select sc;

            var paginatedQuery = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            PagedList<TSuatChieu> lst = new PagedList<TSuatChieu>(paginatedQuery, pageNumber, pageSize);

            return View(lst);
        }
        [Route("ThemMoiSuatChieu")]
        [HttpGet]
        public IActionResult ThemMoiSuatChieu(int? IdUser, int? IdPhim)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            ViewBag.IdUser = HttpContext.Session.GetString("Iduser");
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.IdPhim = IdPhim;

            ViewBag.DanhSachPhongChieu = (from tk in db.TTaiKhoans
                                          join rc in db.TRapChieuPhims on tk.Iduser equals rc.Iduser
                                          join pc in db.TPhongChieus on rc.IdrapChieuPhim equals pc.IdrapChieuPhim
                                          where tk.Iduser == IdUser
                                          select new SelectListItem
                                          {
                                              Text = pc.TenPhongChieu
                                          }).ToList();

            var rapChieuPhims = db.TRapChieuPhims
                    .Where(x => x.Iduser == IdUser)
                    .FirstOrDefault();
            if (rapChieuPhims != null )
            {
                var diaChiRap = rapChieuPhims.IdrapChieuPhim;
                ViewBag.DiaChiRap = diaChiRap;
                var SuatChieu = new TSuatChieu
                {
                    IdrapChieuPhim = diaChiRap,
                };
                return View(SuatChieu);
            }
            return View();
        }

        [Route("ThemMoiSuatChieu")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemMoiSuatChieu(TSuatChieu suatChieu, int? IdUser, int? IdPhim, string? TenPhongChieu)
        {
            db.TSuatChieus.Add(suatChieu);
            db.SaveChanges();
            return RedirectToAction("DanhSachSuatChieu", new { IdUser = IdUser, IdPhim = IdPhim });
        }

        [Route("SuaSuatChieu")]
        [HttpGet]
        public IActionResult SuaSuatChieu(int? IdUser, int? IdPhim, int? IdSuatChieu)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            ViewBag.IdUser = HttpContext.Session.GetString("Iduser");
            ViewBag.Username = HttpContext.Session.GetString("Username");

            ViewBag.DanhSachPhongChieu = (from tk in db.TTaiKhoans
                                         join rc in db.TRapChieuPhims on tk.Iduser equals rc.Iduser
                                         join pc in db.TPhongChieus on rc.IdrapChieuPhim equals pc.IdrapChieuPhim
                                         where tk.Iduser == IdUser
                                         select new SelectListItem
                                         {
                                             Text = pc.TenPhongChieu
                                         }).ToList();
            var rapChieuPhims = db.TRapChieuPhims
                    .Where(x => x.Iduser == IdUser)
                    .FirstOrDefault();
            var SuatChieu = db.TSuatChieus.Find(IdSuatChieu);
            return View(SuatChieu);
        }

        [Route("SuaSuatChieu")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSuatChieu(TSuatChieu suatChieu, int? IdUser, int? IdPhim, int? IdSuatChieu)
        {
            db.Entry(suatChieu).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("DanhSachSuatChieu", "SuatChieuAdmin", new { IdUser = IdUser, IdPhim = IdPhim });
        }
        [Route("XoaSuatChieu")]
        [HttpGet]
        public IActionResult XoaSuatChieu(int? IdUser, int? IdPhim, int? IdSuatChieu)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            ViewBag.IdUser = HttpContext.Session.GetString("Iduser");
            ViewBag.Username = HttpContext.Session.GetString("Username");

            var SuatChieu = db.TSuatChieus.Where(x => x.IdsuatChieu == IdSuatChieu).ToList();
            db.Remove(db.TSuatChieus.Find(IdSuatChieu));
            db.SaveChanges();
            return RedirectToAction("DanhSachSuatChieu", "SuatChieuAdmin", new { IdUser = IdUser, IdPhim = IdPhim });
        }
    }
}