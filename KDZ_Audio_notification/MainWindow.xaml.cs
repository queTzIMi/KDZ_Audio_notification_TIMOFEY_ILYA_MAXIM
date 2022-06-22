using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Threading;

namespace KDZ_Audio_notification
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        bool Is_Music = false;
        int lunch_hour_start;
        int lunch_hour_end;
        int lunch_minute_start;
        int lunch_minute_end;
        int dinner_minute_start;
        int dinner_minute_end;
        int dinner_hour_start;
        int dinner_hour_end;

        DispatcherTimer timer = new DispatcherTimer();
        DispatcherTimer food_timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //загрузка настроек из базы данных 
            using (SqlConnection connection = new SqlConnection("Server=localhost;Database=PlayerMP3;Trusted_Connection=True;"))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("select settingvalue from Proga where settingname = 'habitual notice'", connection);
                var a = sqlCommand.ExecuteScalar();
                Play_notifications.IsChecked = sqlCommand.ExecuteScalar()?.ToString() == "exist";
                sqlCommand = new SqlCommand("select settingvalue from Proga where settingname = 'random music'", connection);
                Play_in_random_order.IsChecked = sqlCommand.ExecuteScalar()?.ToString() == "exist";
                sqlCommand = new SqlCommand("select settingvalue from Proga where settingname = 'exist music value'", connection);
                Music_on.Visibility = sqlCommand.ExecuteScalar()?.ToString() == "exist" ? Visibility.Visible : Visibility.Collapsed; // если равно ставим визибл, не равно - collapsed
                Music_off.Visibility = sqlCommand.ExecuteScalar()?.ToString() == "exist" ? Visibility.Collapsed : Visibility.Visible; // здесь должны быть картинка другая (т.е мы сделали зачеркнутую и не зачеркнутую
                sqlCommand = new SqlCommand("select settingvalue from Proga where settingname = 'include notification'", connection);
                Notifications_on.Visibility = sqlCommand.ExecuteScalar()?.ToString() == "exist" ? Visibility.Visible : Visibility.Collapsed;
                Notifications_off.Visibility = sqlCommand.ExecuteScalar()?.ToString() == "exist" ? Visibility.Collapsed : Visibility.Visible;
                sqlCommand = new SqlCommand("select settingvalue from Proga where settingname = 'left slider'", connection);
                var b = sqlCommand.ExecuteScalar();
                Volume_controller_music.Value = int.Parse(sqlCommand.ExecuteScalar()?.ToString());
                sqlCommand = new SqlCommand("select settingvalue from Proga where settingname = 'right slider'", connection);
                Volume_controller_notifications.Value = int.Parse(sqlCommand.ExecuteScalar()?.ToString());
                sqlCommand = new SqlCommand("select settingvalue from Proga where settingname = 'lunch start time'", connection);
                Lunch_starting.Text = sqlCommand.ExecuteScalar()?.ToString();
                sqlCommand = new SqlCommand("select settingvalue from Proga where settingname = 'lunch end time'", connection);
                Lunch_finishing.Text = sqlCommand.ExecuteScalar()?.ToString();
                sqlCommand = new SqlCommand("select settingvalue from Proga where settingname = 'dinner start time'", connection);
                Dinner_Starting.Text = sqlCommand.ExecuteScalar()?.ToString();
                sqlCommand = new SqlCommand("select settingvalue from Proga where settingname = 'dinner end time'", connection);
                Dinner_finishing.Text = sqlCommand.ExecuteScalar()?.ToString();
                sqlCommand = new SqlCommand("select settingvalue from Proga where settingname = 'music folder name'", connection);
                Way_to_music_folder.Text = sqlCommand.ExecuteScalar()?.ToString();
                sqlCommand = new SqlCommand("select settingvalue from Proga where settingname = 'notification folder name'", connection);
                Way_to_notifications_folder.Text = sqlCommand.ExecuteScalar()?.ToString();
            }
            food_timer.Interval = TimeSpan.FromMinutes(1);
            food_timer.Tick += Food_timer_Tick;
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Timer_Tick;
            timer.Start();
            food_timer.Start();

        }

        private void Food_timer_Tick(object sender, EventArgs e)
        {
            if (lunch_hour_start == DateTime.Now.Hour && lunch_minute_start == DateTime.Now.Minute)
            {
                Button_play_Click(null, null);
            }
            if (lunch_hour_end == DateTime.Now.Hour && lunch_minute_end == DateTime.Now.Minute)
            {
                Button_pause.Visibility = Visibility.Collapsed;
                Button_play.Visibility = Visibility.Visible;
                Music_controller.Stop();
            }
            if (dinner_hour_start == DateTime.Now.Hour && dinner_minute_start == DateTime.Now.Minute)
            {
                Button_play_Click(null, null);
            }
            if (dinner_hour_end == DateTime.Now.Hour && dinner_minute_end == DateTime.Now.Minute)
            {
                Button_pause.Visibility = Visibility.Collapsed;
                Button_play.Visibility = Visibility.Visible;
                Music_controller.Stop();
            }




        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Music_controller.NaturalDuration.HasTimeSpan)
            {
                Progress_bar.Value = Music_controller.Position.TotalMilliseconds;
                Progress_bar.Maximum = Music_controller.NaturalDuration.TimeSpan.TotalMilliseconds;
                String current_time = (int)Music_controller.Position.TotalMinutes + ":" + ((int)Music_controller.Position.TotalSeconds % 60).ToString("00");
                String overall_time = (int)Music_controller.NaturalDuration.TimeSpan.TotalMinutes + ":" + ((int)Music_controller.NaturalDuration.TimeSpan.TotalSeconds % 60).ToString("00");
                Music_timer.Content = current_time + "/" + overall_time;
            }
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
                Is_Music = false;
                Notifications_List.SelectedIndex = (Notifications_List.SelectedIndex + 1) % Notifications_List.Items.Count;

            }
            else
            {
                Is_Music = true;

                if (Musical_List_.Items.Count == 0)
                {
                    return;
                }
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
            if (Volume_controller_music.Value > 0)
            {
                Music_off.Visibility = Visibility.Collapsed;
                Music_on.Visibility = Visibility.Visible;
            }
            else
            {
                Music_on.Visibility = Visibility.Collapsed;
                Music_off.Visibility = Visibility.Visible;
            }
            if (Is_Music == true) Music_controller.Volume = Volume_controller_music.Value / (double)100;
            Volume_percent_label_music.Content = (int)Volume_controller_music.Value; // (int) отрезает дробную часть у лейбла с показателем уровня громкости в числах

        }

        private void Music_on_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Music_on.Visibility = Visibility.Collapsed;
            Music_off.Visibility = Visibility.Visible;
            if (Is_Music) Music_controller.Volume = 0;
        }

        private void Music_off_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Music_off.Visibility = Visibility.Collapsed;
            Music_on.Visibility = Visibility.Visible;
            if (Is_Music) Music_controller.Volume = Volume_controller_music.Value / (double)100;
        }

        private void Notifications_on_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Notifications_on.Visibility = Visibility.Collapsed;
            Notifications_off.Visibility = Visibility.Visible;
            if (Is_Music == false) Music_controller.Volume = 0;
        }

        private void Notifications_off_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Notifications_off.Visibility = Visibility.Collapsed;
            Notifications_on.Visibility = Visibility.Visible;
            if (Is_Music == false) Music_controller.Volume = Volume_controller_notifications.Value / (double)100;
        }
        private void Volume_controller_notifications_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (Volume_controller_notifications.Value > 0)
            {
                Notifications_off.Visibility = Visibility.Collapsed;
                Notifications_on.Visibility = Visibility.Visible;
            }
            else
            {
                Notifications_on.Visibility = Visibility.Collapsed;
                Notifications_off.Visibility = Visibility.Visible;
            }
            if (Is_Music == false) Music_controller.Volume = Volume_controller_notifications.Value / (double)100;
            Volume_percent_label_notifications.Content = (int)Volume_controller_notifications.Value;
        }

        private void Musical_List__SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Music_controller.Source = new Uri(Way_to_music_folder.Text + Musical_List_.SelectedItem);
            if (Music_on.Visibility == Visibility.Visible)
            {
                Music_controller.Volume = Volume_controller_music.Value / 100;
            }
            else
            {
                Music_controller.Volume = 0;
            }

            Button_play_Click(null, null);

        }

        private void Notifications_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Music_controller.Source = new Uri(Way_to_notifications_folder.Text + Notifications_List.SelectedItem);
            if (Notifications_on.Visibility == Visibility.Visible)
            {
                Music_controller.Volume = Volume_controller_notifications.Value / 100;
            }
            else
            {
                Music_controller.Volume = 0;
            }
            Button_play_Click(null, null);
        }
        private void String_To_Date(TextBox text_box, out int hour, out int minute)
        {
            try
            {
                String[] mas = text_box.Text.Split(':');
                hour = int.Parse(mas[0]);
                minute = int.Parse(mas[1]);
                text_box.Background = Brushes.White;
            }
            catch
            {
                text_box.Background = Brushes.LightCoral;
                hour = 0;
                minute = 0;
            }



        }
        private void Lunch_starting_TextChanged(object sender, TextChangedEventArgs e)
        {
            String_To_Date(Lunch_starting, out lunch_hour_start, out lunch_minute_start);
        }

        private void Lunch_finishing_TextChanged(object sender, TextChangedEventArgs e)
        {
            String_To_Date(Lunch_finishing, out lunch_hour_end, out lunch_minute_end);
        }

        private void Dinner_Starting_TextChanged(object sender, TextChangedEventArgs e)
        {
            String_To_Date(Dinner_Starting, out dinner_hour_start, out dinner_minute_start);
        }

        private void Dinner_finishing_TextChanged(object sender, TextChangedEventArgs e)
        {
            String_To_Date(Dinner_finishing, out dinner_hour_end, out dinner_minute_end);
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //сохранение настроек обратно в базу данных. Window closing это событие при закрытии окна
            using (SqlConnection connection = new SqlConnection("Server=localhost;Database=PlayerMP3;Trusted_Connection=True;"))
            {
                connection.Open();
                string exist = Play_notifications.IsChecked == true ? "exist" : "not exist";
                SqlCommand sqlCommand = new SqlCommand($"update Proga set settingvalue = '{exist}' where settingname = 'habitual notice'", connection);
                sqlCommand.ExecuteNonQuery();
                exist = Play_in_random_order.IsChecked == true ? "exist" : "not exist";
                sqlCommand = new SqlCommand($"update Proga set settingvalue = '{exist}' where settingname = 'random music'", connection);
                sqlCommand.ExecuteNonQuery();
                exist = Music_on.Visibility == Visibility.Visible ? "exist" : "not exist";

                sqlCommand = new SqlCommand($"update Proga set settingvalue = '{exist}' where settingname = 'exist music value'", connection);
                sqlCommand.ExecuteNonQuery();

                exist = Notifications_on.Visibility == Visibility.Visible ? "exist" : "not exist";

                sqlCommand = new SqlCommand($"update Proga set settingvalue = '{exist}' where settingname = 'include notification'", connection);
                sqlCommand.ExecuteNonQuery();

                sqlCommand = new SqlCommand($"update Proga set settingvalue = '{(int)Volume_controller_music.Value}' where settingname  = 'left slider'", connection);
                sqlCommand.ExecuteNonQuery();

                sqlCommand = new SqlCommand($"update Proga set settingvalue = '{(int)Volume_controller_notifications.Value}' where settingname  = 'right slider'", connection);
                sqlCommand.ExecuteNonQuery();

                sqlCommand = new SqlCommand($"update Proga set settingvalue = '{Lunch_starting.Text}' where settingname  = 'lunch start time'", connection);
                sqlCommand.ExecuteNonQuery();

                sqlCommand = new SqlCommand($"update Proga set settingvalue = '{Lunch_finishing.Text}' where settingname  = 'lunch end time'", connection);
                sqlCommand.ExecuteNonQuery();

                sqlCommand = new SqlCommand($"update Proga set settingvalue = '{Dinner_Starting.Text}' where settingname  = 'dinner start time'", connection);
                sqlCommand.ExecuteNonQuery();

                sqlCommand = new SqlCommand($"update Proga set settingvalue = '{Dinner_finishing.Text}' where settingname  = 'dinner end time'", connection);
                sqlCommand.ExecuteNonQuery();

                sqlCommand = new SqlCommand($"update Proga set settingvalue = '{Way_to_music_folder.Text}' where settingname  = 'music folder name'", connection);
                sqlCommand.ExecuteNonQuery();

                sqlCommand = new SqlCommand($"update Proga set settingvalue = '{Way_to_notifications_folder.Text}' where settingname  = 'notification folder name'", connection);
                sqlCommand.ExecuteNonQuery();

            }
        }
    }
}

