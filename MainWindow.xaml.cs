using EventMap.Classes;
using EventMap.Pages;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace EventMap
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }




        private MapPage stranicaMape;
        private TablePage stranicaTabele;
        private HelpPage stranicaPomoci;
        private InformationPage stranicaLogovanja;
        private Dogadjaj _dogadjaj;

        private string _nesto;

        public MapPin Pin
        {
            get;
            set;
        }

        public string Nesto
        {
            get
            {
                return _nesto;
            }
            set
            {
                _nesto = value;
                //OnPropertyChanged("NekiTekst");
            }
        }

        public Dogadjaj Dogadjaj
        {
            get
            {
                return _dogadjaj;
            }
            set
            {
                _dogadjaj = value;
                this.OnPropertyChanged("Dogadjaj");
            }
        }

        // Pinovati lokaciju na mapi u odnosu na grad koji se nalazi u odredjenoj drzavi 
/*        public static List<string> sveDrzave = new List<string>();
        public static Dictionary<string, List<string>> gradoviUDrzavi = new Dictionary<string, List<string>>();*/
        //public static Dictionary<string, LokacijaNaMapi> lokacijeGradovaNaMapi = new Dictionary<string, LokacijaNaMapi>();

        public List<string> SveDrzave
        {
            get
            {
                return Dogadjaj.sveDrzave;
            }
        }

        public Dictionary<string, List<string>> GradoviUDrzavi
        {
            get
            {
                return Dogadjaj.gradoviUDrzavi;
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Updating the Label which displays the current second
            this.Datum.Content = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            // Forcing the CommandManager to raise the RequerySuggested event
            CommandManager.InvalidateRequerySuggested();
        }


        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();


            this.stranicaMape = new MapPage();
            this.stranicaTabele = new TablePage();
            this.stranicaPomoci = new HelpPage();
            this.stranicaLogovanja = new InformationPage();

            this.Dogadjaj = new Dogadjaj();
            this.Nesto = "NekiTekst";
            this.Pin = new MapPin(100, 200, "Novi Sad", "Srbija");
            this.Pin.ListaDogadjaja.Add(new Dogadjaj());
            
        }

        


        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void prikazMape_Click(object sender, RoutedEventArgs e)
        {
            this.MainFrame.Content = stranicaMape;
        }

        private void prikazTabele_Click(object sender, RoutedEventArgs e)
        {
            this.MainFrame.Content = stranicaTabele;
        }

        private void pomoc_Click(object sender, RoutedEventArgs e)
        {
            this.MainFrame.Content = stranicaPomoci;
        }

        private void ulogujteSe_Click(object sender, RoutedEventArgs e)
        {
            this.MainFrame.Content = stranicaLogovanja;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Drzave_Click(object sender, RoutedEventArgs e)
        {
            /*podesavanjeDrzava.Visibility = Visibility.Visible;
            podesavanjeDrzava.ShowDialog(MainFrame);*/
        }

        private void Drzave_Click_1(object sender, RoutedEventArgs e)
        {
            
            dijalog.ShowDialog();
        }
    }
}
