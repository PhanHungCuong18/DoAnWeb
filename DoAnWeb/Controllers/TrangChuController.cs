using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;

using PagedList;
using PagedList.Mvc;

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
        public ActionResult Index(int ?page )
        {
            int pageSize = 4;
            int pageNum = (page ?? 1);
            var hoamoi = Layhoamoi(15);
            return View(hoamoi.ToPagedList(pageNum,pageSize));
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
        public ActionResult ChiTietHoa(int id)
        {
            var hoa = from h in data.SanPhans
                      where h.MaSP == id
                      select h;
            return View(hoa.Single());
        }
    }
}