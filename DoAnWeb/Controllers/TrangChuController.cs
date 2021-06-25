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
            var hoamoi = Layhoamoi(6);
           
            return View(hoamoi);
           
        }
 

        public ActionResult ChuDe()
        {
            var chude = from ChuDe in data.ChuDes select ChuDe;
            return PartialView(chude);
        }
        public ActionResult MauSac()
        {
            var mausac = from MauSac in data.MauSacs select MauSac;
            return PartialView(mausac);
        }
        public ActionResult Loai()
        {
            var loai = from LoaiSP in data.LoaiSPs select LoaiSP;
            return PartialView(loai);
        }
        public ActionResult HoatheoLoai(int id)
        {
            var hoa = from h in data.SanPhans where h.MaLoai==id  select h;
            return PartialView(hoa);
        }
        public ActionResult HoatheoCD(int id)
        {
            var hoa = from h in data.SanPhans where h.MaCD == id select h;
            return PartialView(hoa);
        }
        public ActionResult HoatheoMS(int id)
        {
            var hoa = from h in data.SanPhans where h.MaMau ==id  select h;
            return PartialView(hoa);
        }
    }
}