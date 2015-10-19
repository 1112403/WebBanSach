using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WedSiteBanSach.Models;
namespace WedSiteBanSach.Controllers
{
    public class NhaXuatBanController : Controller
    {
        //
        // GET: /NhaXuatBan/
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NhaXuatBanPartial()
        {
            var lstNXB = db.NhaXuatBans.Take(5).ToList();
            return PartialView(lstNXB);
        }
	}
}