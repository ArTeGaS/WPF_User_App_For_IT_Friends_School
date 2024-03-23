using Microsoft.Win32;
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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Виберіть фотографію";
            openFileDialog.Filter = "Image files (*.jpg;*.png)|*.jpg;*.png|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                // Визначення імені файлу з повного шляху
                string fileName = System.IO.Path.GetFileName(filePath);

                // Використання AppDomain.CurrentDomain.BaseDirectory для визначення шляху до кореневої папки проекту
                string basePath = AppDomain.CurrentDomain.BaseDirectory;

                // Формування цільового шляху в папці Pics
                string targetPath = System.IO.Path.Combine(basePath, "Pics", fileName);

                // Перевірка на існування папки 'Pics', і створення її якщо необхідно
                if (!System.IO.Directory.Exists(System.IO.Path.Combine(basePath, "Pics")))
                {
                    System.IO.Directory.CreateDirectory(System.IO.Path.Combine(basePath, "Pics"));
                }

                // Копіювання файлу до папки Pics, замінюючи файл, якщо він вже існує
                System.IO.File.Copy(filePath, targetPath, true);

                List<User> usersData = JSON_FileManager.LoadUsers("Users", "C://OOTDDir");
                foreach (User user in usersData)
                {
                    // Якщо знайдено користувача з вказаним логіном.
                    if (userPageWindow.currentUser.Login == user.Login)
                    {
                        // Зберігаємо шлях до фотографії юзера у його об'єкт.
                        user.PhotoPath = targetPath;
                        userPageWindow.currentUser = user;
                        ProfilePage.Instance().currentUser = user;
                        break;
                    }
                }
                JSON_FileManager.SaveUsers("Users", "C://OOTDDir", usersData);

                // Створення нового зображення та його ініціалізація.
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(openFileDialog.FileName);
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

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            userPageWindow.UserPageFrame.Content = userPageWindow.UserPageFrame;
        }
    }
}
