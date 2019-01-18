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

        [HttpGet]
        [Route("hora")]
            public ActionResult Hora()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            List<ReporteDia> reportes = session.GetNamedQuery("reportehora")
                                                .SetResultTransformer(Transformers.AliasToBean<ReporteDia>())
                                                .List<ReporteDia>()
                                                .ToList();
            NHibernateHelper.CloseSession();
            ViewData["reportes"] = reportes;
            return View();
        }

        [HttpPost]
        [Route("")]
        public ActionResult Carrera(string IdCarrera)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            List<ReporteDia> reportes = session.GetNamedQuery("reportecarrera")
                                               .SetString("carrera", IdCarrera)
                                               .SetResultTransformer(Transformers.AliasToBean<ReporteDia>())
                                               .List<ReporteDia>()
                                               .ToList();
            NHibernateHelper.CloseSession();
            ViewData["reportes"] = reportes;
            return View();
        }
    }
}
