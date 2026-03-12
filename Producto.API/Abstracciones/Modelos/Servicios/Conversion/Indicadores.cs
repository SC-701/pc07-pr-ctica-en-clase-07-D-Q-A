using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Servicios.Conversion
{
    public class Indicadores
    {
        public string CodigoIndicador { get; set; }
        public string NombreIndicador { get; set; }
        public IEnumerable<Series> Series { get; set; }

    }
}
