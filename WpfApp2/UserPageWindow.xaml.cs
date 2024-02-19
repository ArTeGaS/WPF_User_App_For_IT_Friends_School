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
    /// Логіка взаємодії для вікна користувацької сторінки.
    /// </summary>
    public partial class UserPageWindow : Window
    {
        // Єдиний екземпляр вікна сторінки користувача для використання у всьому додатку.
        private static UserPageWindow instance;
        // Посилання на вікно авторизації для доступу до поточного користувача.
        AuthWindow authWindow = AuthWindow.Instance();
        // Поточний користувач, який відображається на сторінці.
        public User currentUser;

        // Конструктор вікна сторінки користувача.
        private UserPageWindow()
        {
            // Ініціалізація компонентів вікна.
            InitializeComponent();

            // Отримання поточного користувача з вікна авторизації.
            currentUser = authWindow.currentUser;

            // Відображення даних користувача у списку.
            listOfUserData.ItemsSource = currentUser.GetStringProperties();

            // Закриття вікна авторизації.
            authWindow.Close();

            // Додавання елементів у список кнопок для навігації або дій.
            listOfButtons.Items.Add("Редагувати дані");
        }

        /// <summary>
        /// Отримує або створює екземпляр вікна сторінки користувача.
        /// </summary>
        /// <returns>Екземпляр UserPageWindow.</returns>
        public static UserPageWindow Instance()
        {
            if (instance == null)
            {
                instance = new UserPageWindow();
            }
            return instance;
        }

        /// <summary>
        /// Обробник події завантаження зображення користувача.
        /// </summary>
        private void userPhoto_Loaded(object sender, RoutedEventArgs e)
        {
            // Налаштування форми зображення користувача для круглого вигляду.
            var image = sender as Image;
            if (image != null && image.Source != null)
            {
                var rect = new RectangleGeometry(new Rect(0, 0, image.ActualWidth, image.ActualHeight));
                rect.RadiusX = rect.RadiusY = image.ActualWidth / 2; // Робимо зображення круглим.
                image.Clip = rect;
            }
        }

        /// <summary>
        /// Обробник зміни вибору у списку кнопок.
        /// </summary>
        private void listOfButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Виконання дій відповідно до вибору користувача у списку кнопок.
            ListView listView = sender as ListView;
            int selectedIndex = listView.SelectedIndex;
            switch (selectedIndex)
            {
                case 0: // Якщо вибрано "Редагувати дані".
                    UserPageFrame.Content = null; // Очищення поточного вмісту фрейму.
                    UserPageFrame.Navigate(ProfileEditingPage.Instance()); // Навігація до сторінки редагування профілю.
                    break;
            }
        }
    }
}
