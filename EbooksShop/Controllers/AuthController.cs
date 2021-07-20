using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EbooksShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace EbooksShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        // POST /api/auth
        [HttpPost]
        public ActionResult Login([FromBody] Login login)
        {
            if (login.Username == "admin" && login.Password == "pass")
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false });
            }
        }
    }
}
