﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GeekCoding.MainApplication.Controllers
{
    public class DiscussController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}