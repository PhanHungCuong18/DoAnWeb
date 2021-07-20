using DoAnWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class ChuDeController : Controller
    {
        dbFSDataContext db = new dbFSDataContext();
        // GET: ChuDe
        public ActionResult Index()
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                return View(db.ChuDes.ToList());
            }
        }
       
      
        [HttpGet]
        public ActionResult ThemChuDe()
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemChuDe(ChuDe cd, HttpPostedFileBase fileUpload)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                        db.ChuDes.InsertOnSubmit(cd);
                        db.SubmitChanges();            
                    return RedirectToAction("Index", "ChuDe");
                
            }
        }

        ////Hiển thị sản phẩm
        public ActionResult ChitietChude(int id)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                //Lay ra doi tuong sach theo ma
                ChuDe cd = db.ChuDes.SingleOrDefault(n => n.MaCD == id);
                ViewBag.Machude = cd.MaCD;
                if (cd == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(cd);
            }
        }

        ////Xóa sản phẩm
        [HttpGet]
        public ActionResult XoaCD(int id)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                //Lay ra doi tuong sach can xoa theo ma
                ChuDe cd = db.ChuDes.SingleOrDefault(n => n.MaCD == id);
                ViewBag.Machude = cd.MaCD;
                if (cd == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(cd);
            }
        }

        [HttpPost, ActionName("XoaCD")]
        public ActionResult Xacnhanxoa(int id)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                //Lay ra doi tuong sach can xoa theo ma
                ChuDe cd = db.ChuDes.SingleOrDefault(n => n.MaCD == id);
                ViewBag.Machude = cd.MaCD;
                if (cd == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                db.ChuDes.DeleteOnSubmit(cd);
                db.SubmitChanges();
                return RedirectToAction("Index","ChuDe");
            }
        }

        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var chude = from cd in db.ChuDes where cd.MaCD == id select cd;
                return View(chude.SingleOrDefault());
            }
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Xacnhansua(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                ChuDe chude= db.ChuDes.SingleOrDefault(n => n.MaCD == id);

                UpdateModel(chude);
                db.SubmitChanges();
                return RedirectToAction("Index", "ChuDe");
            }
        }
    }
}