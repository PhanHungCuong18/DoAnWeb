using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;

namespace DoAnWeb.Controllers
{
    public class TrangChuController : Controller
    {
        dbFSDataContext data = new dbFSDataContext();
        private List<SanPhan> Layhoamoi(int count)
        {
            return data.SanPhans.OrderByDescending(a => a.NgayUpdate).Take(count).ToList();
        }
        

        // GET: TrangChu
        public ActionResult Index()
        {
        }
    }
}