﻿using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using POSWindowsForms.Datos.DTOS;
using POSWindowsForms.Enviroments;

namespace POSWindowsForms.Datos
{
    internal class ClientesService
    {
        public async Task<List<ClienteDTO>> ObtenerClientes()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Urls.TOKEN);

                HttpResponseMessage response = await client.GetAsync(Urls.URL_BASE + "Clientes");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

               var data = JsonConvert.DeserializeObject<List<ClienteDTO>>(responseBody);
               return data;
            }
        }

        public async void Crear(ClienteDTO data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Urls.TOKEN);
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(Urls.URL_BASE + "Clientes", content);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
            }
        }

        public async void Actualizar(ClienteDTO data)
        {
            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Urls.TOKEN);
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(Urls.URL_BASE + "Clientes/" + data.CodCliente.ToString(), content);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
