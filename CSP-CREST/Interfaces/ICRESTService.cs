using CSP_CREST.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_CREST.Interfaces
{
    public interface ICRESTService
    {
        Token sa_token { get; set; }
        string oauth_token { get; set; }
        string reseller_id { get; set; }
        bool initialized { get; }
        void Initialize();
        void Initialize(string OAuthToken, string ResellerId);
        Token AcquireTokenForReseller();
        Task<Token> AcquireTokenForResellerAsync();
        Token AcquireTokenForCustomer(string customerId);
        Task<Token> AcquireTokenForCustomerAsync(string customerId);
        Customer GetReseller();
        Task<Customer> GetResellerAsync();
        Customer GetCustomer(string CustomerAADId);
        Task<Customer> GetCustomerAsync(string CustomerAADId);
        UsageRecord GetUsage(string SubscriptionId, bool showDetails = false);
        UsageRecord GetUsage(string SubscriptionId, DateTime StartDate, DateTime EndDate, string Granularity, bool ShowDetails, int Count);
        Task<UsageRecord> GetUsageAsync(string SubscriptionId, bool showDetails = false);
        Task<UsageRecord> GetUsageAsync(string SubscriptionId, DateTime StartDate, DateTime EndDate, string Granularity, bool ShowDetails, int Count);
        List<Entitlement> GetEntitlements(string CustomerId);
        List<Entitlement> GetEntitlements(string CustomerId, Token cus_token);
        Task<List<Entitlement>> GetEntitlementsAsync(string CustomerId);
        Task<List<Entitlement>> GetEntitlementsAsync(string CustomerId, Token cus_token);
        Subscription GetSubscription(string SubscriptionId);
        Task<Subscription> GetSubscriptionAsync(string SubscriptionId);
        List<Subscription> GetSubscriptionsByCustomer(string CustomerId);
        Task<List<Subscription>> GetSubscriptionsByCustomerAsync(string CustomerId);
        RateCard GetRateCard();
        Task<RateCard> GetRateCardAsync();
    }
}
