using CSP_Graph.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSP_Graph.Interfaces
{
    public interface IGraphService
    {
        bool initialized { get; }
        Token oauth_token { get; set; }
        string app_id { get; set; }
        string app_key { get; set; }
        string tenant_name { get; set; }
        void Initialize();
        void Initialize(string TenantName, string AppId, string AppKey);
        Token AcquireTokenForApplication();
        Task<Token> AcquireTokenForApplicationAsync();
        List<Contract> GetContracts();
        Task<List<Contract>> GetContractsAsync();
    }
}
