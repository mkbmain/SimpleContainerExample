using System;

namespace ContainerExample
{
    public class NoRegistrationException : Exception
    {
        public NoRegistrationException(string message) : base(message)
        {
        }
    }
}