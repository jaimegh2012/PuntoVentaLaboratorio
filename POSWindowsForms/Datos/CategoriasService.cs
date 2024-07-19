
using Newtonsoft.Json;
using POSWindowsForms.Datos.DTOS;
using POSWindowsForms.Enviroments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace POSWindowsForms.Datos
{
    public class CategoriasService
    {
        public async Task<List<CategoriaDTO>> ObtenerCategorias()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Urls.TOKEN);

                HttpResponseMessage response = await client.GetAsync(Urls.URL_BASE + "Categorias");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<List<CategoriaDTO>>(responseBody);
                return data;
            }
        }
    }
}
