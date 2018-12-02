using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cocolab.Models;
using NHibernate;
using NHibernate.Cfg;

namespace cocolab.Controllers
{
    [RoutePrefix("basedatos")]
    public class BaseDatosController : Controller
    {
        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            List<Alumno> alumnos = session.Query<Alumno>().ToList();
            NHibernateHelper.CloseSession();
            ViewData["alumnos"] = alumnos;
            return View();
        }

    }
}
