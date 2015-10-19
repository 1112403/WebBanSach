using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WedSiteBanSach.Models;

namespace WedSiteBanSach.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        //
        // GET: /QuanLySanPham/
        public ActionResult Index()
        {
            List<Sach> lstSach = new List<Sach>();
            lstSach = db.Saches.ToList();
            return View(lstSach);
        }
	}
}