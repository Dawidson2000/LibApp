using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibApp.Controllers
{
    public class RentalsController : Controller
    {
        [Authorize(Roles = "Owner, User, StoreManager")]
        public IActionResult New()
        {
            return View();
        }
    }
}
