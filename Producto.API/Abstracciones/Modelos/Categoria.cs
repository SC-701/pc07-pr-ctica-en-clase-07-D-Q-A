using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class Categoria
    {
        public string Nombre { get; set; }

    }

    public class CategoriaResponse : Categoria
    {
        public Guid Id { get; set; }

    }
}
