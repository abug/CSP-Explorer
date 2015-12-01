using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CSP_Foundation.Interfaces
{
    public interface IAPIService
    {
        HttpWebRequest Request { get; set; }
        T Get<T>();
        Task<T> GetAsync<T>();
        T Send<T>(string content);
        Task<T> SendAsync<T>(string content);
    }
}
