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

namespace WpfApp2
{
    /// <summary>
    /// Логіка взаємодії для сторінки редагування профілю.
    /// </summary>
    public partial class ProfileEditingPage : Page
    {
        // Єдиний екземпляр сторінки редагування профілю для використання у всьому додатку.
        private static ProfileEditingPage instance;
        // Посилання на вікно користувацької сторінки.
        private UserPageWindow userPageWindow = UserPageWindow.Instance();

        // Конструктор сторінки редагування профілю.
        private ProfileEditingPage()
        {
            // Ініціалізація компонентів сторінки.
            InitializeComponent();
        }

        /// <summary>
        /// Отримує або створює екземпляр сторінки редагування профілю.
        /// </summary>
        /// <returns>Екземпляр ProfileEditingPage.</returns>
        public static ProfileEditingPage Instance()
        {
            if (instance == null)
            {
                instance = new ProfileEditingPage();
            }
            return instance;
        }

        /// <summary>
        /// Обробник натискання кнопки редагування.
        /// </summary>
        private void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            // Створення нового зображення та його ініціалізація.
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri("pack://application:,,,/Pics/photo_2023-01-18_20-26-30.jpg");
            image.EndInit();
            // Встановлення зображення для фото користувача.
            userPhoto.Source = image;

            // Оновлення розмірів фото користувача.
            userPhoto.UpdateLayout();

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
