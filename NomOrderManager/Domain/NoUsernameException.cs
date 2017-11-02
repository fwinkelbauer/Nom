using System;

namespace NomOrderManager.Domain
{
    [Serializable]
    public class NoUsernameException : Exception
    {
        public NoUsernameException(string message)
            : base(message)
        {
        }
    }
}
