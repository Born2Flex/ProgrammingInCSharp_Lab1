using System;

namespace KMA.ProgrammingInCSharp.Utils.Exceptions
{
    
    public class InvalidSearchValueException : Exception
    {
        public InvalidSearchValueException(string message) : 
            base(message)
        {}
    }
}