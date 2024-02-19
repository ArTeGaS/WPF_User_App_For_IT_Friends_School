using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Основне вікно програми. Реалізує інтерфейс реєстрації користувачів.
    /// </summary>
    public partial class MainWindow : Window
    {
        // Статичний екземпляр вікна для реалізації шаблону сінглтон
        private static MainWindow instance;
        // Список зареєстрованих користувачів
        public List<User> usersData = new List<User>();

        /// <summary>
        /// Приватний конструктор для реалізації шаблону сінглтон.
        /// </summary>
        private MainWindow()
        {
            // Ініціалізація компонентів вікна
            InitializeComponent();
            // Завантаження даних користувачів, якщо вони існують
            if (File.Exists("C://OOTDDir/Users.json"))
            {
                usersData = JSON_FileManager.LoadUsers("Users", "C://OOTDDir");
            }
        }

        /// <summary>
        /// Метод для отримання екземпляру MainWindow.
        /// </summary>
        /// <returns>Екземпляр MainWindow.</returns>
        public static MainWindow Instance()
        {
            if (instance == null)
            {
                instance = new MainWindow();
            }
            return instance;
        }

        /// <summary>
        /// Обробник натискання кнопки реєстрації.
        /// </summary>
        private void Reg_Button_Click(object sender, RoutedEventArgs e)
        {
            // Спрощення доступу до введених даних
            string login = loginTextBox.Text;
            string password = passwordTextBox.Password;
            string passwordCon = passwordConTextBox.Password;
            string email = emailTextBox.Text;

            // Флаги для перевірки правильності введених даних
            bool loginFlag = false;
            bool passwordFlag = false;
            bool passwordConFlag = false;
            bool emailFlag = false;

            // Перевірка правильності введеного логіна
            if (login.Length < 5 || login.Length > 16)
            {
                loginTextBox.ToolTip = "Логін не може бути менше 5 і більше 16 символів.";
                loginTextBox.Background = Brushes.LightPink;
            }
            else
            {
                loginTextBox.Background = Brushes.LightGray;
                loginTextBox.ToolTip = null;
                loginFlag = true;
            }

            // Перевірка правильності введеного пароля
            if (password.Length < 5 || password.Length > 16)
            {
                passwordTextBox.ToolTip = "Пароль не може бути менше 5 і більше 16 символів.";
                passwordTextBox.Background = Brushes.LightPink;
                passwordFlag = false;
            }
            else
            {
                passwordTextBox.Background = Brushes.LightGray;
                passwordTextBox.ToolTip = null;
                passwordFlag = true;
            }

            // Перевірка на співпадіння паролів
            if (passwordCon != password)
            {
                passwordConTextBox.ToolTip = "Паролі не співпадають.";
                passwordConTextBox.Background = Brushes.LightPink;
                passwordConFlag = false;
            }
            else
            {
                passwordConTextBox.Background = Brushes.LightGray;
                passwordConTextBox.ToolTip = null;
                passwordConFlag = true;
            }

            // Перевірка правильності введеної електронної пошти
            if (!email.Contains("@") || !email.Contains("."))
            {
                emailTextBox.ToolTip = "Адреса введена невірно.";
                emailTextBox.Background = Brushes.LightPink;
                emailFlag = false;
            }
            else
            {
                emailTextBox.Background = Brushes.LightGray;
                emailTextBox.ToolTip = null;
                emailFlag = true;
            }

            // Якщо всі дані введено правильно, створюємо та зберігаємо нового користувача
            if (loginFlag && passwordFlag && passwordConFlag && emailFlag)
            {
                User tempUser = new User(login, email, password);
                usersData.Add(tempUser);
                JSON_FileManager.SaveUsers("Users", "C://OOTDDir", usersData);
            }
        }

        /// <summary>
        /// Обробник натискання кнопки для переходу до вікна авторизації.
        /// </summary>
        private void ToAuth_Button_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = AuthWindow.Instance();
            authWindow.Show();
            // При закритті вікна авторизації закривається і головне вікно
            authWindow.Closed += Main_Window_Closed;
            Hide();
        }

        /// <summary>
        /// Обробник події закриття головного вікна.
        /// </summary>
        private void Main_Window_Closed(object sender, EventArgs e)
        {
            Close(); // Закриття головного вікна
        }
    }
}
