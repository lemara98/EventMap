using EventMap.Classes;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EventMap.Dialogs
{
    /// <summary>
    /// Interaction logic for PrikaziTutorijalDialog.xaml
    /// </summary>
    public partial class PrikaziTutorijalDialog : Window
    {
        public PrikaziTutorijalDialog()
        {
            InitializeComponent();
            this.tutorijal.Source = new Uri(@"C:\Users\Mile\source\repos\EventMap2\EventMap\Resources\tutorijal.wmv");
            Console.Out.WriteLine(this.tutorijal.IsVisible);
            this.tutorijal.Visibility = Visibility.Visible;
            this.tutorijal.Position = TimeSpan.FromMilliseconds(0.1);
        }

        private void povratakNaPregledPinaButton_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (Storyboard)this.TryFindResource("fadeOutStoryboard");
            sb.Begin();
        }

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void tutorijal_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.tutorijal.Position != TimeSpan.Zero)
                this.tutorijal.Pause();
            else
                this.tutorijal.Play();
        }

        private void pustiButton_Click(object sender, RoutedEventArgs e)
        {
            this.tutorijal.Play();
        }

        private void pauzirajButton_Click(object sender, RoutedEventArgs e)
        {
            this.tutorijal.Pause();
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            this.tutorijal.Stop();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.IsActive && this.IsEnabled)
            {
                string str = HelpProvider.GetHelpKey(this);
                HelpProvider.ShowHelp(str, this);
            }

        }
    }
}
