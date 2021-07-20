using DoAnWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.Net;





namespace DoAnWeb.Controllers
{
    public class AdminController : Controller
    {
        dbFSDataContext db = new dbFSDataContext();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                return View();
            }
        }
        public ActionResult Hoa (int ?page)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else { 
            int pageNumber = (page ?? 1);
            int pageSize = 3;
            
            return View(db.SanPhans.ToList().OrderBy(n => n.MaSP).ToPagedList(pageNumber, pageSize));
            }
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            // Gán các giá trị người dùng nhập liệu cho các biến 
            var tendn = collection["username"];
            var matkhau = collection["password"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                //Gán giá trị cho đối tượng được tạo mới (ad)        

                Admin ad = db.Admins.SingleOrDefault(n => n.UserAdmin == tendn && n.PassAdmin == matkhau);
                if (ad != null)
                {
                    // ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Themsp()
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                
               
                ViewBag.MaCD = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChude");
                ViewBag.MaMau = new SelectList(db.MauSacs.ToList().OrderBy(n => n.TenMau), "MaMau", "TenMau");
                ViewBag.MaLoai = new SelectList(db.LoaiSPs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
                return View();
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemSp(SanPhan sp, HttpPostedFileBase fileUpload)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
               
                ViewBag.MaCD = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChude");
                ViewBag.MaMau = new SelectList(db.MauSacs.ToList().OrderBy(n => n.TenMau), "MaMau", "TenMau");
                ViewBag.MaLoai = new SelectList(db.LoaiSPs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");

                if (fileUpload == null)
                {
                    ViewBag.Thongbao = "Vui lòng chọn ảnh";
                    return View();
                }
                else
                {
                    if (ModelState.IsValid)
                    {

                        var fileName = Path.GetFileName(fileUpload.FileName);

                        var path = Path.Combine(Server.MapPath("~/Images/SPkhuyenmai"), fileName);

                        if (System.IO.File.Exists(path))
                            ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                        else
                        {

                            fileUpload.SaveAs(path);
                        }
                        sp.AnhSP = fileName;

                        db.SanPhans.InsertOnSubmit(sp);
                        db.SubmitChanges();
                    }
                    return RedirectToAction("Hoa","Admin");
                }
            }
        }

        ////Hiển thị sản phẩm
        public ActionResult Chitietsp(int id)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                //Lay ra doi tuong sach theo ma
                SanPhan sach = db.SanPhans.SingleOrDefault(n => n.MaSP == id);
                ViewBag.MaSP = sach.MaSP;
                if (sach == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(sach);
            }
        }

        ////Xóa sản phẩm
        [HttpGet]
        public ActionResult Xoasp(int id)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                //Lay ra doi tuong sach can xoa theo ma
                SanPhan sp = db.SanPhans.SingleOrDefault(n => n.MaSP == id);
                ViewBag.MaSP = sp.MaSP;
                if (sp == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(sp);
            }
        }

        [HttpPost, ActionName("Xoasp")]
        public ActionResult Xacnhanxoa(int id)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                //Lay ra doi tuong sach can xoa theo ma
                SanPhan sp = db.SanPhans.SingleOrDefault(n => n.MaSP == id);
                ViewBag.MaSP = sp.MaSP;
                if (sp == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                db.SanPhans.DeleteOnSubmit(sp);
                db.SubmitChanges();
                return RedirectToAction("Hoa");
            }
        }
       
        [HttpGet]
        public ActionResult Suasp(int id)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {

                SanPhan sp = db.SanPhans.SingleOrDefault(n => n.MaSP == id);
                ViewBag.MaSP = sp.MaSP;
                if (sp == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }


                ViewBag.MaCD = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChude");
                ViewBag.MaMau = new SelectList(db.MauSacs.ToList().OrderBy(n => n.TenMau), "MaMau", "TenMau");
                ViewBag.MaLoai = new SelectList(db.LoaiSPs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
                return View(sp);
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Suasp(int id, HttpPostedFileBase fileUpload)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {

                ViewBag.MaCD = new SelectList(db.ChuDes.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChude");
                ViewBag.MaMau = new SelectList(db.MauSacs.ToList().OrderBy(n => n.TenMau), "MaMau", "TenMau");
                ViewBag.MaLoai = new SelectList(db.LoaiSPs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");

                if (fileUpload == null)
                {
                    ViewBag.Thongbao = "Vui lòng chọn ảnh";
                    return View();
                }
                else
                {
                    SanPhan sp = db.SanPhans.SingleOrDefault(n => n.MaSP == id);
                    if (ModelState.IsValid)
                    {

                        var fileName = Path.GetFileName(fileUpload.FileName);

                        var path = Path.Combine(Server.MapPath("~/Images/SPkhuyenmai"), fileName);

                        if (System.IO.File.Exists(path))
                            ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                        else
                        {

                            fileUpload.SaveAs(path);
                        }

                        sp.AnhSP = fileName;

                        UpdateModel(sp);
                        db.SubmitChanges();
                    }
                    return RedirectToAction("Hoa", "Admin");
                }
            }
        }



    }
}