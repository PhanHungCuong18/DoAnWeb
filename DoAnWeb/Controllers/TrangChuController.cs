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
        private List<SanPhan> Layhoamoi   (int count)
        {
            return data.SanPhans.OrderByDescending(a => a.NgayUpdate).Take(count).ToList();
        }
        

        // GET: TrangChu
        public ActionResult Index()
        {
            var hoamoi = Layhoamoi(6);
            return View(hoamoi);
        }
        public ActionResult Loai()
        {
            var loai = from l in data.LoaiSPs select l;
            return PartialView(loai);

        }
        public ActionResult MauSac()
        {
            var mausac = from ms in data.MauSacs select ms;
            return PartialView(mausac);
        }
        public ActionResult ChuDe()
        {
            var chude = from cd in data.ChuDes select cd;
            return PartialView(chude);
        }
        public ActionResult HoatheoCD(int id)
        {
            var hoa = from h in data.SanPhans where h.MaCD == id select h;
            return View(hoa);
        }
        public ActionResult HoatheoLoai(int id)
        {
            var hoa = from h in data.SanPhans where h.MaLoai == id select h;
            return View(hoa);
        }
        public ActionResult HoatheoMS(int id)
        {
            var hoa = from h in data.SanPhans where h.MaMau == id select h;
            return View(hoa);
        }
    }
}