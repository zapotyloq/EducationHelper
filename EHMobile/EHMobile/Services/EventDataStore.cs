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
    public class EventDataStore : IDataStore<Event>
    {
        readonly List<Event> items;
        const string Url = "http://172.20.10.2:8888/events";

        public EventDataStore()
        {
            items = new List<Event>()
            {
                new Event { Name = "На жалюзи", Total=500, AuthorId = 1, Description="hhhh"  },
                new Event { Name = "На жалюзи", Total=500, AuthorId = 1, Description="hhhh"  },
                new Event { Name = "На жалюзи", Total=500, AuthorId = 1, Description="hhhh"  },
                new Event { Name = "На жалюзи", Total=500, AuthorId = 1, Description="hhhh"  },
                new Event { Name = "На жалюзи", Total=500, AuthorId = 1, Description="hhhh"  },
                new Event { Name = "На жалюзи", Total=500, AuthorId = 1, Description="hhhh"  },
                new Event { Name = "На жалюзи", Total=500, AuthorId = 1, Description="hhhh"  }
            };


        }

        // настройка клиента
        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<bool> AddItemAsync(Event item)
        {
            WebRequest request = WebRequest.Create(Auth.HOST + "/events");
            item.AuthorId = Auth.User.Id;
            request.Method = "POST";
            request.Headers.Set("Authorization", "Bearer " + (string)App.Current.Properties["Token"]);
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await new HttpClient().PostAsync(Auth.HOST + "/events", content);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Event item)
        {
            var oldItem = items.Where((Event arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((Event arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Event> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Event>> GetItemsAsync(bool forceRefresh = false)
        {
            WebRequest request = WebRequest.Create(Auth.HOST + "/events");
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
            return JsonConvert.DeserializeObject<IEnumerable<Event>>(result);
            //return await Task.FromResult(items);
        }
    }
}