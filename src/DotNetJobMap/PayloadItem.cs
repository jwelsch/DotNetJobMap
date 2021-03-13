using System;

namespace DotNetJobMap
{
    public interface IPayloadItem
    {
        object Value { get; }

        void Delete();

        bool IsDeleted { get; }
    }

    public class PayloadItem : IPayloadItem
    {
        public object Value { get; }

        public bool IsDeleted { get; private set; }

        public PayloadItem(object value)
        {
            Value = value;
        }

        public void Delete()
        {
            IsDeleted = true;

            if (Value != null && Value is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
