using System;

namespace NomOrderManager.Domain
{
    [Serializable]
    public class CancelOrderException : Exception
    {
        public CancelOrderException(string message)
            : base(message)
        {
        }
    }
}
