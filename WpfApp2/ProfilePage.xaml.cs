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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        // Єдиний екземпляр сторінки користувача для використання у всьому додатку.
        private static ProfilePage instance;
        // Посилання на вікно авторизації для доступу до поточного користувача.
        AuthWindow authWindow = AuthWindow.Instance();
        // Поточний користувач, який відображається на сторінці.
        public User currentUser;
        private ProfilePage()
        {
            // Ініціалізація компонентів вікна.
            InitializeComponent();

            currentUser = authWindow.currentUser;

            // Закриття вікна авторизації.
            authWindow.Close();

            // Відображення даних користувача у списку.
            listOfUserData.ItemsSource = currentUser.GetStringProperties();

            // Перевірка наявності фото користувача.
            if (currentUser.PhotoPath != null)
            {
                userPhoto_Loaded();
            }
        }
        public static ProfilePage Instance()
        {
            if (instance == null)
            {
                instance = new ProfilePage();
            }
            return instance;
        }
        /// <summary>
        /// Обробник події завантаження зображення користувача.
        /// </summary>
        public void userPhoto_Loaded()
        {
            if (File.Exists(currentUser.PhotoPath))
            {
                // Створення нового зображення та його ініціалізація.
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(currentUser.PhotoPath, UriKind.Absolute);
                image.EndInit();
                // Встановлення зображення для фото користувача.
                userPhoto.Source = image;

                // Оновлення розмірів фото користувача.
                userPhoto.UpdateLayout();
            }
        }
        private void userPhoto_Loaded_1(object sender, RoutedEventArgs e)
        {
            // Налаштування форми фото користувача для круглого вигляду.
            if (userPhoto != null && userPhoto.Source != null)
            {
                var rect = new RectangleGeometry(
                    new Rect(0, 0, userPhoto.ActualWidth, userPhoto.ActualHeight));
                rect.RadiusX = rect.RadiusY = userPhoto.ActualWidth / 2; // Робимо фото круглим.
                userPhoto.Clip = rect;
            }
        }
    }
}
