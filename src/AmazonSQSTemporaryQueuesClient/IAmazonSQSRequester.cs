using System;
using System.Threading.Tasks;

using Amazon.SQS;
using Amazon.SQS.Model;

namespace Amazon.SQS
{
    public interface IAmazonSQSRequester
    {
        AmazonSQSClient GetAmazonSQS();

        public Message SendMessageAndGetResponse(SendMessageRequest request, int timeout);

        public Task<Message> SendMessageAndGetResponseAsync(SendMessageRequest request, int timeout);

        public void Shutdown();
    }
}