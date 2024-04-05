using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using KMA.ProgrammingInCSharp.Models;

namespace KMA.ProgrammingInCSharp.Repositories
{
    public class FileRepository
    {
        private static readonly string BaseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Lab4Shlapak");
        private static readonly string FilePath = Path.Combine(BaseFolder, "data.json");
        private static readonly int DefaultPersonsCount = 50;
        private List<Person> _users;
        
        private static FileRepository? _instance;
        private static readonly object Locker = new object();
        
        public event Action? DataChanged;
        
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
            if (!File.Exists(FilePath))
            {
                if (!Directory.Exists(BaseFolder))
                {
                    Directory.CreateDirectory(BaseFolder);
                }
                File.Create(FilePath).Close();
                GenerateDefaultData();
            }
            else
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            using StreamReader sr = new StreamReader(FilePath);
            string json = sr.ReadToEnd();
            _users = JsonSerializer.Deserialize<List<Person>>(json);
        }

        private void SaveData()
        {
            using (StreamWriter sw = new StreamWriter(FilePath))
            {
                string json = JsonSerializer.Serialize(_users);
                sw.Write(json);
            }
            DataChanged?.Invoke();
        }
        
        public List<Person> GetAllPersons()
        {
            return _users;
        }
        
        public void AddPerson(Person person)
        {
            _users.Add(person);
            SaveData();
        }
        
        public void DeletePerson(Person person)
        {
            _users.Remove(person);
            SaveData();
        }
        
        public void EditPerson(Person oldPerson, Person newPerson)
        {
            int index = _users.IndexOf(oldPerson);
            _users[index] = newPerson;
            SaveData();
        }
        
        private void GenerateDefaultData()
        {
            _users = new List<Person>();
            Random random = new Random();
            for (int i = 0; i < DefaultPersonsCount; ++i)
            {
                _users.Add(new Person("Name" + i, "Surname" + i, "email" + i + "@gmail.com",
                    new DateTime(random.Next(1980, 2024), random.Next(1, 12), random.Next(1, 28))));
            }
            SaveData();
        }
    }
}