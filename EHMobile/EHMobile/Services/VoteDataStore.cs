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
    public class VoteDataStore : IDataStore<Vote>
    {
        readonly List<Vote> items;
        const string Url = "http://172.20.10.2:8888/Votes";

        public VoteDataStore()
        {
            items = new List<Vote>();
        }

        // настройка клиента
        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<bool> AddItemAsync(Vote item)
        {
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + (string)App.Current.Properties["Token"]);
            var response = await client.PostAsync(Auth.HOST + "/votes", content);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Vote item)
        {
            var oldItem = items.Where((Vote arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((Vote arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);
            var response = await GetClient().DeleteAsync(Auth.HOST + "/votes/" + id);
            return true;
        }

        public async Task<Vote> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Vote>> GetItemsAsync(bool forceRefresh = false)
        {
            WebRequest request = WebRequest.Create(Auth.HOST + "/votes");
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
            return JsonConvert.DeserializeObject<IEnumerable<Vote>>(result);
            //return await Task.FromResult(items);
        }
    }
}