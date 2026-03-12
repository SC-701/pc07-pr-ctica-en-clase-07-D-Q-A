using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Abstracciones.SubCategorias;
using Microsoft.AspNetCore.Mvc;

namespace Flujo
{
    public class CategoriaFlujo : ICategoriaFlujo
    {

        private ICategoriaDA _categoriaDA;

        public CategoriaFlujo(ICategoriaDA categoriaDA)
        {
            _categoriaDA = categoriaDA;
        }

        public Task<IEnumerable<CategoriaResponse>> Obtener()
        {
            return _categoriaDA.Obtener();
        }

    }
}
