using System;

namespace CodeBase.API
{
    public class APIException : Exception
    {
        public APIException(string message) : base(message) { }
    }
}