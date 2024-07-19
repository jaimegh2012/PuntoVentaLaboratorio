﻿using Newtonsoft.Json;
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
    public class UsuariosService
    {
        public async Task<List<UsuarioDTO>> ObtenerUsuarios()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Urls.TOKEN);

                HttpResponseMessage response = await client.GetAsync(Urls.URL_BASE + "Usuarios");

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<List<UsuarioDTO>>(responseBody);
                return data;
            }
        }

        public async void Crear(UsuarioDTO data)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Urls.TOKEN);
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(Urls.URL_BASE + "Usuarios", content);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
            }
        }

        public async void Actualizar(UsuarioDTO data)
        {
            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Urls.TOKEN);
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(Urls.URL_BASE + "Usuarios/" + data.CodUsuario.ToString(), content);

                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
