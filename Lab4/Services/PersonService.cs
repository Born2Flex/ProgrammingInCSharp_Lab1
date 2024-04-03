using KMA.ProgrammingInCSharp.Models;

namespace KMA.ProgrammingInCSharp.services
{
    internal class PersonService
    {
        private static Person _person;
        
        public static Person Person
        {
            get { return _person; }
            set { _person = value; }
        }
    }
}