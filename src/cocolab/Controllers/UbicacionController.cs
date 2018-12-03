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

    /* Falta agregar en modificación el hecho de que solo se puede modificar
      si el estado es falso. */

    [RoutePrefix("basedatos/ubicacion")]
    public class UbicacionController : Controller
    {
        [HttpGet]
        [Route("alta")]
        public ActionResult Alta()
        {
            return View();
        }

        [HttpPost]
        [Route("alta")]
        public ActionResult Alta(Ubicacion ubicacion)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    ubicacion.FechaRegistro = DateTime.Now;
                    ubicacion.FechaModificacion = DateTime.Now;
                    ubicacion.Comentario = "Alta";
                    session.Save(ubicacion);
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
        [Route("baja/{IdUbicacion}")]
        public ActionResult Baja(int IdUbicacion)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    Ubicacion ubicacion = session.Query<Ubicacion>()
                                                 .Where(x => x.IdUbicacion == IdUbicacion)
                                                 .First();
                    if (ubicacion != null)
                    {
                        ubicacion.Estatus = false;
                        ubicacion.FechaModificacion = DateTime.Now;
                        ubicacion.Comentario = "Baja";
                        session.Update(ubicacion);
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
        [Route("modificacion/{IdUbicacion}")]
        public ActionResult Modificacion(int IdUbicacion)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    Ubicacion ubicacion = session.Query<Ubicacion>()
                                                 .Where(x => x.IdUbicacion == IdUbicacion)
                                                 .First();
                    if (ubicacion != null)
                    {
                        ViewData["ubicacion"] = ubicacion;
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
        public ActionResult Modificacion(Ubicacion ubicacion)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    ubicacion.FechaModificacion = DateTime.Now;
                    session.Update(ubicacion);
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
            IQueryable<Ubicacion> ubicaciones = session.Query<Ubicacion>();
            switch (Busqueda)
            {
                case "IdUbicacion":
                    ViewData["ubicaciones"] = ubicaciones.Where(x => x.IdUbicacion == int.Parse(Valor)).ToList();
                    break;
                case "IdComputadora":
                    ViewData["ubicaciones"] = ubicaciones.Where(x => x.IdComputadora == int.Parse(Valor)).ToList();
                    break;
                case "Estatus":
                    ViewData["ubicaciones"] = ubicaciones.Where(x => x.Estatus == bool.Parse(Valor)).ToList();
                    break;
                default:
                    ViewData["ubicaciones"] = ubicaciones.ToList();
                    break;
            }
            NHibernateHelper.CloseSession();

            return View();
    }
}
