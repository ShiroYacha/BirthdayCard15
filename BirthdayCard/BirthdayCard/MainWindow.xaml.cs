﻿using PixelLab.Wpf.Transitions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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

namespace BirthdayCard
{
    

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow _staticHandle;
        private static bool _isPlaying = false;

        public MainWindow()
        {
            InitializeComponent();
            _staticHandle = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PropertyInfo[] props = typeof(Brushes).GetProperties();

            List<object> dataLeft = new List<object>(5);
            List<object> dataRight = new List<object>(5);

            dataLeft.Add(new Pages.CoverLeft());
            dataRight.Add(new Pages.CoverRight());

            dataLeft.Add(new Pages.MoviePageLeft());
            dataRight.Add(new Pages.MoviePageRight());

            dataLeft.Add(new Pages.HappyBirthdayLeft());
            dataRight.Add(new Pages.HappyBirthdayRight());

            dataLeft.Add(new Pages.BackCoverLeft());
            dataRight.Add(new Pages.BackCoverRight());

            _dataLeft.Items.Clear();
            _dataLeft.ItemsSource = dataLeft;
            _dataRight.Items.Clear();
            _dataRight.ItemsSource = dataRight;

            _dataRight.SelectedIndex=0;
            _dataLeft.SelectedIndex=0;

            ResetControlButtonsAvailability();

        }


        private void BackPageButton_MouseEnter(object sender, MouseEventArgs e)
        {
            BackPageButton.Opacity = 0.5;
        }

        private void NextPageButton_MouseEnter(object sender, MouseEventArgs e)
        {
            NextPageButton.Opacity = 0.5;
        }

        private void BackPageButton_MouseLeave(object sender, MouseEventArgs e)
        {
            BackPageButton.Opacity = 0;

        }

        private void NextPageButton_MouseLeave(object sender, MouseEventArgs e)
        {
            NextPageButton.Opacity = 0;

        }

        private void BackPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (_dataLeft.SelectedIndex > 0 && _dataRight.SelectedIndex > 0)
            {
                LeftPage.Transition = FindResource("LeftFlip") as Transition;
                RightPage.Transition = FindResource("BasicTransition") as Transition;
                _dataLeft.SelectedIndex--;
                _dataRight.SelectedIndex--;
                ResetControlButtonsAvailability();
            }
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            RightPage.Transition = FindResource("RightFlip") as Transition;
            LeftPage.Transition = FindResource("BasicTransition") as Transition;
            _dataRight.SelectedIndex++;
            _dataLeft.SelectedIndex++;
            ResetControlButtonsAvailability();
        }

        public static void PlayVideo(string assetName)
        {
            if (!_isPlaying)
            {
                _staticHandle.Player.Source = new Uri(assetName, UriKind.Relative);
                System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                dispatcherTimer.Tag = assetName;
                dispatcherTimer.Tick += dispatcherTimer_Tick;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
                dispatcherTimer.Start();
                _isPlaying = true;
            }
        }

        private static void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            var timer = sender as System.Windows.Threading.DispatcherTimer;
            var source = new Uri(timer.Tag as string, UriKind.Relative);
            timer.Tick -= dispatcherTimer_Tick;
            timer.Stop();
            _staticHandle.Player.Play();
            _staticHandle.Player.Visibility = Visibility.Visible;
            _staticHandle.DisableControlButtons();
        }

        private void ResetControlButtonsAvailability()
        {
            BackPageButton.IsEnabled = _dataLeft.SelectedIndex > 0;
            NextPageButton.IsEnabled = _dataRight.SelectedIndex < _dataRight.Items.Count - 1;
            BackPageButton.Visibility = BackPageButton.IsEnabled ? Visibility.Visible : Visibility.Collapsed;
            NextPageButton.Visibility = NextPageButton.IsEnabled ? Visibility.Visible : Visibility.Collapsed;
        }

        private void DisableControlButtons()
        {
            BackPageButton.IsEnabled = false;
            NextPageButton.IsEnabled = false;
            BackPageButton.Visibility = Visibility.Collapsed;
            NextPageButton.Visibility = Visibility.Collapsed;
        }

        private void Player_MediaEnded(object sender, RoutedEventArgs e)
        {
            Player.Visibility = Visibility.Collapsed;
            ResetControlButtonsAvailability();
            _isPlaying = false;
        }
    }

    class ListTransitionSelector : TransitionSelector
    {
        public ListTransitionSelector(Transition forward, Transition backward, IList list)
        {
            _forward = forward;
            _backward = backward;
            _list = list;
        }

        public override Transition SelectTransition(object oldContent, object newContent)
        {
            int oldIndex = _list.IndexOf(oldContent);
            int newIndex = _list.IndexOf(newContent);
            return newIndex > oldIndex ? _forward : _backward;
        }

        private Transition _forward, _backward;
        private IList _list;
    }

    internal class UI
    {
    }

    internal class Picture
    {
        public Picture(string uri)
        {
            _uri = uri;
        }

        public string Uri
        {
            get { return _uri; }
        }

        private readonly string _uri;
    }

    internal class Swatch
    {
        public Swatch(string colorName)
        {
            _colorName = colorName;
        }

        public string ColorName
        {
            get { return _colorName; }
        }

        private readonly string _colorName;
    }
}
