using System;
using System.Collections.Generic;
using System.Linq;
using KMA.ProgrammingInCSharp.Models;
using KMA.ProgrammingInCSharp.Repositories;
using KMA.ProgrammingInCSharp.Utils.Exceptions;

namespace KMA.ProgrammingInCSharp.services
{
    internal class PersonService
    {
        private static readonly FileRepository Repository = FileRepository.Instance;
        
        public List<Person> GetAllPersons()
        {
            return Repository.GetAllPersons();
        }
        
        public void AddPerson(Person person)
        {
            Repository.AddPerson(person);
        }
        
        public void DeletePerson(Person person)
        {
            Repository.DeletePerson(person);
        }
        
        public void EditPerson(Person oldPerson, Person newPerson)
        {
            Repository.EditPerson(oldPerson, newPerson);
        }

        public List<Person> SearchPersons(string selectedOption, string searchValue)
        {
            List<Person> persons = Repository.GetAllPersons();

            switch (selectedOption)
            {
                case "First Name":
                    return SearchByPredicate(person => person.FirstName.Contains(searchValue, StringComparison.OrdinalIgnoreCase));
                case "Last Name":
                    return SearchByPredicate(person => person.LastName.Contains(searchValue, StringComparison.OrdinalIgnoreCase));
                case "Email":
                    return SearchByPredicate(person => person.Email.Contains(searchValue, StringComparison.OrdinalIgnoreCase));
                case "Birth Date":
                    if (DateTime.TryParse(searchValue, out DateTime birthDate))
                        return SearchByPredicate(person => person.BirthDate.Date == birthDate.Date);
                    throw new InvalidSearchValueException("Invalid date format. Please enter date in format: dd-MM-yyyy");
                case "Is Adult":
                    if (bool.TryParse(searchValue, out bool isAdult))
                        return SearchByPredicate(person => person.IsAdult == isAdult);
                    throw new InvalidSearchValueException("Invalid bool format. Please enter true or false");
                case "Sun Sign":
                    return SearchByPredicate(person => person.SunSign.Contains(searchValue, StringComparison.OrdinalIgnoreCase));
                case "Chinese Sign":
                    return SearchByPredicate(person => person.ChineseSign.Contains(searchValue, StringComparison.OrdinalIgnoreCase));
                case "Is Birthday":
                    if (bool.TryParse(searchValue, out bool isBirthday))
                        return SearchByPredicate(person => person.IsBirthday == isBirthday);
                    throw new InvalidSearchValueException("Invalid bool format. Please enter true or false");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private List<Person> SearchByPredicate(Func<Person, bool> predicate)
        {
            return Repository.GetAllPersons().Where(predicate).ToList();
        }
    }
}