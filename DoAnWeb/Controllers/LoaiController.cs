using DoAnWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class LoaiController : Controller
    {
        // GET: Loai
       
        dbFSDataContext data = new dbFSDataContext();

        public ActionResult Index()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
                return View(data.LoaiSPs.ToList());
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
        public ActionResult Create(LoaiSP loai)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                data.LoaiSPs.InsertOnSubmit(loai);
                data.SubmitChanges();
                return RedirectToAction("Index", "Loai");
            }
        }

        public ActionResult Details(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var loai = from lsp in data.LoaiSPs where lsp.MaLoai == id select lsp;
                return View(loai.SingleOrDefault());
            }
        }

        [HttpGet]
        public ActionResult XoaLoai(int id)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                //Lay ra doi tuong sach can xoa theo ma
                LoaiSP loai = data.LoaiSPs.SingleOrDefault(n => n.MaLoai == id);
                ViewBag.MaLoai = loai.MaLoai;
                if (loai == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(loai);
            }
        }

        [HttpPost, ActionName("XoaLoai")]
        public ActionResult Xacnhanxoa(int id)
        {
            if (Session["Taikhoanadmin"] == null || Session["Taikhoanadmin"].ToString() == "")
                return RedirectToAction("Login", "Admin");
            else
            {
                //Lay ra doi tuong sach can xoa theo ma
                LoaiSP loai = data.LoaiSPs.SingleOrDefault(n => n.MaLoai == id);
                ViewBag.MaLoai = loai.MaLoai;
                if (loai == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                data.LoaiSPs.DeleteOnSubmit(loai);
                data.SubmitChanges();
                return RedirectToAction("Index", "Loai");
            }
        }

        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                var loai = from lsp in data.LoaiSPs where lsp.MaLoai == id select lsp;
                return View(loai.SingleOrDefault());
            }
        }
      
        [HttpPost, ActionName("Edit")]
        public ActionResult Xacnhansua(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("Login", "Admin");
            else
            {
                LoaiSP loai = data.LoaiSPs.SingleOrDefault(n => n.MaLoai == id);

                UpdateModel(loai);
                data.SubmitChanges();
                return RedirectToAction("Index", "Loai");
            }
        }
    }
}