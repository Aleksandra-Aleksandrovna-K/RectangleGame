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
using System.Windows.Threading;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int Nstart; // количство элементов на старте
        int NRectangle; // количство прямоуголников на экране
        Random random;
        DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            Nstart = 5;
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 750);
            timer.Tick += new EventHandler(Timer_Tick);


        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (NRectangle == 0)
            {
                MessageBox.Show("You win!");
                Application.Current.Shutdown();
            }


            if (NRectangle > 50)
            {
                MessageBox.Show("You lose!");
                Application.Current.Shutdown();
            }
                
            AddRectangle();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            random = new Random();


            for (int i = 0; i < Nstart; i++)
            {
                AddRectangle();

            }

            timer.Start();
            button1.Visibility = Visibility.Hidden;
        }

        private void AddRectangle()
        {

            Rectangle r = new Rectangle();
            var rnd = new Random();
            var arr1 = new int[] { 40, 200 };
            var rndMember = arr1[rnd.Next(arr1.Length)];
            r.Width = rndMember;

            if (r.Width == 200)
            {
                r.Height = 40;
                int x = random.Next((int)this.Width - 20 - 200);
                int y = random.Next((int)this.Height - 50 - 40);
                r.Margin = new Thickness(x, y, 0, 0);
            }

            else
            {
                r.Height = 200;
                int x = random.Next((int)this.Width - 20 - 40);
                int y = random.Next((int)this.Height - 50 - 200);
                r.Margin = new Thickness(x, y, 0, 0);
            }
                      
            //r.Fill = new SolidColorBrush(Colors.Aqua);

            r.Fill = new SolidColorBrush(Color.FromArgb(255,
                (byte)random.Next(256),
                (byte)random.Next(256),
                (byte)random.Next(256)));

            //r.Stroke = new SolidColorBrush(Colors.Black);
            r.HorizontalAlignment = HorizontalAlignment.Left;
            r.VerticalAlignment = VerticalAlignment.Top;


            r.MouseDown += new MouseButtonEventHandler(Rect_Click);
                                 
            grid1.Children.Add(r);
            NRectangle += 1;
        }

        private void Rect_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Ok!");

            //Цикл проверки на перекрытие
            Rectangle r1 = (Rectangle)sender;
            Rect rect1 = new Rect(r1.Margin.Left, r1.Margin.Top, r1.Width, r1.Height);
            int index = grid1.Children.IndexOf(r1);

            for (int i=index+1;i<grid1.Children.Count;i++)
            {
                Rectangle r2 = (Rectangle)grid1.Children[i];
                Rect rect2 = new Rect(r2.Margin.Left, r2.Margin.Top, r2.Width, r2.Height);
                if (rect1.IntersectsWith(rect2))
                {
                    return; //Не убирается
                }

            }
            grid1.Children.Remove(r1);
            NRectangle -= 1;
        }
    }
}
