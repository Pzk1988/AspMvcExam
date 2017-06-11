using PANExam.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PANExam.Controllers
{
    public class EnrolmentsController : Controller
    {
        private DataAccess dataAccess;

        public EnrolmentsController()
        {
            dataAccess = new DataAccess();
        }

        // GET: Enrolments
        public ActionResult Index()
        {

            return View();
        }
    }
}