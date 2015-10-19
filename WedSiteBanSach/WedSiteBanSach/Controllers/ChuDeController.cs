using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WedSiteBanSach.Models;
using PagedList;
using PagedList.Mvc;

namespace WedSiteBanSach.Controllers
{
    public class ChuDeController : Controller
    {
        //
        // GET: /ChuDe/
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult ChuDePartial()
        {
            var lstChuDe = db.ChuDes.Take(5).OrderBy(n=> n.TenChuDe).ToList();
            return PartialView(lstChuDe);
        }
        public ViewResult sachTheoChuDe(int maChuDe,int? page)
        {
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            

            ChuDe cd = db.ChuDes.SingleOrDefault(n=>n.MaChuDe == maChuDe);
            if(cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            else
            {
                PagedList.IPagedList<Sach> lstSach = db.Saches.Where(n => n.MaChuDe == maChuDe).OrderBy(n => n.GiaBan).ToPagedList(pageNumber,pageSize);
                //PagedList<Sach> lstSach = 
                if(lstSach.Count == 0)
                {
                    ViewBag.Sach = "Không có cuốn sách nào thuộc chủ đề này";
                }
                return View(lstSach);
            }

        }
	}
}