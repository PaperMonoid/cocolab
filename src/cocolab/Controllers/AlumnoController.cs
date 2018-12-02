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
    [RoutePrefix("basedatos/alumno")]
    public class AlumnoController : Controller
    {
        [HttpGet]
        [Route("alta")]
        public ActionResult Alta()
        {
            return View();
        }

        [HttpPost]
        [Route("alta")]
        public ActionResult Alta(Alumno alumno)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    alumno.FechaRegistro = DateTime.Now;
                    alumno.FechaModificacion = null;
                    session.Save(alumno);
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
        [Route("baja/{NoControl}")]
        public ActionResult Baja(long NoControl)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {

                    Alumno alumno = session.Query<Alumno>()
                                           .Where(x => x.NoControl == NoControl)
                                           .First();
                    if (alumno != null)
                    {
                        alumno.Estatus = false;
                        alumno.FechaModificacion = DateTime.Now;
                        session.Update(alumno);
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
        [Route("modificacion/{NoControl}")]
        public ActionResult Modificacion(long NoControl)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {

                    Alumno alumno = session.Query<Alumno>()
                                           .Where(x => x.NoControl == NoControl)
                                           .First();
                    if (alumno != null)
                    {
                        ViewData["alumno"] = alumno;
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
        public ActionResult Modificacion(Alumno alumno)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    alumno.FechaModificacion = DateTime.Now;
                    session.Update(alumno);
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
        [Route("")]
        public ActionResult Index(string Busqueda, string Valor)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            IQueryable<Alumno> alumnos = session.Query<Alumno>();
            switch (Busqueda)
            {
                case "NoControl":
                    ViewData["alumnos"] = alumnos.Where(x => x.NoControl == long.Parse(Valor)).ToList();
                    break;
                case "Nombre":
                    ViewData["alumnos"] = alumnos.Where(x => x.Nombre == Valor).ToList();
                    break;
                case "ApellidoPaterno":
                    ViewData["alumnos"] = alumnos.Where(x => x.ApellidoPaterno == Valor).ToList();
                    break;
                case "ApellidoMaterno":
                    ViewData["alumnos"] = alumnos.Where(x => x.ApellidoMaterno == Valor).ToList();
                    break;
                default:
                    ViewData["alumnos"] = alumnos.ToList();
                    break;
            }
            NHibernateHelper.CloseSession();

            return View();
        }
    }
}
