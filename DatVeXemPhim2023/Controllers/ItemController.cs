using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using DatVeXemPhim2023.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using X.PagedList;
using DatVeXemPhim2023.ViewModels;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Http;
using Humanizer;

namespace DatVeXemPhim2023.Controllers
{
    public class ItemController : Controller
    {
        QldatVeXemPhimContext db = new QldatVeXemPhimContext();
        public IActionResult UuDaiVaKhuyenMai(int? page)
        {
            var a = HttpContext.Session.GetString("Username");

            if (a != null)
            {
                ViewBag.Login = "ĐĂNG XUẤT";
            }
            else
            {
                ViewBag.Login = "ĐĂNG NHẬP";
            }

            int pageSize = 9;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var listKhuyenMai = db.TThongTinKhuyenMais.AsNoTracking().OrderBy(x => x.TimeBegin);
            PagedList<TThongTinKhuyenMai> lst = new PagedList<TThongTinKhuyenMai>(listKhuyenMai, pageNumber, pageSize);

            return View(lst);
        }

        public IActionResult ChiTietkhuyenMai(int? IdKhuyenMai)
        {
            var khuyenmai = db.TThongTinKhuyenMais.FirstOrDefault(x => x.IdkhuyenMai == IdKhuyenMai);
            return View(khuyenmai);
        }

