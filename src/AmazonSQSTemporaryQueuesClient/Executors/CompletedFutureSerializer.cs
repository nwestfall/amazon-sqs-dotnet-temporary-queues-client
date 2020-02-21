using System;
using System.Threading;
using System.Threading.Tasks;

namespace Amazon.SQS.Executors
{
    public class CompletedFutureSerializer<T> : InvertibleFunction<Task<T>, string>
    {
        const char NORMAL_VALUE_PREFIX = '.';
        const char CANCELLED_PREFIX = 'C';
        const char EXCEPTION_PREFIX = 'E';

        readonly IInvertibleFunction<T, string> _resultSerializer;
        readonly IInvertibleFunction<Exception, string> _exceptionSerializer;

        public CompletedFutureSerializer(IInvertibleFunction<T, string> resultSerializer, IInvertibleFunction<Exception, string> exceptionSerializer)
        {
            _resultSerializer = resultSerializer;
            _exceptionSerializer = exceptionSerializer;
        }

        public override string Apply(Task<T> t)
        {
            if(!t.IsCompleted)
                throw new ArgumentException();
            try
            {
                return NORMAL_VALUE_PREFIX + _resultSerializer.Apply(t.Result);
            }
            catch(TaskCanceledException)
            {
                return char.ToString(CANCELLED_PREFIX);
            }
            catch(OperationCanceledException)
            {
                throw new InvalidOperationException();
            }
            catch(Exception e)
            {
                return EXCEPTION_PREFIX + _exceptionSerializer.Apply(e);
            }
        }

        public override Task<T> Unapply(string r)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Task<T> t;
            string serializedValue = r.Substring(1, r.Length);
            switch(serializedValue[0])
            {
                case NORMAL_VALUE_PREFIX:
                    t = Task.Run(() => _resultSerializer.Unapply(r));
                    break;
                case CANCELLED_PREFIX:
                    
                    t = Task.FromCanceled(cts.Token);
                    break;
                case EXCEPTION_PREFIX:
                    t = Task.FromException(_exceptionSerializer.Unapply(serializedValue));
                    break;
            }
        }
    }
}