using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
namespace DoAnWeb.Controllers
{
    public class MauSacController : Controller
    {
        dbFSDataContext data = new dbFSDataContext();
  
        public ActionResult Index()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View(data.MauSacs.ToList());
        }
      
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View();
        }
        [HttpPost]
        public ActionResult Create(MauSac mausac)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                data.MauSacs.InsertOnSubmit(mausac);
                data.SubmitChanges();
                return RedirectToAction("Index", "MauSac");
            }
        }
       
        public ActionResult Details (int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var mausac = from ms in data.MauSacs where ms.MaMau == id select ms;
                return View(mausac.SingleOrDefault());
            }
        }
        [HttpGet]
        public ActionResult XoaMau(int id)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                //Lay ra doi tuong sach can xoa theo ma
                MauSac mau = data.MauSacs.SingleOrDefault(n => n.MaMau == id);
                ViewBag.MaMau = mau.MaMau;
                if (mau == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(mau);
            }
        }

        [HttpPost, ActionName("XoaMau")]
        public ActionResult Xacnhanxoa(int id)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                //Lay ra doi tuong sach can xoa theo ma
                MauSac mau = data.MauSacs.SingleOrDefault(n => n.MaMau == id);
                ViewBag.MaMau = mau.MaMau;
                if (mau == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                data.MauSacs.DeleteOnSubmit(mau);
                data.SubmitChanges();
                return RedirectToAction("Index", "MauSac");
            }
        }


        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var mausac = from ms in data.MauSacs where ms.MaMau == id select ms;
                return View(mausac.SingleOrDefault());
            }
        }
        //Do tên Action trùng tên, nên cần tên bí doanh
        [HttpPost, ActionName("Edit")]
        public ActionResult Xacnhansua(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                MauSac mausac = data.MauSacs.SingleOrDefault(n => n.MaMau == id);

                UpdateModel(mausac);
                data.SubmitChanges();
                return RedirectToAction("Index", "MauSac");
            }
        }
    }
}