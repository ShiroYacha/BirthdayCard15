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
    /// Interaction logic for HappyBirthdayPreRight.xaml
    /// </summary>
    public partial class HappyBirthdayPreRight : UserControl
    {
        private static HappyBirthdayPreRight _staticHandle;

        public HappyBirthdayPreRight()
        {
            InitializeComponent();
            _staticHandle = this;
        }

        public static void Trigger()
        {
            _staticHandle.Box.Visibility = Visibility.Collapsed;
            _staticHandle.Card.Visibility = Visibility.Visible;
        }
    }
}
