using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CommonLibrary.Web.Mvc;
using CustomComponentKendo.Models;
using IranSoftjo.Core.Web.Mvc;

namespace CustomComponentKendo.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Validation()
        {
            return View(new BookingVm());
        }

        [HttpPost]
        public ActionResult Validation(BookingVm inVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (inVm.Name == "1")
                    {
                        TempData.SetMessage("پیغام موفق است", MessageType.Success);
                    }
                    else if (inVm.Name == "2")
                    {
                        TempData.SetMessage("پیغام خطا است", MessageType.Error);
                    }
                    else if (inVm.Name == "3")
                    {
                        TempData.SetMessage("پیغام هشدار است", MessageType.Warn);
                    }
                }
                catch (Exception ex)
                {
                    TempData.SetMessage(ex.Message, MessageType.Error);
                }
            }
            return View();
        }

        public ActionResult SaveFile(HttpPostedFileBase file)
        {
            Session["HttpPostedFileBase"] = file;
            return Content("");
        }
        public Dictionary<string, string> DicType = new Dictionary<string, string>
        {
            {"1", "مرد"},
            {"2", "زن"}
        };
        public ActionResult Components()
        {
        ViewBag.Jensiat = new SelectList(DicType, "Key", "Value", "1");
            return View(new BookingVm());
        }
        public ActionResult Index()
        {
            return View(new BookingVm());
        }

        public ActionResult Numbers()
        {
            return View(new BookingVm());
        }
        public ActionResult Grid()
        {
            var lst =new List<BookingVm>();
            lst.Add(new BookingVm()
            {
                CardNumber="test"
            });
            return View(lst);
        }

        public ActionResult Editor()
        {
            return View();
        }
    }
}