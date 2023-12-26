using Microsoft.AspNetCore.Mvc;
using DatVeXemPhim2023.Models;
using DatVeXemPhim2023.Models.Authentication;

namespace DatVeXemPhim2023.Controllers
{
    public class AccessController : Controller
    {
        QldatVeXemPhimContext db = new QldatVeXemPhimContext();
        [HttpGet]
        public IActionResult Login(int? IdPhim)
        {
            ViewBag.IdPhim = IdPhim;
            if (HttpContext.Session.GetString("Username") == null)
            {
                return View();
            }
            if (IdPhim.HasValue)
            {
                return RedirectToAction("DatVe", "Datve", new { IdPhim = IdPhim });
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        public IActionResult LoginAdmin()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return View();
            }
            return RedirectToAction("Admin", "homeadmin");
        }
        public IActionResult LoginRapPhim()
        {
            if (HttpContext.Session.GetString("Username") == null)
            {
                return View();
            }
            return RedirectToAction("Admin", "danhmucphim");
        }
        [HttpPost]
        public IActionResult Login(TTaiKhoan user, int? IdPhim)
        {
            HttpContext.Response.Cookies.Delete("tenPhongChieu");
            HttpContext.Response.Cookies.Delete("selectedTime");
            HttpContext.Response.Cookies.Delete("chosePlaces");
            HttpContext.Response.Cookies.Delete("carts");
            HttpContext.Response.Cookies.Delete("idSuatChieu");
            HttpContext.Response.Cookies.Delete("selectedTime");
            HttpContext.Response.Cookies.Delete("idRapPhim");
            if (HttpContext.Session.GetString("Username") == null)
            {
                var u = db.TTaiKhoans.Where(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password)).FirstOrDefault();

                if (u != null)
                {
                    if (u.TypeUser == "khach")
                    {
                        HttpContext.Session.SetString("Username", u.Username.ToString());
                        HttpContext.Session.SetString("Iduser", u.Iduser.ToString());
                        HttpContext.Session.SetString("TypeUser", u.TypeUser.ToString());
                        if (IdPhim.HasValue)
                        {
                            ViewBag.IdPhim = IdPhim;
                            TempData["IdPhim"] = IdPhim;
                        }
                        if (IdPhim.HasValue)
                        {
                            return RedirectToAction("DatVe", "Datve", new { IdPhim = IdPhim });
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else if (u.TypeUser == "admin")
                    {
                        ViewBag.type = HttpContext.Session.GetString("TypeUser");
                        HttpContext.Session.SetString("Username", u.Username.ToString());

                        ViewBag.Username = HttpContext.Session.GetString("Username");

                        ViewBag.IdUser = HttpContext.Session.GetString("Iduser");
                        HttpContext.Session.SetString("Iduser", u.Iduser.ToString());

                        HttpContext.Session.SetString("TypeUser", u.TypeUser.ToString());
                        return RedirectToAction("HomeAdmin", "Admin");
                    }
                    else if(u.TypeUser == "churapphim")
                    {
                        ViewBag.Username = HttpContext.Session.GetString("Username");

                        ViewBag.type = HttpContext.Session.GetString("TypeUser");
                        HttpContext.Session.SetString("Username", u.Username.ToString());

                        ViewBag.IdUser = HttpContext.Session.GetString("Iduser");
                        HttpContext.Session.SetString("Iduser", u.Iduser.ToString());

                        HttpContext.Session.SetString("TypeUser", u.TypeUser.ToString());
                        return RedirectToAction("danhmucphim", "Admin");
                    }
                }
            }
            return View();
        }
        [Route("DangKy")]
        [HttpGet]
        public IActionResult DangKi()
        {
            return View();
        }

        [Route("DangKy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DangKy(TTaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                taiKhoan.TypeUser = "khach";
                db.TTaiKhoans.Add(taiKhoan);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(taiKhoan);
        }
        public IActionResult Logout(int? IdPhim)
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Access", new {IdPhim = IdPhim});
        }
    }
}