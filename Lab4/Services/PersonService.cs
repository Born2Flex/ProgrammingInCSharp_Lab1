using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                    return persons.Where(person => person.FirstName
                        .Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
                case "Last Name":
                    return persons.Where(person => person.LastName
                        .Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
                case "Email":
                    return persons.Where(person => person.Email
                        .Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
                case "Birth Date":
                    if (DateTime.TryParse(searchValue, out DateTime birthDate))
                        return persons.Where(person => person.BirthDate.Date == birthDate.Date).ToList();
                    throw new InvalidSearchValueException("Invalid date format. Please enter date in format: dd-MM-yyyy");
                case "Is Adult":
                    if (bool.TryParse(searchValue, out bool isAdult))
                        return persons.Where(person => person.IsAdult == isAdult).ToList();
                    throw new InvalidSearchValueException("Invalid bool format. Please enter true or false");
                case "Sun Sign":
                    return persons.Where(person => person.SunSign
                        .Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
                case "Chinese Sign":
                    return persons.Where(person => person.ChineseSign
                        .Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();
                case "Is Birthday":
                    if (bool.TryParse(searchValue, out bool isBirthday))
                        return persons.Where(person => person.IsBirthday == isBirthday).ToList();
                    throw new InvalidSearchValueException("Invalid bool format. Please enter true or false");
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}