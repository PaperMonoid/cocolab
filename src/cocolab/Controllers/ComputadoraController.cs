using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using cocolab.Models;
using NHibernate;

namespace cocolab.Controllers
{
    [RoutePrefix("basedatos/computadora")]
    public class ComputadoraController : Controller
    {

        ]
        [Route("alta")]
        public ActionResult Alta()
        {
            return View();
        }

        [HttpPost]
        [Route("alta")]
        public ActionResult Alta(Computadora computadora)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    computadora.FechaRegistro = DateTime.Now;
                    computadora.FechaModificacion = null;
                    session.Save(computadora);
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
        [Route("baja/{IdComputadora}")]
        public ActionResult Baja(int IdComputadora)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {

                    Computadora computadora = session.Query<Computadora>()
                                                .Where(x => x.IdComputadora == IdComputadora)
                                           .First();
                    if (computadora != null)
                    {
                        computadora.Estatus = false;
                        computadora.FechaModificacion = DateTime.Now;
                        session.Update(computadora);
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
        [Route("modificacion/{IdComputadora}")]
        public ActionResult Modificacion(int IdComputadora)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {

                    Computadora computadora = session.Query<Computadora>()
                                           .Where(x => x.IdComputadora == IdComputadora)
                                           .First();
                    if (computadora != null)
                    {
                        ViewData["computadora"] = computadora;
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
        public ActionResult Modificacion(Computadora computadora)
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    computadora.FechaModificacion = DateTime.Now;
                    session.Update(computadora);
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
            IQueryable<Computadora> computadoras = session.Query<Computadora>();
            switch (Busqueda)
            {
                case "NoInventario":
                    ViewData["computadoras"] = computadoras.Where(x => x.NoInventario == Valor).ToList();
                    break;
                case "Serie":
                    ViewData["computadoras"] = computadoras.Where(x => x.Serie == Valor).ToList();
                    break;
                case "Marca":
                    ViewData["computadoras"] = computadoras.Where(x => x.Marca == Valor).ToList();
                    break;
                case "Modelo":
                    ViewData["computadoras"] = computadoras.Where(x => x.Modelo == Valor).ToList();
                    break;
                case "Estatus":
                    ViewData["computadoras"] = computadoras.Where(x => x.Estatus == bool.Parse(Valor)).ToList();
                    break;
                default:
                    ViewData["computadoras"] = computadoras.ToList();
                    break;
            }
            NHibernateHelper.CloseSession();

            return View();
        }
    }
}
