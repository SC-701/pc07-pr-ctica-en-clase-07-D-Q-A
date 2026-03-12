using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Servicios.Conversion
{
    public class Datos
    {
        public string Titulo { get; set; }
        public string Periodicidad { get; set; }
        public IEnumerable<Indicadores> Indicadores { get; set; }


    }
}
