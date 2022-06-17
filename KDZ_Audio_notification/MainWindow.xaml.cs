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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ShadowProjectKDZ
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        bool Is_Music = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Way_to_music_folder.Text = (@"C:\Users\RIP\Source\Repos\KDZ_Audio_notification_TIMOFEY_ILYA_MAXIM\KDZ_Audio_notification\mp3\");
            Way_to_notifications_folder.Text = (@"C:\Users\RIP\Source\Repos\KDZ_Audio_notification_TIMOFEY_ILYA_MAXIM\KDZ_Audio_notification\notifications\");
        }

        private void Button_prev_Click(object sender, RoutedEventArgs e)
        {
            if (Musical_List_.SelectedIndex > 0)
            {
                Musical_List_.SelectedIndex = Musical_List_.SelectedIndex - 1;
            }

        }

        private void Play_in_random_order_Checked(object sender, RoutedEventArgs e)
        {
            Button_prev.Visibility = Visibility.Hidden;
        }

        private void Play_in_random_order_Unchecked(object sender, RoutedEventArgs e)
        {
            Button_prev.Visibility = Visibility.Visible;
        }

        private void Button_play_Click(object sender, RoutedEventArgs e)
        {

            if ((Musical_List_.SelectedIndex == -1) && (Musical_List_.Items.Count > 0))
            {
                Button_next_Click(null, null);
            }
            else
            {
                Button_play.Visibility = Visibility.Collapsed;
                Button_pause.Visibility = Visibility.Visible;
                Music_controller.Play();
            }


        }

        private void Button_pause_Click(object sender, RoutedEventArgs e)
        {
            Button_pause.Visibility = Visibility.Collapsed;
            Button_play.Visibility = Visibility.Visible;
            Music_controller.Pause();
        }

        private void Button_next_Click(object sender, RoutedEventArgs e)
        {
            if ((Play_notifications.IsChecked == true) && (Is_Music == true) && (Notifications_List.Items.Count > 0))
            {
                Notifications_List.SelectedIndex = (Notifications_List.SelectedIndex + 1) % Notifications_List.Items.Count;
                Is_Music = false;

            }
            else
            {
                Is_Music = true;


                if (Play_in_random_order.IsChecked == true)
                {
                    Random random = new Random();
                    Musical_List_.SelectedIndex = random.Next(Musical_List_.Items.Count);

                }
                else
                {

                    Musical_List_.SelectedIndex = (Musical_List_.SelectedIndex + 1) % Musical_List_.Items.Count;

                }
            }


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
            Music_controller.Source = new Uri(Way_to_music_folder.Text + Musical_List_.SelectedItem);
            Button_play_Click(null, null);

        }

        private void Notifications_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Music_controller.Source = new Uri(Way_to_notifications_folder.Text + Notifications_List.SelectedItem);
            Button_play_Click(null, null);
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
            Folder_color_check();
            string way_to_catalogue = Way_to_music_folder.Text;
            if (Directory.Exists(way_to_catalogue))
            {
                if (Way_to_music_folder.Text.EndsWith(@"\") == false)
                {
                    Way_to_music_folder.Text += @"\";
                }
                DirectoryInfo directoryInfo = new DirectoryInfo(way_to_catalogue);
                FileInfo[] Files = directoryInfo.GetFiles();
                Musical_List_.Items.Clear();
                foreach (FileInfo file in Files)
                {
                    if (file.Name.EndsWith(".mp3"))
                    {

                        Musical_List_.Items.Add(file.Name);
                    }
                }
            }
        }

        private void Button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            string way_to_notifications = Way_to_notifications_folder.Text;
            Folder_color_check();
            if (Directory.Exists(way_to_notifications))
            {
                if (Way_to_notifications_folder.Text.EndsWith(@"\") == false)
                {
                    Way_to_notifications_folder.Text += @"\";
                }

                DirectoryInfo directoryInfo = new DirectoryInfo(way_to_notifications);
                FileInfo[] Files = directoryInfo.GetFiles();
                Notifications_List.Items.Clear();
                foreach (FileInfo file in Files)
                {
                    if (file.Name.EndsWith(".mp3"))
                    {

                        Notifications_List.Items.Add(file.Name);
                    }
                }
            }
        }
        private void Folder_color_check()
        {
            if (Directory.Exists(Way_to_notifications_folder.Text))
            {
                Way_to_notifications_folder.Background = Brushes.LightGreen;
            }
            else
            {
                Way_to_notifications_folder.Background = Brushes.LightCoral;
            }
            if (Directory.Exists(Way_to_music_folder.Text))
            {
                Way_to_music_folder.Background = Brushes.LightGreen;
            }
            else
            {
                Way_to_music_folder.Background = Brushes.LightCoral;
            }
            if (Way_to_music_folder.Text.ToUpper() == Way_to_notifications_folder.Text.ToUpper())
            {
                Way_to_notifications_folder.Background = Brushes.Yellow;
                Way_to_music_folder.Background = Brushes.Yellow;
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.SelectedPath = Way_to_music_folder.Text;
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    Way_to_music_folder.Text = dialog.SelectedPath;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                dialog.SelectedPath = Way_to_notifications_folder.Text;
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    Way_to_notifications_folder.Text = dialog.SelectedPath;
            }
        }

        private void Music_controller_MediaEnded(object sender, RoutedEventArgs e)
        {
            Button_next_Click(null, null);
        }
    }
}

