using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using Newtonsoft.Json;

namespace EHMobile.Services
{
    public class UserEventDocumentDataStore : IDataStore<UserEventDocument>
    {
        readonly List<UserEventDocument> items;

        public UserEventDocumentDataStore()
        {
            items = new List<UserEventDocument>();
        }

        public async Task<bool> AddItemAsync(UserEventDocument item)
        {
            //WebRequest request = WebRequest.Create(Auth.HOST + "/usereventdocuments");
            //request.Method = "POST";
            //request.Headers.Set("Authorization", "Bearer " + (string)App.Current.Properties["Token"]);

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await new HttpClient().PostAsync(Auth.HOST + "/usereventdocuments", content);
            //using (Stream dataStream = request.GetRequestStream())
            //{
            //    dataStream.Write(byteArray, 0, byteArray.Length);
            //}
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(UserEventDocument item)
        {
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await new HttpClient().PutAsync(Auth.HOST + "/usereventdocuments", content);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((UserEventDocument arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<UserEventDocument> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<UserEventDocument>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<List<UserEventDocument>> GetUEDAsync(int usereventid)
        {
            WebRequest request = WebRequest.Create(Auth.HOST + "/usereventdocuments/GetForUserEvent?usereventid=" + usereventid);
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
            return JsonConvert.DeserializeObject<List<UserEventDocument>>(result);
        }

        public async Task<List<UserEventDocument>> GetBEDAsync(int eventid)
        {
            WebRequest request = WebRequest.Create(Auth.HOST + "/usereventdocuments/GetByEventId?eventid=" + eventid);
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

            return JsonConvert.DeserializeObject<List<UserEventDocument>>(result);
            //return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }
    }
}