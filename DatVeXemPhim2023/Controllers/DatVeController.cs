using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DatVeXemPhim2023.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;
using DatVeXemPhim2023.ViewModels;
namespace DatVeXemPhim2023.Controllers
{
    public class DatVeController : Controller
    {
        QldatVeXemPhimContext db = new QldatVeXemPhimContext();
        public IActionResult DatVe(int? IdPhim)
        {
            var a = HttpContext.Session.GetString("Username");
            if (a != null)
            {
                ViewBag.Login = "ĐĂNG XUẤT";

                if (IdPhim.HasValue)
                {
                    ViewBag.IDPhim = IdPhim;
                    var ListClass = new ListClass
                    {
                        phims = db.TPhims.SingleOrDefault(x => x.Idphim == IdPhim),
                        LsuatChieus = db.TSuatChieus.Where(x => x.Idphim == IdPhim).OrderBy(x => x.TgbatDau).ToList(),
                        LRapPhim = db.TRapChieuPhims.ToList(),
                        Lphims = db.TPhims.ToList(),
                    };
                    return View(ListClass);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.Login = "ĐĂNG NHẬP";
                return RedirectToAction("Login", "Access", new {IdPhim = IdPhim});
            }
        }

        public IActionResult DiaChi(int? IdPhim, int? IdRapPhim)
        {
            ViewBag.IDRapPhim = IdRapPhim;
            var ListClass = new ListClass
            {
                phims = db.TPhims.SingleOrDefault(x => x.Idphim == IdPhim),
                rapChieuPhim = db.TRapChieuPhims.SingleOrDefault(x => x.IdrapChieuPhim == IdRapPhim),
                LRapPhim = db.TRapChieuPhims.Where(x=>x.IdrapChieuPhim == IdRapPhim).ToList(),
                LsuatChieus = db.TSuatChieus.Where(x => x.Idphim == IdPhim).OrderBy(x => x.TgbatDau).ToList(),
                Lphims = db.TPhims.ToList(),
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
    }
}