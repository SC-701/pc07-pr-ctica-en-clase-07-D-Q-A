using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class ProductoBase
    {
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(80)]
        public string Descripcion { get; set; }

        [Required]
        [Range(1, 100000000)]
        public decimal Precio { get; set; }

        [Required]
        [Range(1, 10000)]
        public int Stock { get; set; }

        [Required]
        [StringLength(10)]
        public string CodigoBarras { get; set; }
    }

    public class ProductoRequest : ProductoBase
    {
        public Guid IdSubCategoria { get; set; }
    }

    public class ProductoResponse : ProductoBase
    {
        public Guid Id { get; set; }
        public string? SubCategoria { get; set; }
        public string? Categoria { get; set; }
    }

    public class ProductoConvertido : ProductoResponse
    {

        public decimal PrecioDolares { get; set; }
    }
}
