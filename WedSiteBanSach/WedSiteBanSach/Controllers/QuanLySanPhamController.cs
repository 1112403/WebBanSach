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
	}
}