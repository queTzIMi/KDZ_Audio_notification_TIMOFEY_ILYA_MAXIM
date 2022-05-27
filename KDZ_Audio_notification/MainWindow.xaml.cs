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

namespace KDZ_Audio_notification
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_prev_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Play_in_random_order_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Play_in_random_order_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void Button_play_Click(object sender, RoutedEventArgs e)
        {
            Button_play.Visibility = Visibility.Collapsed;
            Button_pause.Visibility = Visibility.Visible;
        }

        private void Button_pause_Click(object sender, RoutedEventArgs e)
        {
            Button_pause.Visibility = Visibility.Collapsed;
            Button_play.Visibility = Visibility.Visible;
        }

        private void Button_next_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Volume_controller_music_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void Music_on_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Music_on.Visibility = Visibility.Collapsed;
            Music_off.Visibility = Visibility.Visible;
        }

        private void Music_off_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Music_off.Visibility = Visibility.Collapsed;
            Music_on.Visibility = Visibility.Visible;
        }

        private void Notifications_on_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Notifications_on.Visibility = Visibility.Collapsed;
            Notifications_off.Visibility = Visibility.Visible;
        }

        private void Notifications_off_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Notifications_off.Visibility = Visibility.Collapsed;
            Notifications_on.Visibility = Visibility.Visible;
        }

        private void Volume_controller_notifications_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void Music_list_dropped_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Music_list_dropped.Visibility = Visibility.Collapsed;
            Music_list_up.Visibility = Visibility.Visible;
        }

        private void Music_list_up_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Music_list_up.Visibility = Visibility.Collapsed;
            Music_list_dropped.Visibility = Visibility.Visible;
        }

        private void Musical_List__SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Notifications_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Lunch_finishing_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Dinner_Starting_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Dinner_finishing_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Way_to_folder_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }
    }
}



