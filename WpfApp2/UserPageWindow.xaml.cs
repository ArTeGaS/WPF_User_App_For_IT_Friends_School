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
        // Поточний користувач, який відображається на сторінці.
        public User currentUser;

        // Конструктор вікна сторінки користувача.
        private UserPageWindow()
        {
            // Ініціалізація компонентів вікна.
            InitializeComponent();


            UserPageFrame.Content = ProfilePage.Instance();

            // Отримання поточного користувача
            currentUser = ProfilePage.Instance().currentUser;

            // Додавання елементів у список кнопок для навігації або дій.
            listOfButtons.Items.Add("Головна");
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
        /// Обробник зміни вибору у списку кнопок.
        /// </summary>
        private void listOfButtons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Виконання дій відповідно до вибору користувача у списку кнопок.
            ListView listView = sender as ListView;
            int selectedIndex = listView.SelectedIndex;
            switch (selectedIndex)
            {
                case 0:
                    UserPageFrame.Content = ProfilePage.Instance();
                    ProfilePage.Instance().userPhoto_Loaded();
                    break;

                case 1: // Якщо вибрано "Редагувати дані".
                    UserPageFrame.Content = ProfileEditingPage.Instance(); // Навігація до сторінки редагування профілю.
                    break;
            }
        }
    }
}
