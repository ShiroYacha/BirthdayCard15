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

namespace BirthdayCard.Pages
{
    /// <summary>
    /// Interaction logic for HappyBirthdayPreLeft.xaml
    /// </summary>
    public partial class HappyBirthdayPreLeft : UserControl
    {
        public HappyBirthdayPreLeft()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.MediaEnded += MainWindow_MediaEnded;
            MainWindow.PlayVideo("Assets\\Scene_HappyBirthday.mp4");
        }

        private void MainWindow_MediaEnded()
        {
            MainWindow.MediaEnded -= MainWindow_MediaEnded;
            Box.Visibility = Visibility.Collapsed;
            Card.Visibility = Visibility.Visible;
            HappyBirthdayPreRight.Trigger();
        }
    }
}
