using System;
namespace cocolab.Models
{
    public class ReporteDia
    {
        public int no_control { get; set; }
        public DateTime fecha_solicitud { get; set; }      
        public DateTime fecha_finalizacion { get; set; }      
        public string uso { get; set; }
		public int ubicacion { get; set; }
    }
}
