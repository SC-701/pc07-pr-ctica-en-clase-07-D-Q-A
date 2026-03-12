using Abstracciones.Reglas.Reglas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;
using Abstracciones.Modelos;

namespace Producto.Web.Pages.Productos
{
    public class AgregarModelo : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public AgregarModelo(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public ProductoRequest producto { get; set; }
        [BindProperty]
        public List<SelectListItem> categorias { get; set; }
        [BindProperty]
        public List<SelectListItem> subcategorias { get; set; }
        [BindProperty]
        public Guid categoriaseleccionada { get; set; }


        public async Task<ActionResult> OnGet()
        {
            await ObtenerCategorias();
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {

            if (!ModelState.IsValid)
                return Page();
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarProducto");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Post, string.Format(endpoint));
            var respuesta = await cliente.PostAsJsonAsync(endpoint, producto);
            respuesta.EnsureSuccessStatusCode();
            return RedirectToPage("./Index");
        }

        private async Task ObtenerCategorias()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerCategorias");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            var resultado = await respuesta.Content.ReadAsStringAsync();

            var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var resultadoserialziado = JsonSerializer.Deserialize<List<Categoria>>(resultado, opciones);
            categorias = resultadoserialziado.Select(m =>
            new SelectListItem
            {
                Value = m.Id.ToString(),
                Text = m.Nombre
            }).ToList();
        }

        private async Task<List<SubCategoria>> ObtenerSubCategorias(Guid categoriaId)
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerSubCategorias");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, categoriaId));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };    
                return JsonSerializer.Deserialize<List<SubCategoria>>(resultado, opciones);
            }
            return new List<SubCategoria>();
        }

        public async Task<JsonResult> OnGetObtenerSubCategorias(Guid categoriaId)
        {
            var subcategorias = await ObtenerSubCategorias(categoriaId);
            return new JsonResult(subcategorias);
        }
    }

}
