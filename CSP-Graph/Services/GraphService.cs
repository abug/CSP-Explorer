using CSP_Foundation.Services;
using CSP_Graph.Entities;
using CSP_Graph.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CSP_Graph.Services
{
    public class GraphService : IGraphService
    {
        public Token oauth_token { get; set; }
        public string app_id { get; set; }
        public string app_key { get; set; }
        public string tenant_name { get; set; }

        const string authUrl = "https://login.microsoftonline.com";
        const string graphApiUrl = "https://graph.windows.net";

        public virtual bool initialized {
            get { return (oauth_token != null && app_id != null); }
        }

        public void Initialize()
        {
            this.oauth_token = AcquireTokenForApplication();
        }

        public void Initialize(string TenantName, string AppId, string AppKey)
        {
            this.app_id = AppId;
            this.app_key = AppKey;
            this.tenant_name = TenantName;
            this.oauth_token = AcquireTokenForApplication();
        }

        public Token AcquireTokenForApplication()
        {
            string uri = string.Format("{0}/{1}/oauth2/token", authUrl, tenant_name);
            string contentType = "application/x-www-form-urlencoded";
            string content = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&resource={2}", app_id, HttpUtility.UrlEncode(app_key), HttpUtility.UrlEncode(graphApiUrl));

            APIService service = new APIService(uri, contentType);

            return service.Send<Token>(content);
        }

        public Task<Token> AcquireTokenForApplicationAsync()
        {
            string uri = string.Format("{0}/{1}/oauth2/token", authUrl, tenant_name);
            string contentType = "application/x-www-form-urlencoded";
            string content = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&resource={2}", app_id, HttpUtility.UrlEncode(app_key), HttpUtility.UrlEncode(graphApiUrl));

            APIService service = new APIService(uri, contentType);

            return service.SendAsync<Token>(content);
        }

        public List<Contract> GetContracts()
        {
            string uri = string.Format("{0}/{1}/contracts?api-version=1.6", graphApiUrl, tenant_name);

            APIService service = new APIService(uri);

            service.Request.Headers.Add("Authorization", "Bearer " + oauth_token.access_token);
            
            dynamic result = service.Get<dynamic>();
            
            return result.value.ToObject<List<Contract>>();
        }

        public Task<List<Contract>> GetContractsAsync()
        {
            string uri = string.Format("{0}/{1}/contracts?api-version=1.6", graphApiUrl, tenant_name);

            APIService service = new APIService(uri);

            service.Request.Headers.Add("Authorization", "Bearer " + oauth_token.access_token);

            dynamic result = service.GetAsync<dynamic>();

            return result.value.ToObject<List<Contract>>();
        }
    }
}
