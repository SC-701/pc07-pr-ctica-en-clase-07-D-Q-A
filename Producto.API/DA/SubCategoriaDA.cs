using Abstracciones.Interfaces.DA;
using Abstracciones.Modelos;
using Abstracciones.SubCategorias;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace DA
{
    public class SubCategoriaDA : ISubCategoriaDA
    {

        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _connection;

        public SubCategoriaDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _connection = _repositorioDapper.ObtenerRepositorio(); ;
        }


        public async Task<IEnumerable<SubCategoriaResponse>> Obtener()
        {
            string query = @"ObtenerSubCategorias";
            var resultado = await _connection.QueryAsync<SubCategoriaResponse>(query);
            return resultado;
        }

        public async Task<IEnumerable<SubCategoriaResponse>> Obtener(Guid Id)
        {
            string query = @"ObtenerSubCategoria";
            var resultado = await _connection.QueryAsync<SubCategoriaResponse>(query, new { Id = Id });
            return resultado;
        }


    }
}
