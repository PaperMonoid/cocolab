using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using cocolab.Models;
using NHibernate;
using NHibernate.Transform;

namespace cocolab.Controllers
{

    [RoutePrefix("estadisticas")]
    public class EstadisticasController : Controller
    {
        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            List<ReporteDia> reportes = session.GetNamedQuery("reportedia")
                                                .SetResultTransformer(Transformers.AliasToBean<ReporteDia>())
                                                .List<ReporteDia>()
                                                .ToList();
            NHibernateHelper.CloseSession();
            ViewData["reportes"] = reportes;
            return View();
        }

        [HttpPost]
        [Route("")]
        public ActionResult Index(DateTime Fecha, int Hora, string IdCarrera)
        {
            return View();
        }

        [HttpGet]
        [Route("carrera")]
        public ActionResult Carrera()
        {
            return View();
        }
    }
}
