using Abstracciones.Interfaces.DA;
using Abstracciones.SubCategorias;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Dapper;

namespace DA
{
    public class ProductoDA : IProductoDA
    {

        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _connection;

        public ProductoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _connection = _repositorioDapper.ObtenerRepositorio(); ;
        }

        public async Task<Guid> Agregar(ProductoRequest producto)
        {
            string query = @"AgregarProducto";
            var resultado = await _connection.ExecuteScalarAsync<Guid>(query, new
            {

                Id = Guid.NewGuid(),
                IdSubCategoria = producto.IdSubCategoria,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Stock = producto.Stock,
                Precio = producto.Precio,
                CodigoBarras = producto.CodigoBarras            
            });
            return resultado;
        }

        public async Task<Guid> Editar(Guid Id, ProductoRequest producto)
        {
            await verificarproducto(Id);
            string query = @"EditarProducto";
            var resultado = await _connection.ExecuteScalarAsync<Guid>(query, new
            {

                Id = Id,
                IdSubCategoria = producto.IdSubCategoria,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Stock = producto.Stock,
                Precio = producto.Precio,
                CodigoBarras = producto.CodigoBarras,
            });
            return resultado;
        }

        public async Task<Guid> Eliminar(Guid Id)
        {
            await verificarproducto(Id);
            string query = @"EliminarProducto";
            var resultado = await _connection.ExecuteScalarAsync<Guid>(query, new
            {

                Id = Id
            });
            return resultado;
        }

        public async Task<IEnumerable<ProductoResponse>> Obtener()
        {
            string query = @"ObtenerProductos";
            var resultado = await _connection.QueryAsync<ProductoResponse>(query);
            return resultado;
        }

        public async Task<ProductoConvertido> Obtener(Guid Id)
        {
            string query = @"ObtenerProducto";
            var resultado = await _connection.QueryAsync<ProductoConvertido>(query,
                new { Id = Id });
            return resultado.FirstOrDefault();
        }

        private async Task verificarproducto(Guid Id)
        {
            ProductoResponse? resultadoproducto = await Obtener(Id);
            if (resultadoproducto == null)
            { throw new Exception("No se encontró el producto"); }
        }
    }
}
