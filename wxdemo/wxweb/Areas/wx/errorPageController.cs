using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wxweb.Areas.wx
{
    public class errorPageController : Controller
    {
        // GET: wx/errorPage
        public ActionResult Index()
        {
            return View();
        }
    }
}