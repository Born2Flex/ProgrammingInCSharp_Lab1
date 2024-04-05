using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using KMA.ProgrammingInCSharp.Models;

namespace KMA.ProgrammingInCSharp.Repositories
{
    internal class FileRepository
    {
        #region Fields
        
        private static readonly string BaseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Lab4Shlapak");
        private static readonly string FilePath = Path.Combine(BaseFolder, "data.json");
        private static readonly int DefaultPersonsCount = 50;
        private List<Person> _persons;
        
        private static FileRepository? _instance;
        private static readonly object Locker = new object();
        
        public event Action? DataChanged;
        
        #endregion

        public static FileRepository Instance
        {
            get
            {
                if (_instance != null)
                {
                    return _instance;
                }
                lock (Locker)
                {
                    return _instance ??= new FileRepository();
                }
            }
        }

        private FileRepository()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (File.Exists(FilePath))
            {
                using StreamReader sr = new StreamReader(FilePath);
                string json = sr.ReadToEnd();
                _persons = JsonSerializer.Deserialize<List<Person>>(json) ?? new List<Person>();
            }
            else
            {
                if (!Directory.Exists(BaseFolder))
                {
                    Directory.CreateDirectory(BaseFolder);
                }
                File.Create(FilePath).Close();
                _persons = GenerateDefaultData();
                SaveDataAsync();
            }
        }

        private async void SaveDataAsync()
        {
            await using (StreamWriter sw = new StreamWriter(FilePath))
            {
                string json = JsonSerializer.Serialize(_persons);
                await sw.WriteAsync(json);
            }
            DataChanged?.Invoke();
        }
        
        public List<Person> GetAllPersons()
        {
            return _persons;
        }
        
        public void AddPerson(Person person)
        {
            _persons.Add(person);
            SaveDataAsync();
        }
        
        public void DeletePerson(Person person)
        {
            _persons.Remove(person);
            SaveDataAsync();
        }
        
        public void EditPerson(Person oldPerson, Person newPerson)
        {
            int index = _persons.IndexOf(oldPerson);
            _persons[index] = newPerson;
            SaveDataAsync();
        }
        
        private List<Person> GenerateDefaultData()
        {
            List<Person> data = new List<Person>();
            Random random = new Random();
            for (int i = 0; i < DefaultPersonsCount; ++i)
            {
                data.Add(new Person("Name" + i, "Surname" + i, "email" + i + "@gmail.com",
                    new DateTime(random.Next(1980, 2024), random.Next(1, 12), random.Next(1, 28))));
            }
            return data;
        }
    }
}