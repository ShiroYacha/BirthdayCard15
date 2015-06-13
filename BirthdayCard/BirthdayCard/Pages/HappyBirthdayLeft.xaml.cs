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
    /// Interaction logic for HappyBirthdayLeft.xaml
    /// </summary>
    public partial class HappyBirthdayLeft : UserControl
    {
        private static HappyBirthdayLeft _staticeHandle;

        public HappyBirthdayLeft()
        {
            InitializeComponent();
            _staticeHandle = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Open.Visibility = Visibility.Visible;
            MainWindow._staticHandle.DisableControlButtons();
        }

        private void Open_MouseEnter(object sender, MouseEventArgs e)
        {
            Open.Opacity = 0.5;
        }

        private void Open_MouseLeave(object sender, MouseEventArgs e)
        {
            Open.Opacity = 0.0;
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.TriggerNextPageOpenStyle();
        }
    }
}
