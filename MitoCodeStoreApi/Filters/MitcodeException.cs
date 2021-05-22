using System;

namespace MitoCodeStoreApi.Filters
{
    public class MitcodeException : Exception
    {
        public MitcodeException(string message)
            : base(message)
        {

        }
    }
}