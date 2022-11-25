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
using System.IO;

namespace practice
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>


    public partial class MainWindow : Window
    {

        public List<CheckBox> tasks = new List<CheckBox>();
        public string[] task_arr = File.ReadAllLines("tasks.txt");
        

        public MainWindow()
        {
            InitializeComponent();
            Fileload();
            
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ekran.Focus();
            DragMove();
        }

        private void quit_Button_Click(object sender, RoutedEventArgs e)
        {
            SaveTasks();
            Environment.Exit(0);
        }

        private void minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            addTask();
            tbx_add.Text = "Your Task...";
            buttonAnimation();
        }

        private void tbx_add_GotFocus(object sender, RoutedEventArgs e)
        {
            altCizgi.Background = new SolidColorBrush(Color.FromArgb(255, 66, 250, 171));
            tbx_add.Foreground = new SolidColorBrush(Color.FromArgb(255, 225, 243, 235));
            if (tbx_add.Text == "Your Task...")
                tbx_add.Text = "";
        }

        private void tbx_add_LostFocus(object sender, RoutedEventArgs e)
        {
            altCizgi.Background = new SolidColorBrush(Color.FromArgb(255, 160, 160, 160));
            tbx_add.Foreground = new SolidColorBrush(Color.FromArgb(255, 77, 96, 112));
            if (tbx_add.Text == "")
                tbx_add.Text = "Your Task...";
        }

        private void tbx_add_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                addTask();
                tbx_add.Text = "";
            }

        }

        private void addTask()
        {
            if (tbx_add.Text != "Your Task..." && tbx_add.Text != "")
            {
                tasks.Add(new CheckBox() { MaxWidth = 400, LayoutTransform = new ScaleTransform(1.2, 1.2)});
                tasks.ElementAt(tasks.Count - 1).Content = new TextBox() { Text = tbx_add.Text, MaxWidth = 300, FontSize = 15 };
                lw_liste.Items.Refresh();
                tbx_add.Text = "";
            }
        }

        public void Fileload()
        {
            int counter = 0;

            for (int i = 0; i < task_arr.Length; i++)
                tasks.Add(new CheckBox() { MaxWidth = 400, LayoutTransform = new ScaleTransform(1.2,1.2)}) ;
            foreach (var task in tasks)
            {
                task.Content = new TextBox() { Text = task_arr[counter], MaxWidth = 300, FontSize = 15 };
                counter++;
            }
            lw_liste.ItemsSource = tasks;
        }

        public void SaveTasks()
        {
            int counter = 0;
            string[] savedTasks = new string[tasks.Count];
            foreach (var task in tasks)
            {
                savedTasks[counter] = task.Content.ToString();
                counter++;
            }

            for (int i = 0; i < savedTasks.Length; i++)
            {
                savedTasks[i] = savedTasks[i].Replace("System.Windows.Controls.TextBox: ", "");
            }

            File.WriteAllLines("tasks.txt", savedTasks);
        }

        private async void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            
            for (int i = 0; i < tasks.Count; i++)
            {
                if ((bool)tasks.ElementAt(i).IsChecked)
                {
                    await Task.Delay(250);
                    tasks.RemoveAt(i); 
                    lw_liste.Items.Refresh();
                }
            }
        }

        private async void buttonAnimation()
        {
            btn_add.Margin = new Thickness(305, 529, 0, 0);
            await Task.Delay(100);
            btn_add.Margin = new Thickness(305, 525, 0, 0);
        }

    }
}
