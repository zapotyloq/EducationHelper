using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Common.Models;
using Newtonsoft.Json;

namespace EHMobile.Services
{
    public static class Auth
    {
        //public const string HOST = "http://172.20.10.2:8888";
        public const string HOST = "http://192.168.100.42:8888";
        //public const string HOST = "http://192.168.0.103:8888";

        static User user;
        public static User User {
            get { return user; }
            set {
                if (user != value)
                {
                    user = value;
                    UserChanged();
                }
            }
        }

        public delegate void UserChangedDelegate();
        public static event UserChangedDelegate UserChanged;
        public static async Task<User> GetUser()
        {
            WebRequest request = WebRequest.Create(HOST + "/checkAuth");
            //request2.Headers.Set("Accept", "application/json");
            ((HttpWebRequest)request).Accept = "application/json";
            request.Headers.Set("Authorization", "Bearer " + (string)App.Current.Properties["Token"]);
            WebResponse response;
            try
            {
                response = await request.GetResponseAsync();
            }
            catch{
                return null;
            }

            string res;
            res = "";
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    res += reader.ReadToEnd();
                }
            }
            response.Close();
            User = JsonConvert.DeserializeObject<User>(res);

            return User;
        }


        public static async Task<User> Login(string username, string password)
        {
            WebRequest request = WebRequest.Create(HOST + "/token");
            request.Method = "POST";
            string data = "username=" + username + "&password=" + password;
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            try
            {
                WebResponse response = await request.GetResponseAsync();
                string token = "";
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        token += reader.ReadToEnd();
                    }
                }

                //App.Current.Properties.Add("Token", "");
                App.Current.Properties["Token"] = token.Split(':')[1].Split(',')[0].Replace("\"", "");
                response.Close();

                return await GetUser();
            }
            catch
            {
                return null;
            }
        }
    }
}
