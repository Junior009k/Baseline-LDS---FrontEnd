using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;

namespace Baseline_LDS___FrontEnd.Components.API.Config
{

   

    public class userModel
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }

        [JsonProperty("username")]
        public string username { get; set; }

        [JsonProperty("password")]
        public string password { get; set; }
    }


    public class valueModel
    {
        [JsonProperty("token")]
        public string token { get; set; }

        [JsonProperty("message")]
        public string message { get; set; }
    }
    public class responseModel
    {
        [JsonProperty("value")]
        public valueModel value { get; set; }

    }

    public class Api
    {
        static HttpClient client = new HttpClient();
        public string pokemones = "";
        public Api() 
        {
            if (client.BaseAddress == null)
            {
                //client.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/");
            }

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<responseModel> Inicializar(string URL,string username,string password)
        {

            Console.WriteLine(URL);
            // Datos que deseas enviar en la petición POST
            var postData = new
            {
                username = username,
                password = password
            };

            // Convertir los datos a formato JSON
            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(postData);

            // Crear el contenido de la petición
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Enviar la petición POST
            HttpResponseMessage response = await client.PostAsync(URL + "/auth/login", content);

            // Leer y manejar la respuesta
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Respuesta del servidor: " + responseData);

                return getObject(responseData);
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
                return null;
            }
        }

        public async Task<responseModel> Register(string URL, string username, string password, string name, string id_perfil)
        {

            Console.WriteLine(URL);
            // Datos que deseas enviar en la petición POST
            var postData = new
            {
                username,
                password,
                name,
                id_perfil

            };

            // Convertir los datos a formato JSON
            string jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(postData);

            // Crear el contenido de la petición
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Enviar la petición POST
            HttpResponseMessage response = await client.PostAsync(URL + "/auth/register", content);

            // Leer y manejar la respuesta
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Respuesta del servidor: " + responseData);

                return getObject(responseData);
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
                return null;
            }
        }


        public responseModel getObject(string response)
        {
            responseModel model = JsonConvert.DeserializeObject<responseModel>(response);
            return model;
        }
    }

       
}
