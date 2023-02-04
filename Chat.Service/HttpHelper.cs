using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Service
{
    public class HttpHelper
    {
        public static HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7004")
        };
    }
}
