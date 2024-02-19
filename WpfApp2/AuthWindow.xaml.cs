using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Логіка взаємодії для вікна авторизації користувачів.
    /// </summary>
    public partial class AuthWindow : Window
    {
        // Єдиний екземпляр вікна авторизації для використання у всьому додатку.
        private static AuthWindow instance;
        // Посилання на головне вікно для доступу до даних користувачів.
        private MainWindow mainWindow = MainWindow.Instance();
        // Поточний користувач, який пройшов авторизацію.
        public User currentUser;

        // Конструктор вікна авторизації.
        private AuthWindow()
        {
            // Ініціалізація компонентів вікна.
            InitializeComponent();
        }

        /// <summary>
        /// Отримує або створює екземпляр вікна авторизації.
        /// </summary>
        /// <returns>Екземпляр AuthWindow.</returns>
        public static AuthWindow Instance()
        {
            if (instance == null)
            {
                instance = new AuthWindow();
            }
            return instance;
        }

        /// <summary>
        /// Обробник натискання кнопки авторизації.
        /// </summary>
        private void Auth_Button_Click(object sender, RoutedEventArgs e)
        {
            // Отримання даних з форми та їх нормалізація.
            string login = loginTextBox.Text.Substring(0, 1).ToUpper() + loginTextBox.Text.Substring(1).ToLower();
            string password = passwordTextBox.Password;

            // Флаги для перевірки коректності введених даних.
            bool loginFlag = false;
            bool passwordFlag = false;

            // Перевірка логіна на відповідність вимогам.
            if (login.Length < 5 || login.Length > 16)
            {
                // Якщо логін не відповідає вимогам, виділяємо поле червоним.
                loginTextBox.ToolTip = "Логін не може бути менше 5 і більше 16 символів.";
                loginTextBox.Background = Brushes.LightPink;
            }
            else
            {
                // Якщо вимогам відповідає, скидаємо виділення.
                loginTextBox.Background = Brushes.White;
                loginTextBox.ToolTip = null;
                loginFlag = true;
            }

            // Аналогічна перевірка для пароля.
            if (password.Length < 5 || password.Length > 16)
            {
                passwordTextBox.ToolTip = "Пароль не може бути менше 5 і більше 16 символів.";
                passwordTextBox.Background = Brushes.LightPink;
            }
            else
            {
                passwordTextBox.Background = Brushes.White;
                passwordTextBox.ToolTip = null;
                passwordFlag = true;
            }

            // Якщо логін і пароль відповідають вимогам, спробуємо знайти користувача.
            if (loginFlag && passwordFlag)
            {
                foreach (User user in mainWindow.usersData)
                {
                    // Якщо знайдено користувача з вказаним логіном.
                    if (loginTextBox.Text == user.Login)
                    {
                        TryToAuth(user);
                        break;
                    }
                    else
                    {
                        // Якщо користувача з таким логіном не існує, виділяємо поле червоним.
                        loginTextBox.Background = Brushes.LightPink;
                        loginTextBox.ToolTip = "Логіна не існує";
                    }
                }
            }
        }

        /// <summary>
        /// Спроба авторизації користувача.
        /// </summary>
        /// <param name="user">Користувач для авторизації.</param>
        void TryToAuth(User user)
        {
            // Перевірка пароля.
            if (user.Password == passwordTextBox.Password)
            {
                // Якщо пароль вірний, показуємо вікно користувача.
                MessageBox.Show(user.Login + ", " + user.Password);

                currentUser = user;

                UserPageWindow userPageWin = UserPageWindow.Instance();
                userPageWin.Show();
                Hide();
            }
            else
            {
                // Якщо пароль невірний, виділяємо поле червоним.
                passwordTextBox.Background = Brushes.LightPink;
                passwordTextBox.ToolTip = "Невірно введений пароль";
            }
        }

        /// <summary>
        /// Обробник натискання кнопки переходу до вікна реєстрації.
        /// </summary>
        private void ToReg_Button_Click(object sender, RoutedEventArgs e)
        {
            // Показуємо головне вікно і закриваємо поточне.
            mainWindow.Show();
            mainWindow.Closed += Auth_Window_Closed;
            Hide();
        }

        /// <summary>
        /// Обробник події закриття вікна авторизації.
        /// </summary>
        private void Auth_Window_Closed(object sender, EventArgs e)
        {
            // Закриття вікна авторизації.
            Close();
        }
    }
}
