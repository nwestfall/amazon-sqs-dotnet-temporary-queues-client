using System;

using Amazon.SQS;

namespace Amazon.SQS
{
    public interface IAmazonSQSResponder
    {
        AmazonSQSClient GetAmazonSQS();

        public bool IsResponseMessageRequested(MessageContent requestMessage);

        public void SendResponseMessage(MessageContent requestMessage, MessageContent response);

        public void Shutdown();
    }
}