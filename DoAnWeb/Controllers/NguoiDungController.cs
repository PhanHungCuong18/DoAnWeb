using DoAnWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class NguoiDungController : Controller
    {
        dbFSDataContext db = new dbFSDataContext();
        // GET: NguoiDung
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        
        public ActionResult DangKy(FormCollection collection,KhanhHang kh)
        {
            var hoten = collection["HotenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            var matkhaunhaplai = collection["Matkhaunhaplai"];
            var diachi = collection["Diachi"];
            var email = collection["Email"];
            var dienthoai = collection["Dienthoai"];
           
            if(String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên khách hàng không được để trống ";
            }
            else if(String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Phải nhập tên đăng nhập ";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Phải nhập mật khẩu  ";
            }
            else if (String.IsNullOrEmpty(matkhaunhaplai))
            {
                ViewData["Loi4"] = "Phải nhập mật khẩu nhập lại  ";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Email không được để trống  ";

            }
            else if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi6"] = "Nhập số điện thoại  ";
            }
           else
            {
                kh.HoTen = hoten;
                kh.TaiKhoan = tendn;
                kh.Password = matkhau;
                kh.Email = email;
                kh.DiaChi = diachi;
                kh.DT = dienthoai;
                db.KhanhHangs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return RedirectToAction("Dangnhap");
            }
            return this.DangKy();
        }
        [HttpGet]
        public ActionResult Dangnhap()
        {
            return View();
        }
        public ActionResult Dangnhap(FormCollection collection, KhanhHang kh)
        {
            var matkhau = collection["Matkhau"];
            var tendn = collection["TenDN"];
            

            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập ";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu ";
            }
           
            else
            {
                KhanhHang KH = db.KhanhHangs.SingleOrDefault(n => n.TaiKhoan == tendn && n.Password == matkhau);
                if (KH != null)
                {
                    ViewBag.Thongbao = "Chúc mừng đăng nhập thành công ";
                    Session["Taikhoan"] = kh;
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng ";
            }
            return View();
        }
    }
}