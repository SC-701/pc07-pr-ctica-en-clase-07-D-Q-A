using Abstracciones.Reglas.Reglas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;
using System.Text.Json;
using Abstracciones.Modelos;

namespace Producto.Web.Pages.Productos
{
    public class EditarModelo : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public EditarModelo(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public ProductoResponse productoResponse { get; set; }
        [BindProperty]
        public List<SelectListItem> categorias { get; set; }
        [BindProperty]
        public List<SelectListItem> subcategorias { get; set; }
        [BindProperty]
        public Guid categoriaseleccionada { get; set; }
        [BindProperty]
        public Guid subcategoriaseleccionada { get; set; }


        public async Task<ActionResult> OnGet(Guid? Id)
        {
            if (Id == Guid.Empty)
                return NotFound();
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerProducto");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, Id));
            var respuesta = await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            if (respuesta.StatusCode == HttpStatusCode.OK)
            {
                await ObtenerCategorias();
                var resultado = await respuesta.Content.ReadAsStringAsync();
                var opciones = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                productoResponse = JsonSerializer.Deserialize<ProductoResponse>(resultado, opciones);
                if (productoResponse != null) {
                    categoriaseleccionada = Guid.Parse(categorias.Where(m => m.Text == productoResponse.Categoria).FirstOrDefault().Value);
                    subcategorias = (await ObtenerSubCategorias(categoriaseleccionada)).Select(m=>
                    new SelectListItem
                    {
                        Value=m.Id.ToString(),
                        Text=m.Nombre,
                        Selected=m.Nombre == productoResponse.SubCategoria
                    }).ToList();
                    subcategoriaseleccionada = Guid.Parse(subcategorias.Where(m => m.Text == productoResponse.SubCategoria).FirstOrDefault().Value);
                }

            }
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {

            if (!ModelState.IsValid)
                return Page();
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarProducto");
            var cliente = new HttpClient();
            var respuesta = await cliente.PutAsJsonAsync<ProductoRequest>(string.Format(endpoint, productoResponse.Id.ToString()), 
                new ProductoRequest
            {
                Nombre = productoResponse.Nombre,
                IdSubCategoria = subcategoriaseleccionada,
                Descripcion = productoResponse.Descripcion,
                Precio = productoResponse.Precio,
                Stock = productoResponse.Stock,
                CodigoBarras = productoResponse.CodigoBarras

            });
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

        private async Task<List<SubCategoria>> ObtenerSubCategorias(Guid marcaId)
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerSubCategorias");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, string.Format(endpoint, marcaId));
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

        public async Task<JsonResult> OnGetObtenerSubCategorias(Guid marcaId)
        {
            var subcategorias = await ObtenerSubCategorias(marcaId);
            return new JsonResult(subcategorias);
        }
    }

}
