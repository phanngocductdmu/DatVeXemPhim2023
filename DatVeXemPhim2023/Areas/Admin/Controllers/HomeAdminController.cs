using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatVeXemPhim2023.Models;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;
using DatVeXemPhim2023.ViewModels;

namespace DatVeXemPhim2023.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        QldatVeXemPhimContext db = new QldatVeXemPhimContext();
        [Route("")]
        [Route("index")]
        [HttpGet]
        [HttpPost]
        public IActionResult Index(int? page)
        {
            int pageSize = 20;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listTaiKhoan = db.TTaiKhoans.Where(x=>x.TypeUser == "khach").ToList();
            PagedList<TTaiKhoan> lst = new PagedList<TTaiKhoan>(listTaiKhoan, pageNumber, pageSize);
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            ViewBag.IdUser = HttpContext.Session.GetString("Iduser");
            ViewBag.Username = HttpContext.Session.GetString("Username");
            return View(lst);
        }
        [Route("TKRapPhim")]
        [HttpGet]
        [HttpPost]
        public IActionResult TKRapPhim(int? page)
        {
            int pageSize = 20;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listTaiKhoan = db.TTaiKhoans.Where(x => x.TypeUser == "churapphim").ToList();
            PagedList<TTaiKhoan> lst = new PagedList<TTaiKhoan>(listTaiKhoan, pageNumber, pageSize);
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            ViewBag.IdUser = HttpContext.Session.GetString("Iduser");
            ViewBag.Username = HttpContext.Session.GetString("Username");
            return View(lst);
        }
        [Route("ThemTaiKhoan")]
        [HttpGet]
        public IActionResult ThemTaiKhoan()
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            ViewBag.IdUser = HttpContext.Session.GetString("Iduser");
            ViewBag.Username = HttpContext.Session.GetString("Username");
            return View();
        }
         
        [Route("ThemTaiKhoan")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemTaiKhoan(TTaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.TTaiKhoans.Add(taiKhoan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taiKhoan);
        }
        [Route("SuaTaiKhoan")]
        [HttpGet]
        public IActionResult SuaTaiKhoan(int IdUser)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            ViewBag.IdUser = HttpContext.Session.GetString("Iduser");
            ViewBag.Username = HttpContext.Session.GetString("Username");
            var taikhoan = db.TTaiKhoans.Find(IdUser);
            return View(taikhoan);
        }        
        [Route("SuaTaiKhoan")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaTaiKhoan(TTaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taiKhoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("index", "HomeAdmin");
            }
            return View(taiKhoan);
        }

        [Route("ThemTaiKhoanRP")]
        [HttpGet]
        public IActionResult ThemTaiKhoanRP()
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            ViewBag.IdUser = HttpContext.Session.GetString("Iduser");
            ViewBag.Username = HttpContext.Session.GetString("Username");
            return View();
        }

        [Route("ThemTaiKhoanRP")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemTaiKhoanRP(TTaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.TTaiKhoans.Add(taiKhoan);
                db.SaveChanges();
                return RedirectToAction("TKRapPhim");
            }
            return View(taiKhoan);
        }
        [Route("SuaTaiKhoanRP")]
        [HttpGet]
        public IActionResult SuaTaiKhoanRP(int IdUser)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            ViewBag.IdUser = HttpContext.Session.GetString("Iduser");
            ViewBag.Username = HttpContext.Session.GetString("Username");
            var taikhoan = db.TTaiKhoans.Find(IdUser);
            return View(taikhoan);
        }

        [Route("SuaTaiKhoanRP")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaTaiKhoanRP(TTaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taiKhoan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TKRapPhim", "HomeAdmin");
            }
            return View(taiKhoan);
        }
        [Route("XoaTaiKhoan")]
        [HttpGet]
        public IActionResult XoaTaiKhoan(int IdUser)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            ViewBag.IdUser = HttpContext.Session.GetString("Iduser");
            TempData["Message"] = "";
            var TaiKhoan = db.TTaiKhoans.Where(x => x.Iduser == IdUser).ToList();
            db.Remove(db.TTaiKhoans.Find(IdUser));
            db.SaveChanges();
            TempData["Message"] = "Sản phẩm đã được xóa";
            return RedirectToAction("index", "HomeAdmin");
        }

        [Route("danhmucphim")]
        [HttpGet]
        [HttpPost]
        public IActionResult DanhMucPhim(int? page)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            ViewBag.IdUser = HttpContext.Session.GetString("Iduser");
            ViewBag.Username = HttpContext.Session.GetString("Username");
            int pageSize = 20;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listSanPham = db.TPhims.AsNoTracking().OrderBy(x => x.TenPhim);
            PagedList<TPhim> lst = new PagedList<TPhim>(listSanPham, pageNumber, pageSize);
            return View(lst);
        }
        [Route("ThemPhimMoi")]
        [HttpGet]
        public IActionResult ThemPhimMoi()
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            ViewBag.IdUser = HttpContext.Session.GetString("Iduser");
            ViewBag.Username = HttpContext.Session.GetString("Username");
            return View();
        }

        [Route("ThemPhimMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemPhimMoi(TPhim Phim, IFormFile HinhAnh, [FromServices] IWebHostEnvironment hostingEnvironment)
        {
            var phim = db.TPhims.Find(Phim.Idphim);
            if (phim != null && !string.IsNullOrEmpty(phim.HinhAnh))
            {
                // Delete the old image
                string oldFilePath = Path.Combine(hostingEnvironment.WebRootPath, "LayoutDatVe", "phim", phim.HinhAnh);

                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            if (HinhAnh != null && HinhAnh.Length > 0)
            {
                // Save the new image
                string webRootPath = hostingEnvironment.WebRootPath;
                string uploadFolder = Path.Combine(webRootPath, "LayoutDatVe", "Phim");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                string fileName = Path.GetFileName(HinhAnh.FileName);
                string filePath = Path.Combine(uploadFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    HinhAnh.CopyTo(fileStream);
                }

                Phim.HinhAnh = fileName;
            }
            db.TPhims.Add(Phim);
            db.SaveChanges();
            return RedirectToAction("DanhMucPhim");
        }

        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(int IdPhim)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            ViewBag.IdUser = HttpContext.Session.GetString("Iduser");
            ViewBag.IdPhim = IdPhim;
            var sanPham = db.TPhims.Find(IdPhim);
            return View(sanPham);
        }

        [Route("SuaSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSanPham(TPhim phim, IFormFile HinhAnh, [FromServices] IWebHostEnvironment hostingEnvironment, int? IdPhim)
        {
            var existingPhim = db.TPhims.FirstOrDefault(x => x.Idphim == IdPhim);

            if (existingPhim != null)
            {
                // Delete old image if a new image is provided
                
                    if (!string.IsNullOrEmpty(existingPhim.HinhAnh))
                    {
                        var oldFilePath = Path.Combine(hostingEnvironment.WebRootPath, "LayoutDatVe", "phim", existingPhim.HinhAnh);
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    // Save new image
                    string webRootPath = hostingEnvironment.WebRootPath;
                    string uploadFolder = Path.Combine(webRootPath, "LayoutDatVe", "phim");

                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }

                    string fileName = Path.GetFileName(HinhAnh.FileName);
                    string filePath = Path.Combine(uploadFolder, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            HinhAnh.CopyTo(fileStream);
                        }

                    phim.HinhAnh = fileName;
                

                // Update existing entity
                db.Entry(existingPhim).CurrentValues.SetValues(phim);
                db.SaveChanges();
            }

            return RedirectToAction("DanhMucPhim", "HomeAdmin");
        }


        [Route("XoaPhim")]
        [HttpGet]
        public IActionResult XoaPhim(int IdPhim)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            ViewBag.IdUser = HttpContext.Session.GetString("Iduser");
            TempData["Message"] = "";
            var chiTietSanPham = db.TPhims.Where(x => x.Idphim == IdPhim).ToList();
            db.Remove(db.TPhims.Find(IdPhim));
            db.SaveChanges();
            TempData["Message"] = "Sản phẩm đã được xóa";
            return RedirectToAction("DanhMucPhim", "HomeAdmin");
        }
        
        [Route("danhmucghe")]
        [HttpGet]
        public IActionResult DanhMucGhe(int? page, string? IdPhong, int? IdUser)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            ViewBag.IdUser = HttpContext.Session.GetString("Iduser");
            ViewBag.Username = HttpContext.Session.GetString("Username");
            IdUser = Convert.ToInt32(ViewBag.IdUser);
            ViewBag.IdPhong = IdPhong;

            int pageSize = 20;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;

            var listghe = from rc in db.TRapChieuPhims
                          join pc in db.TPhongChieus on rc.IdrapChieuPhim equals pc.IdrapChieuPhim
                          join g in db.TGhes on pc.TenPhongChieu equals g.TenPhongChieu
                          where rc.Iduser == IdUser && pc.TenPhongChieu == IdPhong
                          select g;
            listghe = listghe.OrderBy(x => x.TenPhongChieu);
            PagedList<TGhe> lst = new PagedList<TGhe>(listghe, pageNumber, pageSize);
            return View(lst);
        }
        [Route("ThemGhe")]
        [HttpGet]
        public IActionResult ThemGhe(string? IdPhong, int? IdUser)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            ViewBag.IdUser = HttpContext.Session.GetString("Iduser");
            ViewBag.Username = HttpContext.Session.GetString("Username");

            ViewBag.IdPhong = IdPhong;

            var rapchieuphim = db.TRapChieuPhims.SingleOrDefault(x => x.Iduser == IdUser);
            if(rapchieuphim != null)
            {
                ViewBag.IdRapPhim = rapchieuphim.IdrapChieuPhim;
            }

            return View();
        }

        [Route("ThemGhe")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemGhe(TGhe ghe, string? IdPhong, int? IdUser)
        {
            db.TGhes.Add(ghe);
            db.SaveChanges();
            return RedirectToAction("DanhMucGhe", new { Idphong = IdPhong, IdUser = IdUser });
        }
        [Route("SuaGhe")]
        [HttpGet]
        public IActionResult SuaGhe(int IdGhe, string? IdPhong, int? IdUser)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            ViewBag.IdUser = HttpContext.Session.GetString("Iduser");
            IdUser = Convert.ToInt32(ViewBag.IdUser);
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.IdPhong = IdPhong;
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
            var ghe = db.TGhes.Find(IdGhe);
            return View(ghe);
        }

        [Route("SuaGhe")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaGhe(TGhe ghe, string? IdPhong, int? IdUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ghe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhMucGhe", "HomeAdmin", new {IdPhong = IdPhong, IdUser = IdUser});
            }
            return View(ghe);
        }
        [Route("XoaGhe")]
        [HttpGet]
        public IActionResult XoaGhe(int IdGhe, string? IdPhong, int? IdUser)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            ViewBag.IdUser = HttpContext.Session.GetString("Iduser");
            IdUser = Convert.ToInt32(ViewBag.IdUser);
            ViewBag.Username = HttpContext.Session.GetString("Username");
            TempData["Message"] = "";
            var ghe = db.TGhes.Where(x => x.Idghe == IdGhe).ToList();
            db.Remove(db.TGhes.Find(IdGhe));
            db.SaveChanges();
            TempData["Message"] = "Sản phẩm đã được xóa";
            return RedirectToAction("DanhMucGhe", "HomeAdmin", new {IdPhong = IdPhong, IdUser = IdUser});
        }

        [Route("XemChiTietMoHinhGheNgoi")]
        [HttpGet]
        public IActionResult XemChiTietMoHinhGheNgoi(string? IdPhong, int? IdUser)
        {
            ViewBag.IdPhong = IdPhong;
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            ViewBag.IdUser = HttpContext.Session.GetString("Iduser");
            ViewBag.Username = HttpContext.Session.GetString("Username");
            var listghe = from rc in db.TRapChieuPhims
                          join pc in db.TPhongChieus on rc.IdrapChieuPhim equals pc.IdrapChieuPhim
                          join g in db.TGhes on pc.TenPhongChieu equals g.TenPhongChieu
                          where rc.Iduser == IdUser && pc.TenPhongChieu == IdPhong
                          select g;
            List<TGhe> lst = new List<TGhe>(listghe);
            return View(lst);
        }

        [Route("PhongXemPhim")]
        [HttpGet]            
        public IActionResult PhongXemPhim(int? page, int? IdUser)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            ViewBag.IdUser = HttpContext.Session.GetString("Iduser");
            ViewBag.Username = HttpContext.Session.GetString("Username");
           
            int pageSize = 10;
            int pageNumber = page == null || page < 1 ? 1 : page.Value;

            var query = from tk in db.TTaiKhoans
                        join rc in db.TRapChieuPhims on tk.Iduser equals rc.Iduser
                        join pc in db.TPhongChieus on rc.IdrapChieuPhim equals pc.IdrapChieuPhim
                        where tk.Iduser == IdUser
                        select pc;

            var paginatedQuery = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            PagedList<TPhongChieu> lst = new PagedList<TPhongChieu>(paginatedQuery, pageNumber, pageSize);

            return View(lst);
        }

        [Route("ThemPhong")]
        [HttpGet]
        public IActionResult ThemPhong(int? IdUser)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            ViewBag.IdUser = HttpContext.Session.GetString("Iduser");
            ViewBag.Username = HttpContext.Session.GetString("Username");

            var rapChieuPhims = db.TRapChieuPhims
                    .Where(x => x.Iduser == IdUser)
                    .FirstOrDefault();
            if (rapChieuPhims != null)
            {
                var diaChiRap = rapChieuPhims.IdrapChieuPhim;
                ViewBag.DiaChiRap = diaChiRap;

                var phongChieu = new TPhongChieu
                {
                    IdrapChieuPhim = diaChiRap
                };

                return View(phongChieu);
            }
            return View();
        }

        [Route("ThemPhong")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemPhong(TPhongChieu phong, int? IdUser)
        {
            if (ModelState.IsValid)
            {
                db.TPhongChieus.Add(phong);
                db.SaveChanges();
                return RedirectToAction("PhongXemPhim", new { IdUser });
            }
            return View(phong);
        }
        [Route("SuaPhong")]
        [HttpGet]
        public IActionResult SuaPhong(string? TenPhong, int? IdUser)
        {
            ViewBag.IdUser = HttpContext.Session.GetString("Iduser");
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            var rapChieuPhims = db.TRapChieuPhims
                    .Where(x => x.Iduser == IdUser)
                    .FirstOrDefault();

            if (rapChieuPhims != null)
            {
                var diaChiRap = rapChieuPhims.IdrapChieuPhim;
                ViewBag.DiaChiRap = diaChiRap;

                var phongChieu = new TPhongChieu
                {
                    IdrapChieuPhim = diaChiRap
                };

                return View(phongChieu);
            }
            var phong = db.TPhongChieus.Find(TenPhong);
            return View(phong);
        }

        [Route("SuaPhong")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaPhong(TPhongChieu IdPhong, int? IdUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(IdPhong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PhongXemPhim", "HomeAdmin", new { IdUser });
            }
            return View(IdPhong);
        }    
    }
}