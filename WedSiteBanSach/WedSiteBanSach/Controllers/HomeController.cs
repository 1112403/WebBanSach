using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WedSiteBanSach.Models;

namespace WedSiteBanSach.Controllers
{
    public class HomeController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View(db.Saches.Where(n=>n.Moi==1).ToList());
        }
        
	}
}