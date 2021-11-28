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
        private const string API_URL = "https://functionseindopdracht.azurewebsites.net/api/v1";


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
                string url = $"{API_URL}/reviews";
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
                string url = $"{API_URL}/reviews/{bookId}";
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
                string url = $"{API_URL}/hart/{bookId}";
                try
                {
                    string json = await client.GetStringAsync(url);
                    if (json != "" && json != null)
                    {
                        return JsonConvert.DeserializeObject<Hart>(json);
                    }
                    return new Hart()
                    {
                        id = "",
                        BookId = bookId,
                        Like = false
                        
                    };
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }

        public async static Task<List<int>> GetHartsIdAsync()
        {
            using (HttpClient client = GetClient())
            {
                string url = $"{API_URL}/harts";
                try
                {
                    string json = await client.GetStringAsync(url);
                    if (json != "" && json != null)
                    {
                        List<Hart> harts = JsonConvert.DeserializeObject<List<Hart>>(json);
                        List<int> lstIds = new List<int>();
                        foreach(Hart hart in harts)
                        {
                            lstIds.Add(hart.BookId);
                        }
                        return lstIds;
                    }
                    List<int> ls = new List<int>();
                   
                    return ls;
                
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }


        public async static Task PutHartAsync(int bookId, bool like, string id)
        {
            using (HttpClient client = GetClient())
            {
                string url = $"{API_URL}/hart";
                try
                {
                    var j = new {BookId = bookId, Like = like, Id = id };
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
