using Eindopdracht.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Eindopdracht.Repositories
{
    static class BooksRepositorie
    {
        private const string API_URL = "https://gutendex.com/books/";


        private static HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("accept", "application/json");
            client.Timeout = TimeSpan.FromSeconds(2);
            return client;
        }

        public async static Task<List<Book>> GetBooksAsync()
        {
            using (HttpClient client = GetClient())
            {
                string url = $"{API_URL}";
                try
                {  
                    string json = await client.GetStringAsync(url);
                    if (json != null)
                    {

                        var o = JsonConvert.DeserializeObject<JObject>(json);
                        string boeken = JsonConvert.SerializeObject(o["results"]);
                        //var h = o.Value<JObject>("results").ToObject<List<Book>>();
                        //return h;
                        //JObject jk = JObject.Parse(json);
                        //string boeken = JsonConvert.SerializeObject(jk["results"]);
                        return JsonConvert.DeserializeObject<List<Book>>(boeken);
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
