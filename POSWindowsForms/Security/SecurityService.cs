using Newtonsoft.Json;
using POSWindowsForms.Enviroments;
using POSWindowsForms.Security.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace POSWindowsForms.Security
{
    public class SecurityService
    {
        public async Task<AuthorizationResponse> Autenticar(AuthorizationRequest data)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var json = JsonConvert.SerializeObject(data);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(Urls.URL_BASE + "SignIn/Autenticar", content);

                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    var dataRespuesta = JsonConvert.DeserializeObject<AuthorizationResponse>(responseBody);
                    return dataRespuesta;
                }
                catch (Exception ex)
                {
                    return new AuthorizationResponse() { Resultado = false };
                }
            }
        }
    }
}
