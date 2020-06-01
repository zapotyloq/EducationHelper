using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using Newtonsoft.Json;

namespace EHMobile.Services
{
    public class NewDataStore : IDataStore<New>
    {
        readonly List<New> items;
        const string Url = "http://172.20.10.2:8888/new";

        public NewDataStore()
        {
            items = new List<New>()
            {
            };


        }

        // настройка клиента
        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + (string)App.Current.Properties["Token"]);
            return client;
        }

        public async Task<bool> AddItemAsync(New item)
        {
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await GetClient().PostAsync(Auth.HOST + "/news", content);


            return true;
        }
        public async Task<bool> UpdateItemAsync(New item)
        {
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await GetClient().PutAsync(Auth.HOST + "/news", content);
            return true;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var response = await GetClient().DeleteAsync(Auth.HOST + "/news/"+ id);
            return true;
        }

        public async Task<New> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<New>> GetItemsAsync(bool forceRefresh = false)
        {
            WebRequest request = WebRequest.Create(Auth.HOST + "/news");
            //request2.Headers.Set("Accept", "application/json");
            ((HttpWebRequest)request).Accept = "application/json";
            request.Headers.Set("Authorization", "Bearer " + (string)App.Current.Properties["Token"]);
            WebResponse response = await request.GetResponseAsync();

            string result = "";
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    result += reader.ReadToEnd();
                }
            }
            response.Close();

            //HttpClient client = GetClient();
            //string result = await client.GetStringAsync(Url);
            return JsonConvert.DeserializeObject<IEnumerable<New>>(result);
            //return await Task.FromResult(items);
        }
    }
}