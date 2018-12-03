using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using cocolab.Models;
using NHibernate;

namespace cocolab.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public ActionResult Index()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            List<RegistroUbicacion> registroUbicaciones =
                session.Query<Ubicacion>()
                       .Select(x => new RegistroUbicacion { Ubicacion = x, Registro = session.Query<Registro>().OrderByDescending(y => y.FechaSolicitud).First() })
                       .ToList();
            ViewData["registroUbicaciones"] = registroUbicaciones.ToList();
            NHibernateHelper.CloseSession();
            return View();
        }

        [HttpPost]
        [Route("")]
        public ActionResult Index(int IdUbicacion, long NoControl)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            Registro registro = session.Query<Registro>()
                                       .Where(x => x.IdUbicacion == IdUbicacion && x.NoControlAlumno == NoControl && x.FechaFinalizacion == null)
                                       .OrderByDescending(x => x.FechaSolicitud)
                                       .FirstOrDefault();
            try 
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    if (registro == null)
                    {
                        registro = new Registro
                        {
                            NoControlAlumno = NoControl,
                            FechaSolicitud = DateTime.Now,
                            Uso = "Actual",
                            IdUbicacion = IdUbicacion
                        };
                        session.Save(registro);
                        tx.Commit();
                    }
                    else
                    {
                        registro.FechaFinalizacion = DateTime.Now;
                        session.Update(registro);
                        tx.Commit();
                    }
                }
            } 
            finally 
            {
                NHibernateHelper.CloseSession();
            }
            return RedirectToAction("Index");
        }
    }
}
