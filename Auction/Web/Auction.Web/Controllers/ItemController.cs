﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Auction.Web.Controllers
{
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult CreateItem()
        {
            return View();
        }
    }
}