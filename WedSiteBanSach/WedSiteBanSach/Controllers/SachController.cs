using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WedSiteBanSach.Models;

namespace WedSiteBanSach.Controllers
{
    public class SachController : Controller
    {
        //
        // GET: /Sach/
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult SachMoiPartial()
        {
            var lstSachMoi = db.Saches.Take(3).ToList();
            return PartialView(lstSachMoi);
        }
        //Xem chi tiết sách
        public ViewResult xemChiTiet(int maSach)
        {
            Sach sach = db.Saches.SingleOrDefault(n => n.MaSach == maSach);
            if(sach == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            ViewBag.TenChuDe = db.ChuDes.SingleOrDefault(n => n.MaChuDe == sach.MaChuDe).TenChuDe;
            ViewBag.NXB = db.NhaXuatBans.SingleOrDefault(n => n.MaNXB == sach.MaNXB).TenNXB;

            return View(sach);
        }
	}
}