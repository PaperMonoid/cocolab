using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cocolab.Models
{
    public class Alumno
    {
        public virtual long NoControl { get; set; }
        public virtual string Nombre { get; set; }
        public virtual string ApellidoPaterno { get; set; }
        public virtual string ApellidoMaterno { get; set; }
        public virtual string IdCarrera { get; set; }
        public virtual bool Estatus { get; set; }
        public virtual DateTime FechaRegistro { get; set;}
        public virtual DateTime FechaModificacion { get; set; }
    }
}