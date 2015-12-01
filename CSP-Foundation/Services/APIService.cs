using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSP_Foundation.Interfaces;
using System.Net;
using System.Web;
using System.IO;
using Newtonsoft.Json;

namespace CSP_Foundation.Services
{
    public class APIService : IAPIService
    {
        public HttpWebRequest Request { get; set; }
        public APIService(string uri)
        {
            Request = (HttpWebRequest)WebRequest.Create(uri);
            Request.Method = "GET";
            Request.Accept = "application/json";
        }

        public APIService(string uri, string contentType)
        {
            Request = (HttpWebRequest)WebRequest.Create(uri);
            Request.Method = "POST";
            Request.Accept = "application/json";
            Request.ContentType = contentType;
        }

        public T Get<T>()
        {
            var response = Request.GetResponse();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var responseContent = reader.ReadToEnd();
                var value = JsonConvert.DeserializeObject<T>(responseContent);
                return value;
            }
        }

        public async Task<T> GetAsync<T>()
        {
            var response = await Request.GetResponseAsync().ConfigureAwait(false);
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var responseContent = reader.ReadToEnd();
                var value = JsonConvert.DeserializeObject<T>(responseContent);
                return value;
            }
        }

        public T Send<T>(string content)
        {
            using (var writer = new StreamWriter(Request.GetRequestStream()))
            {
                writer.Write(content);
            }

            var response = Request.GetResponse();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var responseContent = reader.ReadToEnd();
                var value = JsonConvert.DeserializeObject<T>(responseContent);
                return value;
            }
        }

        public async Task<T> SendAsync<T>(string content)
        {
            using (var writer = new StreamWriter(Request.GetRequestStream()))
            {
                writer.Write(content);
            }

            var response = await Request.GetResponseAsync().ConfigureAwait(false);
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var responseContent = reader.ReadToEnd();
                var value = JsonConvert.DeserializeObject<T>(responseContent);
                return value;
            }
        }
    }
}
