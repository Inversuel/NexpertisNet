using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CrudWebList.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CrudWebList.Controllers.API
{
    public class ApiHandler
    {
        private static readonly string ApiUrl = "http://localhost:3000/";
        public static async Task<Response> GetAllTask()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = client.GetAsync("todolist/all").Result;
                var response = new Response();
                if (responseMessage.IsSuccessStatusCode)
                {
                    response.ResponseMessage = await responseMessage.Content.ReadAsStringAsync();
                }
                else
                {
                    response.ErrorMessage = await responseMessage.Content.ReadAsStringAsync();
                }
                return response;


            }

        }

        public static async Task<Response> PostTask(string jsonData)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = client.PostAsync("todolist/add", content).Result;
                var response = new Response();
                if (responseMessage.IsSuccessStatusCode)
                {
                    response.ResponseMessage = await responseMessage.Content.ReadAsStringAsync();
                }
                else
                {
                    response.ErrorMessage = await responseMessage.Content.ReadAsStringAsync();
                }
                return response;


            }

        }
        public static async Task<Response> PatchTask(string jsonData, string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //var content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
                //HttpResponseMessage responseMessage = client.PatchAsync("todolist/re/"+ id, content).Result;

                var request = new HttpRequestMessage(new HttpMethod("PATCH"), "todolist/re/" + id);
                request.Content = new StringContent(jsonData.ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage responseMessage = await client.SendAsync(request);


                var response = new Response();
                if (responseMessage.IsSuccessStatusCode)
                {
                    response.ResponseMessage = await responseMessage.Content.ReadAsStringAsync();
                }
                else
                {
                    response.ErrorMessage = await responseMessage.Content.ReadAsStringAsync();
                }
                return response;


            }

        }


        public static async Task<Response> GetDeleted(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrl);
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = client.DeleteAsync("todolist/id/remove/" + id).Result;
                var response = new Response();
                if (responseMessage.IsSuccessStatusCode)
                {
                    response.ResponseMessage = await responseMessage.Content.ReadAsStringAsync();
                }
                else
                {
                    response.ErrorMessage = await responseMessage.Content.ReadAsStringAsync();
                }
                return response;


            }

        }
    }
}