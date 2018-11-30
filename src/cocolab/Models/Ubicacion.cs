using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cocolab.Models
{
    public class Ubicacion
    {
        public virtual int IdUbicacion { get; set; }
        public virtual bool Estatus { get; set; }
        public virtual DateTime FechaRegistro { get; set; }
        public virtual DateTime FechaModificacion { get; set; }
        public virtual string Comentario { get; set; }
        public virtual int IdComputadora { get; set; }
    }
}