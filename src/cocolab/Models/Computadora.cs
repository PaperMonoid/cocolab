using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cocolab.Models
{
    public class Computadora
    {
        public virtual int IdComputadora { get; set; }
        public virtual bool Estatus { get; set; }
        public virtual string Serie { get; set; }
        public virtual string Marca { get; set; }
        public virtual string Modelo { get; set; }
        public virtual string NoInventario { get; set; }
        public virtual DateTime FechaRegistro { get; set; }
        public virtual DateTime? FechaModificacion { get; set; }
    }
}