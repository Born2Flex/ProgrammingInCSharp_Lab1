using System;

namespace KMA.ProgrammingInCSharp.Utils.Exceptions
{
    public class BirthDateInPastException : Exception
    {
        public BirthDateInPastException(string message, DateTime badDate) :
            base($"{message}. Date: {badDate:dd-MM-yyyy}")
        {}
    }
}