using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp2
{
    /// <summary>
    /// Клас, що представляє користувача в системі.
    /// </summary>
    public class User
    {
        // Приватні поля для зберігання інформації про користувача.
        private string login, password, email, firstName, lastName, photoPath;

        // Властивості для доступу до даних користувача.
        public string Login
        {
            get { return login; }
            set { login = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string PhotoPath
        {
            get { return photoPath; }
            set { photoPath = value; }
        }

        // Конструктор за замовчуванням.
        public User() { }

        /// <summary>
        /// Конструктор для створення нового користувача з основними даними.
        /// </summary>
        /// <param name="login">Логін користувача.</param>
        /// <param name="email">Електронна адреса користувача.</param>
        /// <param name="password">Пароль користувача.</param>
        public User(string login, string email, string password)
        {
            this.login = login;
            this.email = email;
            this.password = password;
        }

        /// <summary>
        /// Отримує всі рядкові властивості об'єкта та їх значення.
        /// </summary>
        /// <returns>Список рядків, де кожен рядок містить ім'я властивості та її значення.</returns>
        public List<string> GetStringProperties()
        {
            List<string> stringProperties = new List<string>();
            // Отримуємо інформацію про всі властивості об'єкта.
            PropertyInfo[] properties = this.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                // Якщо тип властивості - рядок, додаємо її ім'я та значення до списку.
                if (property.PropertyType == typeof(string))
                {
                    string value = property.GetValue(this) as string;
                    stringProperties.Add($"{property.Name}: {value}");
                }
            }
            // Видаляємо логін та пароль зі списку, залишаючи лише особисті дані.
            stringProperties.RemoveRange(0, 2);
            return stringProperties;
        }
    }
}
