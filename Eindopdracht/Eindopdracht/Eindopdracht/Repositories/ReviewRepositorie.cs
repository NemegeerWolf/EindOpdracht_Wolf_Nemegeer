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
        private const string API_URL = "https://gutendex.com/books/";


        private static HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("accept", "application/json");
            client.Timeout = TimeSpan.FromSeconds(2);
            return client;
        }

        public async static Task<List<Review>> GetBooksAsync(int PageNumber)
        {
            using (HttpClient client = GetClient())
            {
                string url = $"{API_URL}/?page={PageNumber}";
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
    
    }
}
