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
    public class ProductosService
    {
        public async Task<List<ProductoDTO>> ObtenerProductos()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Urls.TOKEN);

                HttpResponseMessage response = await client.GetAsync(Urls.URL_BASE + "Productos");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<List<ProductoDTO>>(responseBody);
                return data;
            }
        }

        public async void Crear(ProductoDTO data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Urls.TOKEN);
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(Urls.URL_BASE + "Productos", content);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
            }
        }

        public async void Actualizar(ProductoDTO data)
        {
            using (HttpClient client = new HttpClient())
            {
                
                //var url = $"{Urls.URL_BASE}Productos/{}"  
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Urls.TOKEN);
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(Urls.URL_BASE + "Productos/" + data.CodProducto.ToString(), content);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
