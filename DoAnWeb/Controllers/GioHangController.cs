using DoAnWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class GioHangController : Controller
    {
        dbFSDataContext db = new dbFSDataContext();
     
        public List<Giohang> Laygiohang()
        {
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang == null)
            {
 
                lstGiohang = new List<Giohang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }
    
        public ActionResult ThemGiohang(int iMasp, string strURL)
        {
     
            List<Giohang> lstGiohang = Laygiohang();
         
            Giohang sanpham = lstGiohang.Find(n => n.iMasp == iMasp);
            if (sanpham == null)
            {
                sanpham = new Giohang(iMasp);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
        }


       
        public ActionResult GioHang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
     
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSoluong);
            }
            return iTongSoLuong;
        }
      
        private double TongTien()
        {
            double iTongTien = 0;
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongTien = lstGiohang.Sum(n => n.dThanhtien);
            }
            return iTongTien;
        }
       
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }
    
        public ActionResult CapnhatGiohang(int iMaSP, FormCollection f)
        {

     
            List<Giohang> lstGiohang = Laygiohang();
        
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iMasp == iMaSP);
        
            if (sanpham != null)
            {
                sanpham.iSoluong = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("Giohang");
        }
     
        public ActionResult XoaGiohang(int iMaSP)
        {
         
            List<Giohang> lstGiohang = Laygiohang();
          
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.iMasp == iMaSP);
       
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.iMasp == iMaSP);
                return RedirectToAction("GioHang");

            }
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "TrangChu");
            }
            return RedirectToAction("GioHang");
        }
 
        public ActionResult XoaTatcaGiohang()
        {
  
            List<Giohang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("Index", "TrangChu");
        }

       
        [HttpGet]
        public ActionResult DatHang()
        {
       
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "NguoiDung");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "TrangChu");
            }

        
            List<Giohang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();

            return View(lstGiohang);
        }
      
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
          
            DonDatHang ddh = new DonDatHang();
            KhanhHang kh = (KhanhHang)Session["Taikhoan"];
            List<Giohang> gh = Laygiohang();
            ddh.MaKH = kh.MaKH;
            ddh.NgayDH = DateTime.Now;
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["Ngaygiao"]);
            ddh.NgayGiao = DateTime.Parse(ngaygiao);
            ddh.Tinhtranggiaohang = false;
            ddh.DaThanhToan = false;
            db.DonDatHangs.InsertOnSubmit(ddh);
            db.SubmitChanges();
            //Them chi tiet don hang            
            foreach (var item in gh)
            {
                CTDatHang ctdh = new CTDatHang();
                ctdh.SoDH = ddh.SoDH;
                ctdh.MaSp = item.iMasp;
                ctdh.Soluong = item.iSoluong;
                ctdh.DonGia = (float)item.dGia;
                db.CTDatHangs.InsertOnSubmit(ctdh);
            }
            db.SubmitChanges();
            Session["Giohang"] = null;
            return RedirectToAction("Xacnhandonhang", "Giohang");
        }
        public ActionResult Xacnhandonhang()
        {
            return View();
        }
    }
}