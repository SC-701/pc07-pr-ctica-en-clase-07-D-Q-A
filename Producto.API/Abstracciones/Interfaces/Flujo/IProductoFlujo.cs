using Abstracciones.SubCategorias;
using Microsoft.AspNetCore.Mvc;


namespace Abstracciones.Interfaces.Flujo
{
    public interface IProductoFlujo
    {

        Task<IEnumerable<ProductoResponse>> Obtener();
        Task<ProductoConvertido> Obtener(Guid Id);
        Task<Guid> Agregar(ProductoRequest producto);
        Task<Guid> Editar(Guid Id, ProductoRequest producto);
        Task<Guid> Eliminar(Guid Id);
    }
}
