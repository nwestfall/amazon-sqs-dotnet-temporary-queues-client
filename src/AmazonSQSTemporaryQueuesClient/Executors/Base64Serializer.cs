using System;

namespace Amazon.SQS.Executors
{
    public class Base64Serializer : InvertibleFunction<byte[], string>
    {
        public override string Apply(byte[] t)
            => Convert.ToBase64String(t);

        public override byte[] Unapply(string r)
            => Convert.FromBase64String(r);
    }
}