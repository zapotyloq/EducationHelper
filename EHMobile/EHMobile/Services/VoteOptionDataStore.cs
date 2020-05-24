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
    public class VoteOptionDataStore : IDataStore<VoteOption>
    {
        readonly List<VoteOption> items;
        const string Url = "http://172.20.10.2:8888/Votes";

        public VoteOptionDataStore()
        {
            items = new List<VoteOption>();
        }

        // настройка клиента
        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<bool> AddItemAsync(VoteOption item)
        {
            WebRequest request = WebRequest.Create(Auth.HOST + "/voteoptions");
            request.Method = "POST";
            request.Headers.Set("Authorization", "Bearer " + (string)App.Current.Properties["Token"]);
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await new HttpClient().PostAsync(Auth.HOST + "/voteoptions", content);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(VoteOption item)
        {
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await new HttpClient().PutAsync(Auth.HOST + "/voteoptions", content);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var response = await new HttpClient().DeleteAsync(Auth.HOST + "/voteoptions/"+id);

            return await Task.FromResult(true);
        }

        public async Task<VoteOption> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }
        public VoteOption GetItem(int id)
        {
            WebRequest request = WebRequest.Create(Auth.HOST + "/voteoptions/" + id);
            //request2.Headers.Set("Accept", "application/json");
            ((HttpWebRequest)request).Accept = "application/json";
            request.Headers.Set("Authorization", "Bearer " + (string)App.Current.Properties["Token"]);
            WebResponse response = request.GetResponse();

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
            return JsonConvert.DeserializeObject<VoteOption>(result);
        }

        public async Task<IEnumerable<VoteOption>> GetItemsAsync(bool forceRefresh = false)
        {
            WebRequest request = WebRequest.Create(Auth.HOST + "/voteoptions");
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
            return JsonConvert.DeserializeObject<IEnumerable<VoteOption>>(result);
            //return await Task.FromResult(items);
        }

        public async Task<List<VoteOption>> GetItemsByVoteIdAsync(int voteId)
        {
            WebRequest request = WebRequest.Create(Auth.HOST + "/voteoptions/getforvote/" + voteId);
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
            return JsonConvert.DeserializeObject<List<VoteOption>>(result);
            //return await Task.FromResult(items);
        }

    }
}