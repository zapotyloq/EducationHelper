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
    public class UserEventDataStore : IDataStore<UserEvent>
    {
        readonly List<UserEvent> items;
        const string Url = "http://172.20.10.2:8888/userevents";

        public UserEventDataStore()
        {
            items = new List<UserEvent>();


        }

        // настройка клиента
        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<bool> AddItemAsync(UserEvent item)
        {
            //WebRequest request = WebRequest.Create(Auth.HOST + "/userevents");
            //request.Method = "POST";
            //request.Headers.Set("Authorization", "Bearer " + (string)App.Current.Properties["Token"]);

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await new HttpClient().PostAsync(Auth.HOST + "/userevents", content);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(UserEvent item)
        {
            var oldItem = items.Where((UserEvent arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            WebRequest request = WebRequest.Create(Auth.HOST + "/userevents/" + id);
            request.Method = "DELETE";
            ((HttpWebRequest)request).Accept = "application/json";
            request.Headers.Set("Authorization", "Bearer " + (string)App.Current.Properties["Token"]);
            WebResponse response = await request.GetResponseAsync();

            return await Task.FromResult(true);
        }
        public UserEvent GetItem(int id)
        {
            WebRequest request = WebRequest.Create(Auth.HOST + "/userevents/" + id);
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
            return JsonConvert.DeserializeObject<UserEvent>(result);
            //return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }
        public async Task<UserEvent> GetItemAsync(int id)
        {
            WebRequest request = WebRequest.Create(Auth.HOST + "/userevents/" + id);
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
            return JsonConvert.DeserializeObject<UserEvent>(result);
            //return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<UserEvent> GetItemByIDSAsync(int userid, int eventid)
        {
            WebRequest request = WebRequest.Create(Auth.HOST + "/userevents/getbyids?userid=" + userid +"&eventid=" +eventid);
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
            return JsonConvert.DeserializeObject<UserEvent>(result);
            //return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }
        public async Task<UserEvent> GetItemByEventIdAsync(int id)
        {
            WebRequest request = WebRequest.Create(Auth.HOST + "/userevents/byEvId/" + id);
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
            return JsonConvert.DeserializeObject<UserEvent>(result);
            //return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public UserEvent GetItemByEventId(int id)
        {
            WebRequest request = WebRequest.Create(Auth.HOST + "/userevents/byEvId/" + id);
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
            return JsonConvert.DeserializeObject<UserEvent>(result);
        }
        public async Task<UserEvent> GetItemAsync(int eventId, int userId)
        {
            WebRequest request = WebRequest.Create(Auth.HOST + "/userevents/getbyids?eventid=" + eventId +"&userid=" + userId);
            request.Method = "GET";
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
            return JsonConvert.DeserializeObject<UserEvent>(result);
            //return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<UserEvent>> GetItemsAsync(bool forceRefresh = false)
        {
            WebRequest request = WebRequest.Create(Auth.HOST +"/userevents");
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
            return JsonConvert.DeserializeObject<IEnumerable<UserEvent>>(result);
            //return await Task.FromResult(items);
        }
    }
}