using Abstracciones.Modelos;
using Abstracciones.SubCategorias;
using Microsoft.AspNetCore.Mvc;


namespace Abstracciones.Interfaces.Flujo
{
    public interface ISubCategoriaFlujo
    {

        Task<IEnumerable<SubCategoriaResponse>> Obtener();
        Task<IEnumerable<SubCategoriaResponse>> Obtener(Guid Id);
    }
}
