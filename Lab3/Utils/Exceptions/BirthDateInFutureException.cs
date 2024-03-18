using System;

namespace KMA.ProgrammingInCSharp.Utils.Exceptions
{
    public class BirthDateInFutureException : Exception
    {
        public BirthDateInFutureException(string message, DateTime badDate) :
            base($"{message}. Date: {badDate:dd-MM-yyyy}")
        {}
    }
}