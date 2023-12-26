using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DatVeXemPhim2023.Models;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;
using DatVeXemPhim2023.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Azure;

namespace DatVeXemPhim2023.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/TinMoiVaUuDai")]
    public class TinMoiVaUuDaiController : Controller
    {
        QldatVeXemPhimContext db = new QldatVeXemPhimContext();

        [Route("DanhsachKhuyenMai")]
        [HttpGet]
        public IActionResult DanhSachKhuyenMai(int? page, int? IdUser)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            var a = HttpContext.Session.GetString("Iduser");
            ViewBag.IdUser = a;
            ViewBag.Username = HttpContext.Session.GetString("Username");
            var idrapchieu = db.TRapChieuPhims.FirstOrDefault(x => x.Iduser == Convert.ToInt32(a));
            if(idrapchieu != null)
            {
                ViewBag.IdRapChieu = idrapchieu.IdrapChieuPhim;
            }
            int pageSize = 20;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listSanPham = db.TThongTinKhuyenMais.AsNoTracking().OrderBy(x => x.TimeBegin);
            PagedList<TThongTinKhuyenMai> lst = new PagedList<TThongTinKhuyenMai>(listSanPham, pageNumber, pageSize);
            return View(lst);
        }

        [Route("ThemTinMoiVaUuDai")]
        [HttpGet]
        public IActionResult ThemTinMoiVaUuDai()
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            var a = HttpContext.Session.GetString("Iduser");
            ViewBag.IdUser = a;
            ViewBag.Username = HttpContext.Session.GetString("Username");
            
            return View();
        }

        [Route("ThemTinMoiVaUuDai")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemTinmoiVaUuDai(TThongTinKhuyenMai uuDaiVaKhuyenMai, IFormFile HinhAnh, [FromServices] IWebHostEnvironment hostingEnvironment, int? IdRapPhim)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            var a = HttpContext.Session.GetString("Iduser");
            ViewBag.IdUser = a;
            ViewBag.Username = HttpContext.Session.GetString("Username");
            var idrapchieu = db.TRapChieuPhims.FirstOrDefault(x => x.Iduser == Convert.ToInt32(a));
            if (idrapchieu != null)
            {
                IdRapPhim = idrapchieu.IdrapChieuPhim;
            }
            var existingKhuyenMai = db.TThongTinKhuyenMais.Find(uuDaiVaKhuyenMai.IdkhuyenMai);
            if (existingKhuyenMai != null && !string.IsNullOrEmpty(existingKhuyenMai.HinhAnh))
            {
                // Delete the old image
                string oldFilePath = Path.Combine(hostingEnvironment.WebRootPath, "LayoutDatVe", "UuDaiVaKhuyenMai", existingKhuyenMai.HinhAnh);

                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            if (HinhAnh != null && HinhAnh.Length > 0)
            {
                // Save the new image
                string webRootPath = hostingEnvironment.WebRootPath;
                string uploadFolder = Path.Combine(webRootPath, "LayoutDatVe", "UuDaiVaKhuyenMai");

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

                uuDaiVaKhuyenMai.HinhAnh = fileName;
            }

            uuDaiVaKhuyenMai.IdrapChieuPhim = (int)IdRapPhim;
            db.TThongTinKhuyenMais.Add(uuDaiVaKhuyenMai);
            db.SaveChanges();

            return RedirectToAction("DanhSachKhuyenMai", "TinMoiVaUuDai");
        }

        [Route("SuaKhuyenMai")]
        [HttpGet]
        public IActionResult SuaKhuyenMai(int? IdUser, int? IdKhuyenMai)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            ViewBag.IdUser = HttpContext.Session.GetString("Iduser");
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.IdKhuyenMai= IdKhuyenMai;
            var khuyenMai = db.TThongTinKhuyenMais.Find(IdKhuyenMai);
            return View(khuyenMai);
        }

        [Route("SuaKhuyenMai")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaKhuyenMai(TThongTinKhuyenMai khuyenMai, IFormFile HinhAnh, int? IdUser, int? IdKhuyenMai, [FromServices] IWebHostEnvironment hostingEnvironment, int? IdRapPhim)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            var a = HttpContext.Session.GetString("Iduser");
            ViewBag.IdUser = a;
            ViewBag.Username = HttpContext.Session.GetString("Username");
            var idrapchieu = db.TRapChieuPhims.FirstOrDefault(x => x.Iduser == Convert.ToInt32(a));
            if (idrapchieu != null)
            {
                IdRapPhim = idrapchieu.IdrapChieuPhim;
            }

            var existingKhuyenMai = db.TThongTinKhuyenMais
                .AsNoTracking()
                .FirstOrDefault(x => x.IdkhuyenMai == IdKhuyenMai);

            if (existingKhuyenMai != null)
            {
                if (HinhAnh != null && HinhAnh.Length > 0)
                {
                    if (!string.IsNullOrEmpty(existingKhuyenMai.HinhAnh) && existingKhuyenMai.HinhAnh != khuyenMai.HinhAnh)
                    {
                        var oldFilePath = Path.Combine(hostingEnvironment.WebRootPath, "LayoutDatVe", "UuDaiVakhuyenMai", existingKhuyenMai.HinhAnh);
                        if (oldFilePath != null)
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                        else
                        {
                            Console.WriteLine("Old file does not exist: " + oldFilePath);
                        }
                    }

                    string webRootPath = hostingEnvironment.WebRootPath;
                    string uploadFolder = Path.Combine(webRootPath, "LayoutDatVe", "UuDaiVaKhuyenMai");

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

                    khuyenMai.HinhAnh = fileName;
                }               

                khuyenMai.IdrapChieuPhim = (int)IdRapPhim;

                db.Attach(existingKhuyenMai);
                db.Entry(existingKhuyenMai).CurrentValues.SetValues(khuyenMai);

                db.SaveChanges();
            }
            return RedirectToAction("DanhSachKhuyenMai", "TinMoiVaUuDai", new { IdUser = IdUser });
        }

        [Route("ChiTietKhuyenMai")]
        [HttpGet]
        public IActionResult ChiTietKhuyenMai(int? IdKhuyenMai)
        {
            ViewBag.IdKhuyenMai = IdKhuyenMai;
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            var a = HttpContext.Session.GetString("Iduser");
            ViewBag.IdUser = a;
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.IdRapChieu = db.TRapChieuPhims.FirstOrDefault(x => x.Iduser == Convert.ToInt32(a));
            var KhuyenMai = db.TThongTinKhuyenMais.FirstOrDefault(x => x.IdkhuyenMai == IdKhuyenMai);
            return View(KhuyenMai);
        }

        [Route("XoaKhuyenMai")]
        [HttpGet]
        public IActionResult XoaKhuyenMai(int? IdKhuyenMai)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            var IdUser = HttpContext.Session.GetString("Iduser");
            ViewBag.IdUser = IdUser;
            ViewBag.Username = HttpContext.Session.GetString("Username");
            var KhuyenMai = db.TThongTinKhuyenMais.Where(x => x.IdkhuyenMai == IdKhuyenMai).ToList();
            db.Remove(db.TThongTinKhuyenMais.Find(IdKhuyenMai));
            db.SaveChanges();
            return RedirectToAction("DanhSachKhuyenMai", "TinMoivaUuDai", new { IdUser = IdUser });
        }

        [Route("DanhSachThucAn")]
        [HttpGet]
        public IActionResult DanhSachThucAn(int? page,int? IdRapPhim)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            var a = HttpContext.Session.GetString("Iduser");
            ViewBag.IdUser = a;
            ViewBag.Username = HttpContext.Session.GetString("Username");
            var idrapchieu = db.TRapChieuPhims.FirstOrDefault(x => x.Iduser == Convert.ToInt32(a));
            if (idrapchieu != null)
            {
                IdRapPhim = idrapchieu.IdrapChieuPhim;
                ViewBag.IdRapPhim = IdRapPhim;
            }
            var ThucAn = db.TDoAns.Where(x => x.IdRapChieuPhim == IdRapPhim);
            int pageSize = 20;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            PagedList<TDoAn> lst = new PagedList<TDoAn>(ThucAn, pageNumber, pageSize);
            return View(lst);
        }

        [Route("ThemDoAn")]
        [HttpGet]
        public IActionResult ThemDoAn(int? IdRapPhim)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            var a = HttpContext.Session.GetString("Iduser");
            ViewBag.IdUser = a;
            ViewBag.Username = HttpContext.Session.GetString("Username");
            var idrapchieu = db.TRapChieuPhims.FirstOrDefault(x => x.Iduser == Convert.ToInt32(a));
            if (idrapchieu != null)
            {
                IdRapPhim = idrapchieu.IdrapChieuPhim;
                ViewBag.IdRapPhim = IdRapPhim;
            }
            return View();
        }

        [Route("ThemDoAn")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemDoAn(TDoAn doAn, int? IdRapPhim, IFormFile HinhAnh, [FromServices] IWebHostEnvironment hostingEnvironment )
        {
            var existingKhuyenMai = db.TDoAns.Find(doAn.IddoAn);
            if (existingKhuyenMai != null && !string.IsNullOrEmpty(existingKhuyenMai.HinhAnh))
            {
                // Delete the old image
                string oldFilePath = Path.Combine(hostingEnvironment.WebRootPath, "LayoutDatVe", "img", "quangcao",existingKhuyenMai.HinhAnh);

                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            if (HinhAnh != null && HinhAnh.Length > 0)
            {
                // Save the new image
                string webRootPath = hostingEnvironment.WebRootPath;
                string uploadFolder = Path.Combine(webRootPath, "LayoutDatVe", "img", "quangcao");

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

                doAn.HinhAnh = fileName;
            }

            doAn.IdRapChieuPhim = (int)IdRapPhim;
            db.TDoAns.Add(doAn);
            db.SaveChanges();

            return RedirectToAction("DanhSachThucAn", "TinMoiVaUuDai");
        }

        [Route("SuaDoAn")]
        [HttpGet]
        public IActionResult SuaDoAn(int? IdRapPhim, int? IdDoAn)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            var a = HttpContext.Session.GetString("Iduser");
            ViewBag.IdUser = a;
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.IdDoAn = IdDoAn;
            var idrapchieu = db.TRapChieuPhims.FirstOrDefault(x => x.Iduser == Convert.ToInt32(a));
            if (idrapchieu != null)
            {
                IdRapPhim = idrapchieu.IdrapChieuPhim;
                ViewBag.IdRapPhim = IdRapPhim;
            }
            ViewBag.IdDoAn = IdDoAn;
            var doAn = db.TDoAns.Find(IdDoAn);
            return View(doAn);
        }

        [Route("SuaDoAn")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaDoAn(TDoAn DoAn, IFormFile HinhAnh, int? IdUser, int? IdDoAn, [FromServices] IWebHostEnvironment hostingEnvironment, int? IdRapPhim)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            var a = HttpContext.Session.GetString("Iduser");
            ViewBag.IdUser = a;
            ViewBag.Username = HttpContext.Session.GetString("Username");
            var idrapchieu = db.TRapChieuPhims.FirstOrDefault(x => x.Iduser == Convert.ToInt32(a));
            if (idrapchieu != null)
            {
                IdRapPhim = idrapchieu.IdrapChieuPhim;
            }

            var existingKhuyenMai = db.TDoAns
                .AsNoTracking()
                .FirstOrDefault(x => x.IddoAn == IdDoAn);

            if (existingKhuyenMai != null)
            {
                if (HinhAnh != null && HinhAnh.Length > 0)
                {
                    if (!string.IsNullOrEmpty(existingKhuyenMai.HinhAnh) && existingKhuyenMai.HinhAnh != DoAn.HinhAnh)
                    {
                        var oldFilePath = Path.Combine(hostingEnvironment.WebRootPath, "LayoutDatVe", "img","quangcao", existingKhuyenMai.HinhAnh);
                        if (oldFilePath != null)
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                        else
                        {
                            Console.WriteLine("Old file does not exist: " + oldFilePath);
                        }
                    }

                    string webRootPath = hostingEnvironment.WebRootPath;
                    string uploadFolder = Path.Combine(webRootPath, "LayoutDatVe", "img", "quangcao");

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

                    DoAn.HinhAnh = fileName;
                }

                DoAn.IdRapChieuPhim = (int)IdRapPhim;

                db.Attach(existingKhuyenMai);
                db.Entry(existingKhuyenMai).CurrentValues.SetValues(DoAn);

                db.SaveChanges();
            }
            return RedirectToAction("DanhSachThucAn", "TinMoiVaUuDai", new { IdUser = IdUser });
        }
        [Route("XoaDoAn")]
        [HttpGet]
        public IActionResult XoaDoAn(int? IdDoAn)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            var IdUser = HttpContext.Session.GetString("Iduser");
            ViewBag.IdUser = IdUser;
            ViewBag.Username = HttpContext.Session.GetString("Username");
            var KhuyenMai = db.TDoAns.Where(x => x.IddoAn == IdDoAn).ToList();
            db.Remove(db.TDoAns.Find(IdDoAn));
            db.SaveChanges();
            return RedirectToAction("DanhSachThucAn", "TinMoivaUuDai");
        }

        [Route("DanhSachThucUong")]
        [HttpGet]
        public IActionResult DanhSachThucUong(int? page, int? IdRapPhim)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            var a = HttpContext.Session.GetString("Iduser");
            ViewBag.IdUser = a;
            ViewBag.Username = HttpContext.Session.GetString("Username");
            var idrapchieu = db.TRapChieuPhims.FirstOrDefault(x => x.Iduser == Convert.ToInt32(a));
            if (idrapchieu != null)
            {
                IdRapPhim = idrapchieu.IdrapChieuPhim;
                ViewBag.IdRapPhim = IdRapPhim;
            }
            var ThucUong = db.TDoUongs.Where(x => x.IdRapChieuPhim == IdRapPhim);
            int pageSize = 20;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            PagedList<TDoUong> lst = new PagedList<TDoUong>(ThucUong, pageNumber, pageSize);
            return View(lst);
        }

        [Route("ThemDoUong")]
        [HttpGet]
        public IActionResult ThemDoUong(int? IdRapPhim)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            var a = HttpContext.Session.GetString("Iduser");
            ViewBag.IdUser = a;
            ViewBag.Username = HttpContext.Session.GetString("Username");
            var idrapchieu = db.TRapChieuPhims.FirstOrDefault(x => x.Iduser == Convert.ToInt32(a));
            if (idrapchieu != null)
            {
                IdRapPhim = idrapchieu.IdrapChieuPhim;
                ViewBag.IdRapPhim = IdRapPhim;
            }
            return View();
        }
        
        [Route("ThemDoUong")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemDoUong(TDoUong doAn, int? IdRapPhim, IFormFile HinhAnh, [FromServices] IWebHostEnvironment hostingEnvironment)
        {
            var existingKhuyenMai = db.TDoAns.Find(doAn.IddoUong);
            if (existingKhuyenMai != null && !string.IsNullOrEmpty(existingKhuyenMai.HinhAnh))
            {
                // Delete the old image
                string oldFilePath = Path.Combine(hostingEnvironment.WebRootPath, "LayoutDatVe", "img", "quangcao", existingKhuyenMai.HinhAnh);

                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            if (HinhAnh != null && HinhAnh.Length > 0)
            {
                // Save the new image
                string webRootPath = hostingEnvironment.WebRootPath;
                string uploadFolder = Path.Combine(webRootPath, "LayoutDatVe", "img", "quangcao");

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

                doAn.HinhAnh = fileName;
            }

            doAn.IdRapChieuPhim = (int)IdRapPhim;
            db.TDoUongs.Add(doAn);
            db.SaveChanges();

            return RedirectToAction("DanhSachThucAn", "TinMoiVaUuDai");
        }

        [Route("SuaDoUong")]
        [HttpGet]
        public IActionResult SuaDoUong(int? IdRapPhim, int? IdDoUong)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            var a = HttpContext.Session.GetString("Iduser");
            ViewBag.IdUser = a;
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.IdDoUong = IdDoUong;
            var idrapchieu = db.TRapChieuPhims.FirstOrDefault(x => x.Iduser == Convert.ToInt32(a));
            if (idrapchieu != null)
            {
                IdRapPhim = idrapchieu.IdrapChieuPhim;
                ViewBag.IdRapPhim = IdRapPhim;
            }
            ViewBag.IdDoAn = IdDoUong;
            var doAn = db.TDoUongs.Find(IdDoUong);
            return View(doAn);
        }

        [Route("SuaDoUong")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaDoUong(TDoAn DoAn, IFormFile HinhAnh, int? IdUser, int? IdDoUong, [FromServices] IWebHostEnvironment hostingEnvironment, int? IdRapPhim)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            var a = HttpContext.Session.GetString("Iduser");
            ViewBag.IdUser = a;
            ViewBag.Username = HttpContext.Session.GetString("Username");
            var idrapchieu = db.TRapChieuPhims.FirstOrDefault(x => x.Iduser == Convert.ToInt32(a));
            if (idrapchieu != null)
            {
                IdRapPhim = idrapchieu.IdrapChieuPhim;
            }

            var existingKhuyenMai = db.TDoUongs
                .AsNoTracking()
                .FirstOrDefault(x => x.IddoUong == IdDoUong);

            if (existingKhuyenMai != null)
            {
                if (HinhAnh != null && HinhAnh.Length > 0)
                {
                    if (!string.IsNullOrEmpty(existingKhuyenMai.HinhAnh) && existingKhuyenMai.HinhAnh != DoAn.HinhAnh)
                    {
                        var oldFilePath = Path.Combine(hostingEnvironment.WebRootPath, "LayoutDatVe", "img", "quangcao", existingKhuyenMai.HinhAnh);
                        if (oldFilePath != null)
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                        else
                        {
                            Console.WriteLine("Old file does not exist: " + oldFilePath);
                        }
                    }

                    string webRootPath = hostingEnvironment.WebRootPath;
                    string uploadFolder = Path.Combine(webRootPath, "LayoutDatVe", "img", "quangcao");

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

                    DoAn.HinhAnh = fileName;
                }

                DoAn.IdRapChieuPhim = (int)IdRapPhim;

                db.Attach(existingKhuyenMai);
                db.Entry(existingKhuyenMai).CurrentValues.SetValues(DoAn);

                db.SaveChanges();
            }
            return RedirectToAction("DanhSachThucAn", "TinMoiVaUuDai", new { IdUser = IdUser });
        }
        [Route("XoaDoUong")]
        [HttpGet]
        public IActionResult XoaDoUong(int? IdDoUong)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            var IdUser = HttpContext.Session.GetString("Iduser");
            ViewBag.IdUser = IdUser;
            ViewBag.Username = HttpContext.Session.GetString("Username");
            var KhuyenMai = db.TDoUongs.Where(x => x.IddoUong == IdDoUong).ToList();
            db.Remove(db.TDoUongs.Find(IdDoUong));
            db.SaveChanges();
            return RedirectToAction("DanhSachThucAn", "TinMoivaUuDai");
        }

        [Route("DanhSachComBo")]
        [HttpGet]
        public IActionResult DanhSachComBo(int? page, int? IdRapPhim)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            var a = HttpContext.Session.GetString("Iduser");
            ViewBag.IdUser = a;
            ViewBag.Username = HttpContext.Session.GetString("Username");
            var idrapchieu = db.TRapChieuPhims.FirstOrDefault(x => x.Iduser == Convert.ToInt32(a));
            if (idrapchieu != null)
            {
                IdRapPhim = idrapchieu.IdrapChieuPhim;
                ViewBag.IdRapPhim = IdRapPhim;
            }
            var ThucUong = db.TComBos.Where(x => x.IdRapChieuPhim == IdRapPhim);
            int pageSize = 20;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            PagedList<TComBo> lst = new PagedList<TComBo>(ThucUong, pageNumber, pageSize);
            return View(lst);
        }

        [Route("ThemComBo")]
        [HttpGet]
        public IActionResult ThemComBo(int? IdRapPhim)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            var a = HttpContext.Session.GetString("Iduser");
            ViewBag.IdUser = a;
            ViewBag.Username = HttpContext.Session.GetString("Username");
            var idrapchieu = db.TRapChieuPhims.FirstOrDefault(x => x.Iduser == Convert.ToInt32(a));
            if (idrapchieu != null)
            {
                IdRapPhim = idrapchieu.IdrapChieuPhim;
                ViewBag.IdRapPhim = IdRapPhim;
            }
            return View();
        }

        [Route("ThemComBo")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemComBo(TComBo doAn, int? IdRapPhim, IFormFile HinhAnh, [FromServices] IWebHostEnvironment hostingEnvironment)
        {
            var existingKhuyenMai = db.TComBos.Find(doAn.IdcomBo);
            if (existingKhuyenMai != null && !string.IsNullOrEmpty(existingKhuyenMai.HinhAnh))
            {
                // Delete the old image
                string oldFilePath = Path.Combine(hostingEnvironment.WebRootPath, "LayoutDatVe", "img", "quangcao", existingKhuyenMai.HinhAnh);

                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            if (HinhAnh != null && HinhAnh.Length > 0)
            {
                // Save the new image
                string webRootPath = hostingEnvironment.WebRootPath;
                string uploadFolder = Path.Combine(webRootPath, "LayoutDatVe", "img", "quangcao");

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

                doAn.HinhAnh = fileName;
            }

            doAn.IdRapChieuPhim = (int)IdRapPhim;
            db.TComBos.Add(doAn);
            db.SaveChanges();

            return RedirectToAction("DanhSachComBo", "TinMoiVaUuDai");
        }

        [Route("SuaComBo")]
        [HttpGet]
        public IActionResult SuaComBo(int? IdRapPhim, int? IdComBo)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            var a = HttpContext.Session.GetString("Iduser");
            ViewBag.IdUser = a;
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.IdDoUong = IdComBo;
            var idrapchieu = db.TRapChieuPhims.FirstOrDefault(x => x.Iduser == Convert.ToInt32(a));
            if (idrapchieu != null)
            {
                IdRapPhim = idrapchieu.IdrapChieuPhim;
                ViewBag.IdRapPhim = IdRapPhim;
            }
            ViewBag.IdComBo = IdComBo;
            var doAn = db.TComBos.Find(IdComBo);
            return View(doAn);
        }

        [Route("SuaComBo")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaComBo(TComBo DoAn, IFormFile HinhAnh, int? IdUser, int? IdComBo, [FromServices] IWebHostEnvironment hostingEnvironment, int? IdRapPhim)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            var a = HttpContext.Session.GetString("Iduser");
            ViewBag.IdUser = a;
            ViewBag.Username = HttpContext.Session.GetString("Username");
            var idrapchieu = db.TRapChieuPhims.FirstOrDefault(x => x.Iduser == Convert.ToInt32(a));
            if (idrapchieu != null)
            {
                IdRapPhim = idrapchieu.IdrapChieuPhim;
            }

            var existingKhuyenMai = db.TComBos
                .AsNoTracking()
                .FirstOrDefault(x => x.IdcomBo == IdComBo);

            if (existingKhuyenMai != null)
            {
                if (HinhAnh != null && HinhAnh.Length > 0)
                {
                    if (!string.IsNullOrEmpty(existingKhuyenMai.HinhAnh) && existingKhuyenMai.HinhAnh != DoAn.HinhAnh)
                    {
                        var oldFilePath = Path.Combine(hostingEnvironment.WebRootPath, "LayoutDatVe", "img", "quangcao", existingKhuyenMai.HinhAnh);
                        if (oldFilePath != null)
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                        else
                        {
                            Console.WriteLine("Old file does not exist: " + oldFilePath);
                        }
                    }

                    string webRootPath = hostingEnvironment.WebRootPath;
                    string uploadFolder = Path.Combine(webRootPath, "LayoutDatVe", "img", "quangcao");

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

                    DoAn.HinhAnh = fileName;
                }

                DoAn.IdRapChieuPhim = (int)IdRapPhim;

                db.Attach(existingKhuyenMai);
                db.Entry(existingKhuyenMai).CurrentValues.SetValues(DoAn);

                db.SaveChanges();
            }
            return RedirectToAction("DanhSachComBo", "TinMoiVaUuDai", new { IdUser = IdUser });
        }
        [Route("XoaComBo")]
        [HttpGet]
        public IActionResult XoaComBo(int? IdComBo)
        {
            ViewBag.type = HttpContext.Session.GetString("TypeUser");
            var IdUser = HttpContext.Session.GetString("Iduser");
            ViewBag.IdUser = IdUser;
            ViewBag.Username = HttpContext.Session.GetString("Username");
            var KhuyenMai = db.TComBos.Where(x => x.IdcomBo == IdComBo).ToList();
            db.Remove(db.TComBos.Find(IdComBo));
            db.SaveChanges();
            return RedirectToAction("DanhSachThucAn", "TinMoivaUuDai");
        }
    }
}