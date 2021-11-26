using Eindopdracht.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.Repositories
{
    class ReviewRepositorie
    {
        private const string API_URL = "https://functionseindopdracht.azurewebsites.net/api/v1/reviews";


        private static HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("accept", "application/json");
            client.Timeout = TimeSpan.FromSeconds(2);
            return client;
        }

        public async static Task PostReviewsAsync(Review rev)
        {
            using (HttpClient client = GetClient())
            {
                string url = $"{API_URL}";
                try
                {
                    string json = JsonConvert.SerializeObject(rev);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Iets ging mis met de post method ({response.StatusCode})");
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public async static Task<List<Review>> GetReviewsAsync(int bookId)
        {
            using (HttpClient client = GetClient())
            {
                string url = $"{API_URL}/{bookId}";
                try
                {
                    string json = await client.GetStringAsync(url);
                    if (json != null)
                    {
                        return JsonConvert.DeserializeObject<List<Review>>(json);
                    }
                    return null;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public async static Task<Hart> GetHartAsync(int bookId)
        {
            using (HttpClient client = GetClient())
            {
                string url = $"{API_URL}/{bookId}";
                try
                {
                    string json = await client.GetStringAsync(url);
                    if (json != null)
                    {
                        return JsonConvert.DeserializeObject<Hart>(json);
                    }
                    return null;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }


        public async static Task PutHartAsync(int bookId, bool like)
        {
            using (HttpClient client = GetClient())
            {
                string url = $"{API_URL}";
                try
                {
                    var j = new {BookId = bookId, Like = like };
                    string json = JsonConvert.SerializeObject(j);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync(url, content);
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Iets ging mis met de post method ({response.StatusCode})");
                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }






    }
}
