﻿using System;
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
                    if (inVm.FirstName == "1")
                    {
                        TempData.SetMessage("پیغام موفق است", MessageType.Success);
                    }
                    else if (inVm.FirstName == "2")
                    {
                        TempData.SetMessage("پیغام خطا است", MessageType.Error);
                    }
                    else if (inVm.FirstName == "3")
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

        public ActionResult Components()
        {
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
            return View(new BookingVm());
        }

        public ActionResult About()
        {
            return View();
        }
    }
}