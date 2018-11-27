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
    public class AlumnoController : Controller
    {
        public string Index()
        {
            return "placeholder index";
        }

        public string Alta()
        {
            ISessionFactory sessionFactory =
                new Configuration().Configure().BuildSessionFactory();
            ISession session = sessionFactory.OpenSession();
            try
            {
                using (ITransaction tx = session.BeginTransaction())
                {
                    var alumno = new Alumno
                    {
                        NoControl = 1212034,
                        Nombre = "Kim",
                        ApellidoPaterno = "Da",
                        ApellidoMaterno = "Hyun",
                        IdCarrera = "ISIC",
                        Estatus = true,
                        FechaRegistro = DateTime.Now,
                        FechaModificacion = DateTime.Now
                    };

                    session.Save(alumno);
                    tx.Commit();
                }
            }
            finally
            {
                session.Close();
            }
            return "placeholder alta";
        }

        public string Baja()
        {
            return "placeholder baja";
        }

        public string Consulta()
        {
            return "placeholder consulta";
        }
    }
}
