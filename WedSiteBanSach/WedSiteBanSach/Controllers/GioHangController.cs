using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WedSiteBanSach.Models;

namespace WedSiteBanSach.Controllers
{
    public class GioHangController : Controller
    {
        //
        // GET: /GioHang/
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        #region Giỏ hàng
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if(lstGioHang == null)
            {
                lstGioHang = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        }

        //Thêm 1 sán phẩm vào giỏ hàng
        public ActionResult ThemGioHang(int iMaSach, string strURL)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSach);

            if(sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            List<GioHang> lstGioHang = LayGioHang();

            GioHang sanpham = lstGioHang.Find(n => n.iMaSach == iMaSach);
            if (sanpham == null)
            {
                sanpham = new GioHang(iMaSach);
                lstGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoLuong += 1;
                return Redirect(strURL);
            }
        }

        //Update giỏ hàng
        public ActionResult CapNhatGioHang(int iMaSach, FormCollection f)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSach);

            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            List<GioHang> lstGioHang = LayGioHang();

            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSach);
            if(sanpham != null)
            {
                sanpham.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }

            return RedirectToAction("SuaGioHang");
        }

        //Xóa giỏ hàng
        public ActionResult XoaGioHang(int iMaSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == iMaSach);

            if (sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> lstGioHang = LayGioHang();

            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.iMaSach == iMaSach);

            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n=> n.iMaSach == iMaSach);
            }

            if (lstGioHang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("SuaGioHang");
        }


        public ActionResult GioHang()
        {
            if(Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }

        //Tổng số lượng giỏ hàng
        private int TongSoLuong()
        {
            int TongSoLuong = 0;

            List<GioHang> lstGioHang = LayGioHang();

            if(lstGioHang != null)
            {
                TongSoLuong = lstGioHang.Sum(n => n.iSoLuong);
            }

            return TongSoLuong;
        }

        //Tổng tiền giỏ hàng
        private double TongTien()
        {
            double TongTien = 0;

            List<GioHang> lstGioHang = LayGioHang();

            if (lstGioHang != null)
            {
                TongTien = lstGioHang.Sum(n => n.dThanhTien);
            }

            return TongTien;
        }

        public ActionResult GioHangPartial()
        {
            if(TongSoLuong() == 0)
            {
                return null;
            }
            @ViewBag.TongSoLuong = TongSoLuong();
            return PartialView();
        }

        //Sửa giỏ hàng
        public ActionResult SuaGioHang()
        {
            if(Session["GioHang"] == null)
            {
                RedirectToAction("Index", "Home");
            }
            List<GioHang> lstGioHang = LayGioHang();
            return View(lstGioHang);
        }
        #endregion

        #region Đặt hàng
        public ActionResult DatHang()
        {
            if(Session["KhachHang"] == null)
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }

            //Add value to DonHang
            DonHang dh = new DonHang();
            KhachHang kh = Session["KhachHang"] as  KhachHang;
            dh.MaKH = kh.MaKH;
            dh.NgayDat = System.DateTime.Now;
            db.DonHangs.Add(dh);
            db.SaveChanges();

            //Add value to ChiTietDonDatHang
            List<GioHang> lstGioHang = LayGioHang();
            ChiTietDonHang CTDH = new ChiTietDonHang();
            foreach(var item in lstGioHang)
            {
                CTDH.MaDonHang = dh.MaDonHang;
                CTDH.MaSach = item.iMaSach;
                CTDH.SoLuong = item.iSoLuong;
                CTDH.DonGia = (decimal)item.dDonGia;
                db.ChiTietDonHangs.Add(CTDH);
            }
            db.SaveChanges();

            return RedirectToAction("Index","Home");
        }
        #endregion
    }
}