using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cocolab.Models
{
    public class Historial_Maquina
    {
        public virtual int IdHistorialMaquina { get; set; }
        public virtual DateTime FechaRegistro { get; set; }
        public virtual DateTime FechaFinal { get; set; }
        public virtual int IdComputadora { get; set; }
        public virtual int IdUbicacion { get; set; }
    }
}