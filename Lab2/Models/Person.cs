using System;
using KMA.ProgrammingInCSharp.Utils;

namespace KMA.ProgrammingInCSharp.Models;
// Створіть клас, який називається Person. Додайте до класу Person наступні 
//
// властивості:
//
// Ім'я
// Прізвище
//     Адреса електронної пошти
// Дата народження
//
// Додайте конструктори, які приймають наступні параметри (коди в конструкторах не повинен дублюватись):
//
// Всі чотири параметри.
//     Ім’я, прізвище, адреса електронної пошти.
//     Ім’я, прізвище, дата народження
//
// Додайте властивості лише для читання, які повертають наступну наперед обчислену інформацію (обчислення не повинні відбуватись беспосередньо в get, а сам get повинен просто повернути значення):
//
// IsAdult - який повертає true, якщо особа старше 18 років
// SunSign - традиційний західний сонячний знак цієї людини
// ChineseSign - китайський астрологічний знак цієї людини
// IsBirthday – який повертає true, якщо сьогодні день народження людини
//
//     Створіть вікно в яке користувач зможе ввести ім’я, прізвище, електронну адресу, дату народження. Додайте кнопку Proceed. Кнопка повинна бути неактивною, якщо хоча б одне поле не заповнене. 
//
//     Після натискання кнопки, повинні бути виконані перевірки 3 та 5 з Лабораторної роботи 1, а також описані вище обчислення. Якщо перевірки пройшли успішно, вивести значення всіх 8-ми полів 
// класу.
//     Обчислення повинні відбуватись асинхронно. Інтерфейс повинен блокуватись доки виконується асинхронна операція.
//
//     Правила виконання роботи:
//
// Потрібно по максимум приховувати компоненти. Public використовувати лише там, де це необхідно.
//     Потрібно використовувати асинхронність в всіх місцях де можуть бути потенційні затримки часу виконання (команда Proceed)
// Дотримуйтесь правил описаних в попередній роботі

internal class Person
{
    #region Fields
    private string _firstName;
    private string _lastName;
    private string _email;
    private DateTime _birthDate;
    
    private readonly bool _isAdult;
    private readonly string _sunSign;
    private readonly string _chineseSign;
    private readonly bool _isBirthday;
    #endregion
    
    #region Properties
    public string FirstName
    {
        get { return _firstName; }
        set { _firstName = value; }
    }
    public string LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }
    public string? Email
    {
        get { return _email; }
        set { _email = value; }
    }
    public DateTime BirthDate
    {
        get { return _birthDate; }
        set { _birthDate = value; }
    }
    
    public bool IsAdult
    {
        get { return _isAdult; }
    }
    public string SunSign
    {
        get { return _sunSign; }
    }
    public string ChineseSign
    {
        get { return _chineseSign; }
    }
    public bool IsBirthday
    {
        get { return _isBirthday; }
    }
    #endregion

    public Person(string firstName, string lastName, string email, DateTime birthDate)
    {
        _firstName = firstName;
        _lastName = lastName;
        _email = email;
        _birthDate = birthDate;
        
        _isAdult = DateUtils.YearsDiff(_birthDate, DateTime.Today) >= 18;
        _sunSign = DateUtils.GetSunSign(_birthDate);
        _chineseSign = DateUtils.GetChineseZodiacSign(_birthDate);
        _isBirthday = DateUtils.TodayIsBirthday(_birthDate);
    }
    
    public Person(string firstName, string lastName, string email): 
        this(firstName, lastName, email, DateTime.Today)
    {}

    public Person(string firstName, string lastName, DateTime birthDate): 
        this(firstName, lastName, string.Empty, birthDate)
    {}
}