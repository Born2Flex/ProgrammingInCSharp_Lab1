using System;
using KMA.ProgrammingInCSharp.Models;

namespace KMA.ProgrammingInCSharp.Managers
{
    internal class PersonManager
    {
        private static Person? _currentPerson;
        public static Person? CurrentPerson
        {
            get { return _currentPerson; }
            set
            {
                if (_currentPerson != value)
                {
                    _currentPerson = value;
                    OnCurrentPersonChanged();
                }
            }
        }

        public static event Action? CurrentPersonChanged;

        private static void OnCurrentPersonChanged()
        {
            CurrentPersonChanged?.Invoke();
        }
    }
}