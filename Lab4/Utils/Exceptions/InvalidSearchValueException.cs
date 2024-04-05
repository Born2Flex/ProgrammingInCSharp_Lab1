using System;

namespace KMA.ProgrammingInCSharp.Utils.Exceptions
{
    
    internal class InvalidSearchValueException : Exception
    {
        public InvalidSearchValueException(string message) : 
            base(message)
        {}
    }
}