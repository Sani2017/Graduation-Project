using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DAl.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }


        //[MyAuthorize]
        //public ActionResult About()
        //{
        //    ViewBag.Message = "Your application description page.";

        //    return Json("Your application description page.", JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}


        //public ActionResult Error()
        //{
        //    return Json("权限不足", JsonRequestBehavior.AllowGet);
        //}


    }
}
