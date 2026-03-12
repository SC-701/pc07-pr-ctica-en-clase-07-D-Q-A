using Abstracciones.Interfaces.Reglas;
using Abstracciones.Interfaces.Servicios;
using Abstracciones.Modelos.Servicios.Conversion;
using System.Net.Http;
using System.Numerics;
using System.Text.Json;

namespace Servicios
{
    public class ConversionServicio : IConversionServicio
    {
        private readonly IConfiguracion _configuracion;
        private readonly IHttpClientFactory _httpClientFactory;

        public ConversionServicio(IConfiguracion configuracion, IHttpClientFactory httpClientFactory)
        {
            _configuracion = configuracion;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TipoCambio> Obtener()
        {
            var endPoint = _configuracion.ObtenerMetodo("ApiEndPointsConversion",
                "ObtenerConversion");
            var token = _configuracion.ObtenerValor("ApiEndPointsConversion:Token");
            var serviciosConversion = _httpClientFactory.CreateClient("ServicioConversion");
            var URl = string.Format(endPoint, DateTime.Now.ToString("yyyy/MM/dd"), DateTime.Now.ToString("yyyy/MM/dd"));
            var request = new HttpRequestMessage(HttpMethod.Get, URl);
            request.Headers.Add("Authorization", $"Bearer {token}");
            var respuesta = await serviciosConversion.SendAsync(request);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var resultadoDeserializado = JsonSerializer.Deserialize<TipoCambio>(resultado, opciones);
            return resultadoDeserializado;

        }
    }
}
