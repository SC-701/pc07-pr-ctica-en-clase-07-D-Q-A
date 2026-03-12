using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Abstracciones.SubCategorias;
using Microsoft.AspNetCore.Mvc;

namespace Flujo
{
    public class SubCategoriaFlujo : ISubCategoriaFlujo
    {

        private ISubCategoriaDA _subcatDA;

        public SubCategoriaFlujo(ISubCategoriaDA subcatDA)
        {
            _subcatDA = subcatDA;
        }

        public Task<IEnumerable<SubCategoriaResponse>> Obtener()
        {
            return _subcatDA.Obtener();
        }
        public async Task<IEnumerable<SubCategoriaResponse>> Obtener(Guid Id)
        {
            var subcat = await _subcatDA.Obtener(Id);
            return subcat;

        }

    }
}
