using System;
using System.Net.Http;

namespace SQSHttpClient
{
    public class SQSHttpClient : HttpClient
    {
        public SQSHttpClient()
        {

        }

        public SQSHttpClient(HttpMessageHandler handler)
            : base(handler)
        {
            
        }

        public SQSHttpClient(HttpMessageHandler handler, bool disposeHandler)
            : base(handler, disposeHandler)
        {

        }
    }
}
