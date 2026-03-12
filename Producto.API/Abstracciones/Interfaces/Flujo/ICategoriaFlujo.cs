using Abstracciones.Modelos;
using Abstracciones.SubCategorias;
using Microsoft.AspNetCore.Mvc;


namespace Abstracciones.Interfaces.Flujo
{
    public interface ICategoriaFlujo
    {

        Task<IEnumerable<CategoriaResponse>> Obtener();

    }
}
