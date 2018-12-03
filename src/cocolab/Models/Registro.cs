using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cocolab.Models
{
    public class Registro
    {
        public virtual int IdRegistro { get; set; }
        public virtual long NoControlAlumno { get; set; }
        public virtual DateTime FechaSolicitud { get; set; }
        public virtual DateTime? FechaFinalizacion { get; set; }
        public virtual string Uso { get; set; }
        public virtual int IdUbicacion { get; set; }
    }
}