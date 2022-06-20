using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebIamge.Models;

namespace WebIamge.Controllers
{
    public class TrangchuController : Controller
    {
        private WebImageBDContext db = new WebImageBDContext();
        // GET: Trangchu
        public ActionResult Index()
        {
            ViewBag.Image = db.Manages.Count();
            return View();
        }
    }
}