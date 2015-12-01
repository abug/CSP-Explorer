using CSP_CREST.Entities;
using CSP_CREST.Interfaces;
using CSP_Foundation.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_CREST.Services
{
    public class CRESTService : ICRESTService
    {
        public Token sa_token { get; set; }
        public string oauth_token { get; set; }
        public string reseller_id { get; set; }

        const string cspApiUrl = "https://api.cp.microsoft.com";

        public virtual bool initialized
        {
            get { return (sa_token != null && oauth_token != null); }
        }

        public void Initialize()
        {
            this.sa_token = AcquireTokenForReseller();
        }

        public void Initialize(string OAuthToken, string ResellerId)
        {
            this.oauth_token = OAuthToken;
            this.reseller_id = ResellerId;
            this.sa_token = AcquireTokenForReseller();
        }

        public Token AcquireTokenForReseller()
        {
            string uri = string.Format("{0}/{1}", cspApiUrl, "my-org/tokens");
            string contentType = "application/x-www-form-urlencoded";
            string content = "grant_type=client_credentials";

            APIService service = new APIService(uri, contentType);

            service.Request.Headers.Add("api-version", "2015-03-31");
            service.Request.Headers.Add("x-ms-correlation-id", Guid.NewGuid().ToString());
            service.Request.Headers.Add("Authorization", "Bearer " + oauth_token);

            return service.Send<Token>(content);
        }

        public Task<Token> AcquireTokenForResellerAsync()
        {
            string uri = string.Format("{0}/{1}", cspApiUrl, "my-org/tokens");
            string contentType = "application/x-www-form-urlencoded";
            string content = "grant_type=client_credentials";

            APIService service = new APIService(uri, contentType);

            service.Request.Headers.Add("api-version", "2015-03-31");
            service.Request.Headers.Add("x-ms-correlation-id", Guid.NewGuid().ToString());
            service.Request.Headers.Add("Authorization", "Bearer " + oauth_token);

            return service.SendAsync<Token>(content);
        }

        public Token AcquireTokenForCustomer(string customerId)
        {
            string uri = string.Format("{0}/{1}/tokens", cspApiUrl, customerId);
            string contentType = "application/x-www-form-urlencoded";
            string content = "grant_type=client_credentials";

            APIService service = new APIService(uri, contentType);

            service.Request.Headers.Add("api-version", "2015-03-31");
            service.Request.Headers.Add("x-ms-correlation-id", Guid.NewGuid().ToString());
            service.Request.Headers.Add("Authorization", "Bearer " + oauth_token);

            return service.Send<Token>(content);
        }

        public Task<Token> AcquireTokenForCustomerAsync(string customerId)
        {
            string uri = string.Format("{0}/{1}/tokens", cspApiUrl, customerId);
            string contentType = "application/x-www-form-urlencoded";
            string content = "grant_type=client_credentials";

            APIService service = new APIService(uri, contentType);

            service.Request.Headers.Add("api-version", "2015-03-31");
            service.Request.Headers.Add("x-ms-correlation-id", Guid.NewGuid().ToString());
            service.Request.Headers.Add("Authorization", "Bearer " + oauth_token);

            return service.SendAsync<Token>(content);
        }

        public Customer GetReseller()
        {
            string uri = string.Format("{0}/customers/get-by-identity?provider=AAD&type=tenant&tid={1}", cspApiUrl, reseller_id);

            APIService service = new APIService(uri);

            service.Request.Headers.Add("api-version", "2015-03-31");
            service.Request.Headers.Add("x-ms-correlation-id", Guid.NewGuid().ToString());
            service.Request.Headers.Add("x-ms-tracking-id", Guid.NewGuid().ToString());
            service.Request.Headers.Add("Authorization", "Bearer " + sa_token.access_token);

            return service.Get<Customer>();
        }

        public Task<Customer> GetResellerAsync()
        {
            string uri = string.Format("{0}/customers/get-by-identity?provider=AAD&type=tenant&tid={1}", cspApiUrl, reseller_id);

            APIService service = new APIService(uri);

            service.Request.Headers.Add("api-version", "2015-03-31");
            service.Request.Headers.Add("x-ms-correlation-id", Guid.NewGuid().ToString());
            service.Request.Headers.Add("x-ms-tracking-id", Guid.NewGuid().ToString());
            service.Request.Headers.Add("Authorization", "Bearer " + sa_token.access_token);

            return service.GetAsync<Customer>();
        }

        public Customer GetCustomer(string CustomerAADId)
        {
            string[] args = new string[] { cspApiUrl, CustomerAADId, reseller_id};
            string uri = string.Format("{0}/customers/get-by-identity?provider=AAD&type=external_group&tid={1}&etid={2}", args);

            APIService service = new APIService(uri);

            service.Request.Headers.Add("api-version", "2015-03-31");
            service.Request.Headers.Add("x-ms-correlation-id", Guid.NewGuid().ToString());
            service.Request.Headers.Add("x-ms-tracking-id", Guid.NewGuid().ToString());
            service.Request.Headers.Add("Authorization", "Bearer " + sa_token.access_token);

            return service.Get<Customer>();
        }

        public Task<Customer> GetCustomerAsync(string CustomerAADId)
        {
            string[] args = new string[] { cspApiUrl, CustomerAADId, reseller_id };
            string uri = string.Format("{0}/customers/get-by-identity?provider=AAD&type=external_group&tid={1}&etid={2}", args);

            APIService service = new APIService(uri);

            service.Request.Headers.Add("api-version", "2015-03-31");
            service.Request.Headers.Add("x-ms-correlation-id", Guid.NewGuid().ToString());
            service.Request.Headers.Add("x-ms-tracking-id", Guid.NewGuid().ToString());
            service.Request.Headers.Add("Authorization", "Bearer " + sa_token.access_token);

            return service.GetAsync<Customer>();
        }

        public UsageRecord GetUsage(string SubscriptionId, bool showDetails = false)
        {
            return GetUsage(SubscriptionId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), "daily", showDetails, 1000);
        }

        public UsageRecord GetUsage(string SubscriptionId, DateTime StartDate, DateTime EndDate, string Granularity, bool ShowDetails, int Count)
        {
            string[] args = new string[] { cspApiUrl, reseller_id, SubscriptionId, StartDate.ToString("yyyy-MM-dd HH:mm:ssZ"), EndDate.ToString("yyyy-MM-dd HH:mm:ssZ"), Granularity, ShowDetails.ToString(), Count.ToString() };
            string uri = string.Format("{0}/{1}/usage-records?entitlement_id={2}&reported_start_time={3}&reported_end_time={4}&granularity={5}&show_details={6}&count={7}", args);

            APIService service = new APIService(uri);

            service.Request.Headers.Add("api-version", "2015-03-31");
            service.Request.Headers.Add("x-ms-correlation-id", Guid.NewGuid().ToString());
            service.Request.Headers.Add("Authorization", "Bearer " + sa_token.access_token);

            return service.Get<UsageRecord>();
        }

        public Task<UsageRecord> GetUsageAsync(string SubscriptionId, bool showDetails = false)
        {
            return GetUsageAsync(SubscriptionId, new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day), "daily", showDetails, 1000);
        }

        public Task<UsageRecord> GetUsageAsync(string SubscriptionId, DateTime StartDate, DateTime EndDate, string Granularity, bool ShowDetails, int Count)
        {
            string[] args = new string[] { cspApiUrl, reseller_id, SubscriptionId, StartDate.ToString("yyyy-MM-dd HH:mm:ssZ"), EndDate.ToString("yyyy-MM-dd HH:mm:ssZ"), Granularity, ShowDetails.ToString(), Count.ToString() };
            string uri = string.Format("{0}/{1}/usage-records?entitlement_id={2}&reported_start_time={3}&reported_end_time={4}&granularity={5}&show_details={6}&count={7}", args);

            APIService service = new APIService(uri);

            service.Request.Headers.Add("api-version", "2015-03-31");
            service.Request.Headers.Add("x-ms-correlation-id", Guid.NewGuid().ToString());
            service.Request.Headers.Add("Authorization", "Bearer " + sa_token.access_token);

            return service.GetAsync<UsageRecord>();
        }

        public List<Entitlement> GetEntitlements(string CustomerId)
        {
            Token cus_token = AcquireTokenForCustomer(CustomerId);

            return GetEntitlements(CustomerId, cus_token);
        }

        public List<Entitlement> GetEntitlements(string CustomerId, Token cus_token)
        {
            string uri = string.Format("{0}/{1}/Entitlements", cspApiUrl, CustomerId);

            APIService service = new APIService(uri);

            service.Request.Headers.Add("api-version", "2015-03-31");
            service.Request.Headers.Add("x-ms-correlation-id", Guid.NewGuid().ToString());
            service.Request.Headers.Add("Authorization", "Bearer " + cus_token.access_token);

            dynamic result = service.Get<dynamic>();

            return result.items.ToObject<List<Entitlement>>();
        }

        public Task<List<Entitlement>> GetEntitlementsAsync(string CustomerId)
        {
            Token cus_token = AcquireTokenForCustomer(CustomerId);

            return GetEntitlementsAsync(CustomerId, cus_token);
        }

        public Task<List<Entitlement>> GetEntitlementsAsync(string CustomerId, Token cus_token)
        {
            string uri = string.Format("{0}/{1}/Entitlements", cspApiUrl, CustomerId);

            APIService service = new APIService(uri);

            service.Request.Headers.Add("api-version", "2015-03-31");
            service.Request.Headers.Add("x-ms-correlation-id", Guid.NewGuid().ToString());
            service.Request.Headers.Add("Authorization", "Bearer " + cus_token.access_token);

            dynamic result = service.GetAsync<dynamic>();

            return result.items.ToObject<List<Entitlement>>();
        }

        public Subscription GetSubscription(string SubscriptionId)
        {
            string[] args = new string[] { cspApiUrl, reseller_id, SubscriptionId };
            string uri = string.Format("{0}/{1}/subscriptions/{2}", args);

            APIService service = new APIService(uri);

            service.Request.Headers.Add("api-version", "2015-03-31");
            service.Request.Headers.Add("x-ms-correlation-id", Guid.NewGuid().ToString());
            service.Request.Headers.Add("Authorization", "Bearer " + sa_token.access_token);

            return service.Get<Subscription>();
        }

        public Task<Subscription> GetSubscriptionAsync(string SubscriptionId)
        {
            string[] args = new string[] { cspApiUrl, reseller_id, SubscriptionId };
            string uri = string.Format("{0}/{1}/subscriptions/{2}", args);

            APIService service = new APIService(uri);

            service.Request.Headers.Add("api-version", "2015-03-31");
            service.Request.Headers.Add("x-ms-correlation-id", Guid.NewGuid().ToString());
            service.Request.Headers.Add("Authorization", "Bearer " + sa_token.access_token);

            return service.GetAsync<Subscription>();
        }

        public List<Subscription> GetSubscriptionsByCustomer(string CustomerId)
        {
            string[] args = new string[] { cspApiUrl, reseller_id, CustomerId };
            string uri = string.Format("{0}/{1}/subscriptions?recipient_customer_id={2}", args);

            APIService service = new APIService(uri);

            service.Request.Headers.Add("api-version", "2015-03-31");
            service.Request.Headers.Add("x-ms-correlation-id", Guid.NewGuid().ToString());
            service.Request.Headers.Add("Authorization", "Bearer " + sa_token.access_token);

            dynamic result = service.Get<dynamic>();

            return result.items.ToObject<List<Subscription>>();
        }

        public Task<List<Subscription>> GetSubscriptionsByCustomerAsync(string CustomerId)
        {
            string[] args = new string[] { cspApiUrl, reseller_id, CustomerId };
            string uri = string.Format("{0}/{1}/subscriptions?recipient_customer_id={2}", args);

            APIService service = new APIService(uri);

            service.Request.Headers.Add("api-version", "2015-03-31");
            service.Request.Headers.Add("x-ms-correlation-id", Guid.NewGuid().ToString());
            service.Request.Headers.Add("Authorization", "Bearer " + sa_token.access_token);

            dynamic result = service.GetAsync<dynamic>();

            return result.items.ToObject<List<Subscription>>();
        }

        public RateCard GetRateCard()
        {
            string uri = string.Format("{0}/{1}/rate-card?OfferDurableId=MS-AZR-0145P", cspApiUrl, reseller_id);

            APIService service = new APIService(uri);

            service.Request.Headers.Add("api-version", "2015-03-31");
            service.Request.Headers.Add("x-ms-correlation-id", Guid.NewGuid().ToString());
            service.Request.Headers.Add("Authorization", "Bearer " + sa_token.access_token);

            return service.Get<RateCard>();
        }

        public Task<RateCard> GetRateCardAsync()
        {
            string uri = string.Format("{0}/{1}/rate-card?OfferDurableId=MS-AZR-0145P", cspApiUrl, reseller_id);

            APIService service = new APIService(uri);

            service.Request.Headers.Add("api-version", "2015-03-31");
            service.Request.Headers.Add("x-ms-correlation-id", Guid.NewGuid().ToString());
            service.Request.Headers.Add("Authorization", "Bearer " + sa_token.access_token);

            return service.GetAsync<RateCard>();
        }
    }
}
