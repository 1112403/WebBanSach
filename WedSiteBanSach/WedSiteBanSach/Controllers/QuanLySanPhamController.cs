using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WedSiteBanSach.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;

namespace WedSiteBanSach.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        //
        // GET: /QuanLySanPham/
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;


            PagedList.IPagedList<Sach> lstSach = db.Saches.OrderBy(n=>n.TenSach).ToPagedList(pageNumber,pageSize);
            return View(lstSach);
        }

        //Add a new book
        [HttpGet]
        public ActionResult ThemMoi()
        {
            //ViewBag.NXB = db.NhaXuatBans.ToList();
            ViewBag.MaChuDe = new SelectList(db.ChuDes.OrderBy(n => n.TenChuDe).ToList(), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.OrderBy(n => n.TenNXB).ToList(), "MaNXB", "TenNXB");
            return View();
        }

        [HttpPost]
        public ActionResult ThemMoi(Sach sach, HttpPostedFileBase fileUpload)
        {
            //Save the name of file upload
            ViewBag.MaChuDe = new SelectList(db.ChuDes.OrderBy(n => n.TenChuDe).ToList(), "MaChuDe", "TenChuDe");
            ViewBag.MaNXB = new SelectList(db.NhaXuatBans.OrderBy(n => n.TenNXB).ToList(), "MaNXB", "TenNXB");

            if (fileUpload == null)
            {
                ViewBag.ThongBao = "Chọn hình ảnh";
                return View();
            }

            if(ModelState.IsValid)
            {
                var fileName = Path.GetFileName(fileUpload.FileName);
                //Save the path of the file upload
                var filePath = Path.Combine(Server.MapPath("~/HinhAnhSP"), fileName);
                //Is the file upload exist ?
                if (System.IO.File.Exists(filePath))
                {
                    ViewBag.ThongBao = "Hinh anh da ton tai";
                }
                else
                {
                    fileUpload.SaveAs(filePath);
                }
                sach.AnhBia = fileName;
                db.Saches.Add(sach);
                db.SaveChanges();
            }

            return View();
        }
	}
}