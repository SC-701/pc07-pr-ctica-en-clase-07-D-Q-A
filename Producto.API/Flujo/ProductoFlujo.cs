using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.SubCategorias;
using Microsoft.AspNetCore.Mvc;

namespace Flujo
{
    public class ProductoFlujo : IProductoFlujo
    {

        private IProductoDA _productoDA;
        private IConversionReglas _conversionReglas;

        public ProductoFlujo(IProductoDA productoDA, IConversionReglas conversionReglas)
        {
            _productoDA = productoDA;
            _conversionReglas = conversionReglas;
        }

        public Task<Guid> Agregar(ProductoRequest producto)
        {
            return _productoDA.Agregar(producto);
        }

        public Task<Guid> Editar(Guid Id, ProductoRequest producto)
        {
            return _productoDA.Editar(Id, producto);
        }

        public Task<Guid> Eliminar(Guid Id)
        {
            return _productoDA.Eliminar(Id);
        }

        public Task<IEnumerable<ProductoResponse>> Obtener()
        {
            return _productoDA.Obtener();
        }

        public async Task<ProductoConvertido> Obtener(Guid Id)
        {
            var producto = await _productoDA.Obtener(Id);
            producto.PrecioDolares = await _conversionReglas.ConversionColonesADolares(producto.Precio);
            return producto;

        }
    }
}
