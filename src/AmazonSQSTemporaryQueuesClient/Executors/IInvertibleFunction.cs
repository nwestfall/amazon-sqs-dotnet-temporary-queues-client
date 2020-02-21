using System;

namespace Amazon.SQS.Executors
{
    public interface IInvertibleFunction<T, R>
    {
        R Apply(T t);

        T Unapply(R r);
    }

    public abstract class InvertibleFunction<T, R> : IInvertibleFunction<T, R>
    {
        public abstract R Apply(T t);

        public abstract T Unapply(R r);

        InvertibleFunction<R, T> Invserse()
            => throw new NotSupportedException();

        InvertibleFunction<U, R> Compose<U>(InvertibleFunction<U, T> before)
            => throw new NotSupportedException();
    }
}