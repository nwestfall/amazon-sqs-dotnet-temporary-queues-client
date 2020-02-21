using System;
using System.Collections.Generic;

using Amazon.SQS.Model;

namespace Amazon.SQS
{
    public class MessageContent
    {
        readonly string _messageBody;
        readonly IDictionary<string, MessageAttributeValue> _messageAttributes;

        public string MessageBody
        {
            get => _messageBody;
        }
        public IDictionary<string, MessageAttributeValue> MessageAttributes
        {
            get => _messageAttributes;
        }

        public MessageContent(string messageBody)
        {
            _messageBody = messageBody;
            _messageAttributes = new Dictionary<string, MessageAttributeValue>();
        }

        public MessageContent(string messageBody, IDictionary<string, MessageAttributeValue> attributes)
        {
            _messageBody = messageBody;
            _messageAttributes = attributes;
        }

        public static MessageContent FromMessage(Message message)
            => new MessageContent(message.Body, message.MessageAttributes);

        public void AddMessageAttributesEntry(string key, MessageAttributeValue value)
            => _messageAttributes.Add(key, value);

        public SendMessageRequest ToSendMessageRequest()
        {
            var msg = new SendMessageRequest();
            msg.MessageBody = _messageBody;
            msg.MessageAttributes = new Dictionary<string, MessageAttributeValue>(_messageAttributes);
            return msg;
        }

        public SendMessageBatchRequestEntry ToSendMessageBatchRequestEntry()
        {
            var msg = new SendMessageBatchRequestEntry();
            msg.MessageBody = _messageBody;
            msg.MessageAttributes = new Dictionary<string, MessageAttributeValue>(_messageAttributes);
            return msg;
        }

        public Message ToMessage()
        {
            var msg = new Message();
            msg.Body = _messageBody;
            msg.MessageAttributes = new Dictionary<string, MessageAttributeValue>(_messageAttributes);
            return msg;
        }
    }
}
