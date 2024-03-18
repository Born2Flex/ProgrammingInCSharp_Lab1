using System;

namespace KMA.ProgrammingInCSharp.Utils.Exceptions
{
    public class InvalidEmailException : Exception
    {
        public InvalidEmailException(string message) : 
            base(message)
        {}
    }
}