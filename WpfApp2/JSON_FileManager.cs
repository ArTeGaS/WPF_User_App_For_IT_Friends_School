using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Windows;

namespace WpfApp2
{
    /// <summary>
    /// Клас для роботи з файлами JSON, зокрема для збереження та завантаження даних користувачів.
    /// </summary>
    internal class JSON_FileManager
    {
        /// <summary>
        /// Завантажує список користувачів з файлу JSON.
        /// </summary>
        /// <param name="fileName">Ім'я файлу без розширення.</param>
        /// <param name="path">Шлях до директорії з файлом.</param>
        /// <returns>Список користувачів.</returns>
        public static List<User> LoadUsers(string fileName, string path)
        {
            List<User> data = new List<User>();

            // Створення повного шляху до файлу.
            string filePath = Path.Combine(path, fileName + ".json");

            // Перевірка наявності файлу.
            if (File.Exists(filePath))
            {
                // Читання тексту з файлу.
                string jsonText = File.ReadAllText(filePath);
                // Десеріалізація тексту у список користувачів.
                data = JsonConvert.DeserializeObject<List<User>>(jsonText);
            }
            else
            {
                // Повідомлення про відсутність файлу.
                MessageBox.Show("File not found: " + filePath);
            }

            return data;
        }

        /// <summary>
        /// Зберігає список користувачів у файл JSON.
        /// </summary>
        /// <param name="fileName">Ім'я файлу без розширення.</param>
        /// <param name="locPath">Шлях до директорії для збереження файлу.</param>
        /// <param name="data">Список користувачів для збереження.</param>
        public static void SaveUsers(string fileName, string locPath, List<User> data)
        {
            // Серіалізація списку користувачів у формат JSON.
            string jsonText = JsonConvert.SerializeObject(data, Formatting.Indented);

            // Створення повного шляху до файлу.
            string filePath = Path.Combine(locPath, fileName + ".json");

            // Запис серіалізованих даних у файл.
            File.WriteAllText(filePath, jsonText);
        }
    }
}
