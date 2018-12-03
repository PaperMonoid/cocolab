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
    [RoutePrefix ("basedatos/carrera")]
    public class CarreraController : Controller
    {
        [HttpGet]
        [Route("alta")]
        public ActionResult Alta()
        {
            return View();
        }

        [HttpPost]
        [Route("alta")]
        public ActionResult Alta (Carrera carrera)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    session.Save(carrera);
                    tx.Commit();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("baja/{IdCarrera}")]
        public ActionResult Baja(string IdCarrera)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    Carrera carrera = session.Query<Carrera>()
                                             .Where(x => x.IdCarrera == IdCarrera)
                                             .First();
                    if (carrera != null)
                    {
                        session.Delete(carrera);
                    }
                    tx.Commit();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("modificacion/{IdCarrera}")]
        public ActionResult Modificacion(string IdCarrera)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {

                    Carrera carrera = session.Query<Carrera>()
                                             .Where(x => x.IdCarrera == IdCarrera)
                                             .First();
                    if (carrera != null)
                    {
                        ViewData["carrera"] = carrera;
                    }
                    tx.Commit();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return View();
        }

        [HttpPost]
        [Route("modificacion")]
        public ActionResult Modificacion(Carrera carrera)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    session.Update(carrera);
                    tx.Commit();
                }
            }
            finally
            {
                NHibernateHelper.CloseSession();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route ("")]
        public ActionResult Index(string Busqueda, string Valor)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            IQueryable<Carrera> carreras = session.Query<Carrera>();
            switch (Busqueda)
            {
                case "IdCarrera":
                    ViewData["carreras"] = carreras.Where(x => x.IdCarrera == (Valor)).ToList();
                    break;
                case "Descripcion":
                    ViewData["carreras"] = carreras.Where(x => x.Descripcion == (Valor)).ToList();
                    break;
                default:
                    ViewData["carreras"] = carreras.ToList();
                    break;
            }
            NHibernateHelper.CloseSession();
            return View();
        }
    }
}
