using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnWeb.Models
{
    public class Giohang
    {
        dbFSDataContext db = new dbFSDataContext();
        public int iMasp { set; get; }
        public string sTensp { set; get; }
        public string sAnhsp { set; get; }
        public Double dGia { set; get; }
        public int iSoluong { set; get; }
        public Double dThanhtien
        {
            get { return iSoluong * dGia; }

        }
      
        public Giohang(int Masp)
        {
            iMasp = Masp;
            SanPhan sp = db.SanPhans.Single(n => n.MaSP == iMasp);
            sTensp = sp.TenSP;
            sAnhsp = sp.AnhSP;
            dGia = double.Parse(sp.Gia.ToString());
            iSoluong = 1;
        }
    }
}