using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;

namespace cocolab.Models
{
    public class Alumno
    {
        public int id_alumno { get; set; }
        public string nombre { get; set; }
        public string apellido_paterno { get; set; }
        public string apellido_materno { get; set; }
        public string id_carrera { get; set; }
        public bool estatus_alumno { get; set; }
        public DateTime fecha_registro_alumno {get; set;}
        public DateTime fecha_modificacion_alumno { get; set; } 

        
    }
}