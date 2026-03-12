using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios.Conversion;
using System.Numerics;

namespace Reglas
{
    public class ConversionReglas : IConversionReglas
    {
        private readonly IConversionServicio _conversionServicio;
        private readonly IConfiguracion _configuracion;

        public ConversionReglas(IConversionServicio conversionServicio, IConfiguracion configuracion)
        {
            _conversionServicio = conversionServicio;
            _configuracion = configuracion;
        }

        public async Task<decimal> ConversionColonesADolares(decimal precio)
        {
            TipoCambio resultadoConversion = await _conversionServicio.Obtener();
            Datos resultadoDatos = resultadoConversion.Datos.FirstOrDefault();
            Indicadores resultadoIndicadores = resultadoDatos.Indicadores.FirstOrDefault();
            Series resultadoSeries = resultadoIndicadores.Series.FirstOrDefault();
            decimal valorDolar = Convert.ToDecimal(resultadoSeries.ValorDatoPorPeriodo);
            return (precio / valorDolar);        
                
        }




    }
}