        public IActionResult HoaDon() {
            var a = HttpContext.Session.GetString("Username");

            if (a != null)
            {
                ViewBag.Login = "ĐĂNG XUẤT";

                var IdThanhToan = (from tk in db.TTaiKhoans
                                   join tt in db.TThanhToans on tk.Iduser equals tt.IdUser
                                   where tk.Username == a
                                   orderby tt.IdthanhToan descending
                                   select tt.IdthanhToan).FirstOrDefault();

                int id = Convert.ToInt32(IdThanhToan);
                ViewBag.Id = id;

                var tenRapChieu = (from tk in db.TTaiKhoans
                                   join tt in db.TThanhToans on tk.Iduser equals tt.IdUser
                                   join hd in db.THoaDons on tt.IdhoaDon equals hd.IdHoaDon
                                   join sc in db.TSuatChieus on hd.IdSuatChieu equals sc.IdsuatChieu
                                   join rc in db.TRapChieuPhims on sc.IdrapChieuPhim equals rc.IdrapChieuPhim
                                   where tk.Username == a
                                   orderby rc.IdrapChieuPhim descending
                                   select rc.TenRap).FirstOrDefault();
                if (tenRapChieu != null)
                {
                    ViewBag.TenRapChieu = tenRapChieu;
                }

                var TenPhongChieu = (from tk in db.TTaiKhoans
                                     join tt in db.TThanhToans on tk.Iduser equals tt.IdUser
                                     join hd in db.THoaDons on tt.IdhoaDon equals hd.IdHoaDon
                                     join sc in db.TSuatChieus on hd.IdSuatChieu equals sc.IdsuatChieu
                                     join rc in db.TRapChieuPhims on sc.IdrapChieuPhim equals rc.IdrapChieuPhim
                                     where tk.Username == a
                                     orderby rc.IdrapChieuPhim descending
                                     select sc.TenPhongChieu).FirstOrDefault();
                if (TenPhongChieu != null)
                {
                    ViewBag.TenPhongChieu = TenPhongChieu;
                }

                var MaHoaDon = (from tk in db.TTaiKhoans
                                join tt in db.TThanhToans on tk.Iduser equals tt.IdUser
                                join hd in db.THoaDons on tt.IdhoaDon equals hd.IdHoaDon
                                join sc in db.TSuatChieus on hd.IdSuatChieu equals sc.IdsuatChieu
                                join rc in db.TRapChieuPhims on sc.IdrapChieuPhim equals rc.IdrapChieuPhim
                                where tk.Username == a
                                orderby rc.IdrapChieuPhim descending
                                select tt.IdthanhToan).FirstOrDefault();
                if (MaHoaDon != null)
                {
                    ViewBag.MaHoaDon = MaHoaDon;
                }

                var NgayThanhToan = (from tk in db.TTaiKhoans
                                     join tt in db.TThanhToans on tk.Iduser equals tt.IdUser
                                     join hd in db.THoaDons on tt.IdhoaDon equals hd.IdHoaDon
                                     join sc in db.TSuatChieus on hd.IdSuatChieu equals sc.IdsuatChieu
                                     join rc in db.TRapChieuPhims on sc.IdrapChieuPhim equals rc.IdrapChieuPhim
                                     where tk.Username == a
                                     orderby rc.IdrapChieuPhim descending
                                     select tt.NgayThanhToan).FirstOrDefault();
                if (NgayThanhToan != null)
                {
                    ViewBag.NgayThanhToan = NgayThanhToan;
                }

                var item = (from tk in db.TTaiKhoans
                            join tt in db.TThanhToans on tk.Iduser equals tt.IdUser
                            join hd in db.THoaDons on tt.IdhoaDon equals hd.IdHoaDon
                            join sc in db.TSuatChieus on hd.IdSuatChieu equals sc.IdsuatChieu
                            join rc in db.TRapChieuPhims on sc.IdrapChieuPhim equals rc.IdrapChieuPhim
                            where tk.Username == a && tt.IdthanhToan == id
                            orderby rc.IdrapChieuPhim descending
                            select hd.NameFood).ToList();
                if (item != null)
                {
                    ViewBag.Item = item;
                }

                var SuatChieu = (from tk in db.TTaiKhoans
                                 join tt in db.TThanhToans on tk.Iduser equals tt.IdUser
                                 join hd in db.THoaDons on tt.IdhoaDon equals hd.IdHoaDon
                                 join sc in db.TSuatChieus on hd.IdSuatChieu equals sc.IdsuatChieu
                                 join rc in db.TRapChieuPhims on sc.IdrapChieuPhim equals rc.IdrapChieuPhim
                                 where tk.Username == a && tt.IdthanhToan == id
                                 orderby rc.IdrapChieuPhim descending
                                 select sc.TgbatDau).FirstOrDefault();
                if (SuatChieu != null)
                {
                    DateTime suatChieuDateTime = Convert.ToDateTime(SuatChieu);
                    string gioPhut = suatChieuDateTime.ToString("HH:mm");
                    ViewBag.SuatChieu = gioPhut;
                }

                var danhSachGiaGhe = (from tk in db.TTaiKhoans
                                      join tt in db.TThanhToans on tk.Iduser equals tt.IdUser
                                      join hd in db.THoaDons on tt.IdhoaDon equals hd.IdHoaDon
                                      join sc in db.TSuatChieus on hd.IdSuatChieu equals sc.IdsuatChieu
                                      join rc in db.TRapChieuPhims on sc.IdrapChieuPhim equals rc.IdrapChieuPhim
                                      where tk.Username == "c" && tt.IdthanhToan == 159
                                      orderby rc.IdrapChieuPhim descending
                                      select hd.GiaGhe).ToList();
                var danhSachGiaFood = (from tk in db.TTaiKhoans
                                      join tt in db.TThanhToans on tk.Iduser equals tt.IdUser
                                      join hd in db.THoaDons on tt.IdhoaDon equals hd.IdHoaDon
                                      join sc in db.TSuatChieus on hd.IdSuatChieu equals sc.IdsuatChieu
                                      join rc in db.TRapChieuPhims on sc.IdrapChieuPhim equals rc.IdrapChieuPhim
                                      where tk.Username == "c" && tt.IdthanhToan == 159
                                      orderby rc.IdrapChieuPhim descending
                                      select hd.GiaFood).ToList();
                if (danhSachGiaGhe != null)
                {
                    double? tongGhe = danhSachGiaGhe.Sum() ?? 0;
                    double? tongFood = danhSachGiaFood.Sum() ?? 0;
                    double? tong = tongGhe + tongFood;
                    ViewBag.Tong = tong;
                }

                var TenPhim = (from tk in db.TTaiKhoans
                               join tt in db.TThanhToans on tk.Iduser equals tt.IdUser
                               join hd in db.THoaDons on tt.IdhoaDon equals hd.IdHoaDon
                               join sc in db.TSuatChieus on hd.IdSuatChieu equals sc.IdsuatChieu
                               join rc in db.TRapChieuPhims on sc.IdrapChieuPhim equals rc.IdrapChieuPhim
                               join p in db.TPhims on hd.IdPhim equals p.Idphim
                               where tk.Username == a && tt.IdthanhToan == id
                               orderby rc.IdrapChieuPhim descending
                               select p.TenPhim).FirstOrDefault();
                if (TenPhim != null)
                {
                    ViewBag.IdPhim = TenPhim;
                }

                var danhSachTenGhe = (from tk in db.TTaiKhoans
                                      join tt in db.TThanhToans on tk.Iduser equals tt.IdUser
                                      join hd in db.THoaDons on tt.IdhoaDon equals hd.IdHoaDon
                                      join sc in db.TSuatChieus on hd.IdSuatChieu equals sc.IdsuatChieu
                                      join rc in db.TRapChieuPhims on sc.IdrapChieuPhim equals rc.IdrapChieuPhim
                                      where tk.Username == "c" && tt.IdthanhToan == 159
                                      orderby rc.IdrapChieuPhim descending
                                      select hd.NameGhe).ToList();
                if(danhSachTenGhe != null)
                {
                    ViewBag.TenGhe = danhSachTenGhe;
                }

                return View("HoaDon");
            }
            else
            {
                ViewBag.Login = "ĐĂNG NHẬP";
                return RedirectToAction("Login", "Access");
            }
        }

