using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Abstracciones.SubCategorias;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class CategoriaDA : ICategoriaDA
    {

        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _connection;

        public CategoriaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _connection = _repositorioDapper.ObtenerRepositorio(); ;
        }
       
        public async Task<IEnumerable<CategoriaResponse>> Obtener()
        {
            string query = @"ObtenerCategorias";
            var resultado = await _connection.QueryAsync<CategoriaResponse>(query);
            return resultado;
        }

    }
}