        public IActionResult ChiTietHoaDon(int idThanhToan)
        {
            var a = HttpContext.Session.GetString("Username");

            if (a != null)
            {
                ViewBag.Login = "ĐĂNG XUẤT";
                var thanhtoan = db.TThanhToans.FirstOrDefault(tt => tt.IdthanhToan == idThanhToan);
                var hoaDon = db.THoaDons.Where(hd => hd.IdHoaDon == thanhtoan!.IdhoaDon).ToList();
                var phim = db.TPhims.FirstOrDefault(x => x.Idphim == hoaDon[0]!.IdPhim);

                var tenRapChieu = (from tk in db.TTaiKhoans
                                   join tt in db.TThanhToans on tk.Iduser equals tt.IdUser
                                   join hd in db.THoaDons on tt.IdhoaDon equals hd.IdHoaDon
                                   join sc in db.TSuatChieus on hd.IdSuatChieu equals sc.IdsuatChieu
                                   join rc in db.TRapChieuPhims on sc.IdrapChieuPhim equals rc.IdrapChieuPhim
                                   where tk.Username == a
                                   orderby rc.IdrapChieuPhim descending
                                   select rc.TenRap).FirstOrDefault();

                var TenPhongChieu = (from tk in db.TTaiKhoans
                                   join tt in db.TThanhToans on tk.Iduser equals tt.IdUser
                                   join hd in db.THoaDons on tt.IdhoaDon equals hd.IdHoaDon
                                   join sc in db.TSuatChieus on hd.IdSuatChieu equals sc.IdsuatChieu
                                   join rc in db.TRapChieuPhims on sc.IdrapChieuPhim equals rc.IdrapChieuPhim
                                   where tk.Username == a
                                   orderby rc.IdrapChieuPhim descending
                                   select sc.TenPhongChieu).FirstOrDefault();

                var IdThanhToan = (from tk in db.TTaiKhoans
                                   join tt in db.TThanhToans on tk.Iduser equals tt.IdUser
                                   where tk.Username == a
                                   orderby tt.IdthanhToan descending
                                   select tt.IdthanhToan).FirstOrDefault();

                int id = Convert.ToInt32(IdThanhToan);
                ViewBag.Id = id;
                var item = (from tk in db.TTaiKhoans
                             join tt in db.TThanhToans on tk.Iduser equals tt.IdUser
                             join hd in db.THoaDons on tt.IdhoaDon equals hd.IdHoaDon
                             join sc in db.TSuatChieus on hd.IdSuatChieu equals sc.IdsuatChieu
                             join rc in db.TRapChieuPhims on sc.IdrapChieuPhim equals rc.IdrapChieuPhim
                             where tk.Username == a && tt.IdthanhToan == id
                             orderby rc.IdrapChieuPhim descending
                             select hd.NameFood).ToList();

                if (item != null)
                {
                    ViewBag.Item = item;
                }

                if(TenPhongChieu != null)
                {
                    ViewBag.TenPhongChieu = TenPhongChieu;
                }
                if(tenRapChieu != null)
                {
                    ViewBag.TenRapChieu = tenRapChieu;
                }

                if (thanhtoan != null)
                {
                    ViewBag.thanhToan = thanhtoan;
                }
                if (hoaDon != null)
                {
                    double tongTien = 0;
                    foreach(var hd in hoaDon)
                    {
                        if(hd.GiaGhe != null)
                        {
                            tongTien += Convert.ToDouble(hd.GiaGhe);
                        }
                        if(hd.GiaFood != null)
                        {
                            tongTien += Convert.ToDouble(hd.GiaFood);
                        }
                        ViewBag.tongTien = tongTien;
                    }
                    ViewBag.hoaDon = hoaDon;
                }
                if (phim != null)
                {
                    ViewBag.phim = phim;
                }
                return View();
            }
            else
            {
                ViewBag.Login = "ĐĂNG NHẬP";
                return RedirectToAction("Login", "Access");
            }
        }
    }
}
