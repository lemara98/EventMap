using EventMap.Classes;
using EventMap.Dialogs;
using EventMap.Pages;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
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

        private Point _startPoint;


        private MapPage stranicaMape;
        private TablePage stranicaTabele;
        private HelpPage stranicaPomoci;
        private InformationPage stranicaLogovanja;

        private Dogadjaj _dogadjaj;
        private MapPin _odabraniPin;

        //private PodesiDrzaveDialog _podesiDrzaveIGradoveDialog;
        private AzurirajPinDialog _azurirajPinDialog;
        private ObrisiPinDialog _obrisiPinDialog;
        private DetaljnijiPregledPinaDialog _detaljnijiPregledPinaDialog;

        private static MapPin _pin;
        private List<MapPin> _sviPinovi;
        //private List<MapPin> _sacuvanoStanjePrePretrage;
        //private List<Dogadjaj> _sviPromenjeni;
        /*private ;  */ //(Style)this.Resources["PinoviNaMapi"];


        public static MapPin Pin
        {
            get
            {
                return _pin;
            }
            set
            {
                if (_pin != value)
                {
                    _pin = value;
                    //OnPropertyChanged("Pin");
                }
            }
        }

        public MapPin SelektovaniPin
        {
            get
            {
                return _pin;
            }
            set
            {
                if (_pin != value)
                {
                    _pin = value;
                    OnPropertyChanged("SelektovaniPin");
                }
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
                OnPropertyChanged("Dogadjaj");
            }
        }

        public List<MapPin> SviPinovi
        {
            get
            {
                return _sviPinovi;
            }
            set
            {
                if (value != _sviPinovi)
                {
                    _sviPinovi = value;
                    OnPropertyChanged("SviPinovi");
                }
            }
        }

        /*       public static Style PinStyle
               {
                   get
                   {
                       return _pinStyle;
                   }
               }*/


        // Pinovati lokaciju na mapi u odnosu na grad koji se nalazi u odredjenoj drzavi 
        /*        public static List<string> sveDrzave = new List<string>();
                public static Dictionary<string, List<string>> gradoviUDrzavi = new Dictionary<string, List<string>>();*/
        //public static Dictionary<string, LokacijaNaMapi> lokacijeGradovaNaMapi = new Dictionary<string, LokacijaNaMapi>();

        public List<Drzava> SveDrzave
        {
            get
            {
                return Dogadjaj.sveDrzave;
            }
        }

        public Dictionary<string, List<Grad>> GradoviUDrzavi
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

            Dogadjaj.sveDrzave = new List<Drzava>();
            Dogadjaj.gradoviUDrzavi = new Dictionary<string, List<Grad>>();


            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();



            this.stranicaMape = new MapPage();
         //   this.stranicaTabele = new TablePage();
            this.stranicaPomoci = new HelpPage();
            this.stranicaLogovanja = new InformationPage();
            //this._podesiDrzaveIGradoveDialog = new PodesiDrzaveDialog();

            SviPinovi = this.LoadJson();

            //List<Etiketa> r = new List<Etiketa>();
            //r.Add(new Etiketa("Oznaka Proba", "Neki random tekst"));

            //this.Dogadjaj = new Dogadjaj();
            //Dogadjaj novi = new Dogadjaj("OznakaProba2", "probaNaziv2", "ProbaOpis2", TIP.PIVSKI, POSECENOST_DOGADJAJA.PREKO_10000,
            //    "C:\\Users\\Mile\\source\\repos\\EventMap2\\EventMap\\Resources\\Icons\\help.png",
            //    true, 100, "Velika Britanija", "London", null, new DateTime(), r);

            //Pin = new MapPin(100, 200, "Novi Sad", "Srbija");
            //Pin.dodajDogadjaj(new Dogadjaj());
            //Pin.dodajDogadjaj(novi);




            //SviPinovi = new List<MapPin>();

            //SelektovaniPin = Pin;

            //SviPinovi.Add(Pin);




        }

        public void WriteJson()
        {
            // serialize JSON to a string and then write string to a file
            //File.WriteAllText(@"c:\movie.json", JsonConvert.SerializeObject(SviPinovi));
            //open file stream
            using (StreamWriter file = File.CreateText(@"..\\..\\Podaci.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                // serializer.Converters.Add(new JavaScriptDateTimeConverter());
                serializer.NullValueHandling = NullValueHandling.Ignore;
                serializer.Formatting = Formatting.Indented;
                serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                serializer.PreserveReferencesHandling = PreserveReferencesHandling.None;

                //serialize object directly into file stream
                SelektovaniPin = null;
                //String tt = JsonConvert.SerializeObject(SviPinovi, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                //                                                                                                        NullValueHandling = NullValueHandling.Ignore});

                serializer.Serialize(file, SviPinovi);

            }
        }

        public List<MapPin> LoadJson()
        {
            List<MapPin> items;
            Console.Out.WriteLine(Dogadjaj.sveDrzave.Count);
            using (StreamReader r = new StreamReader(@"..\\..\\Podaci.json"))
            {
                string json = r.ReadToEnd();
                if (json.Equals(""))
                {
                    items = new List<MapPin>();
                }
                else
                    items = new List<MapPin>(JsonConvert.DeserializeObject<List<MapPin>>(json));
            }
            Console.Out.WriteLine(Dogadjaj.sveDrzave.Count);
            Dogadjaj.sveDrzave = new List<Drzava>();
            Dogadjaj.gradoviUDrzavi = new Dictionary<string, List<Grad>>();
            foreach (MapPin p in items)
            {
                p.Style = (Style)this.Resources["PinoviNaMapi"];

                //if (!Dogadjaj.sveDrzave.Contains(p.Drzava))
                //{
                //    Dogadjaj.sveDrzave.Add(p.Drzava);
                //    Dogadjaj.gradoviUDrzavi[p.Drzava.DrzavaText] = new List<Grad>();
                //}
                //if (!Dogadjaj.gradoviUDrzavi[p.Drzava.DrzavaText].Contains(p.Grad))
                //    Dogadjaj.gradoviUDrzavi[p.Drzava.DrzavaText].Add(p.Grad);

                foreach (Dogadjaj d in p.ListaDogadjaja)
                {
                    d.Vidljiv = true;
                    d.Drzava = p.Drzava;
                    d.Grad = p.Grad;
                    if (!Dogadjaj.sveDrzave.Contains(p.Drzava))
                    {
                        Dogadjaj.sveDrzave.Add(p.Drzava);
                        Dogadjaj.gradoviUDrzavi[p.Drzava.DrzavaText] = new List<Grad>();
                    }
                    bool unutri = false;
                    foreach (Grad gr in Dogadjaj.gradoviUDrzavi[p.Drzava.DrzavaText])
                    {
                        if (p.Grad.GradText.Equals(gr.GradText))
                        {
                            unutri = true;
                            break;
                        }
                    }

                    if (!unutri)
                        Dogadjaj.gradoviUDrzavi[p.Drzava.DrzavaText].Add(p.Grad);
                }



                Canvas.SetLeft(p, p.X);
                Canvas.SetTop(p, p.Y);
                MainFrameCanvas.Children.Add(p);
            }

            return items;
        }


        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            this.WriteJson();
            Storyboard sb = (Storyboard) this.TryFindResource("fadeOutStoryboard");
            sb.Begin();
            
        }

        private void prikazMape_Click(object sender, RoutedEventArgs e)
        {
            this.SelektovaniPin = this._odabraniPin;
            this.MainFrame.Content = stranicaMape;
            this.levaTabla.IsEnabled = true;
            this.levaTabla.Opacity = 1;
            //this.drzavaTextBlock.Text = SelektovaniPin.Drzava.DrzavaText;
            //this.gradTextBlock.Text = SelektovaniPin.Grad.GradText;
            if (SelektovaniPin != null)
                this.brojDogadjajaTextBlock.Text = SelektovaniPin.ListaDogadjaja.Count.ToString();
            this.MainFrameCanvas.Visibility = Visibility.Visible;
            this.dodajPin.Opacity = 1;
            this.filterAndSearchWrapPanel.IsEnabled = true;
            this.filterGrid.IsEnabled = true;
            this.filterAndSearchWrapPanel.Opacity = 1;
            this.filterGrid.Opacity = 1;
        }

        private void prikazTabele_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelektovaniPin != null)
                this._odabraniPin = this.SelektovaniPin;
            this.stranicaTabele = new TablePage(SviPinovi);
            this.MainFrame.Content = stranicaTabele;
            this.levaTabla.IsEnabled = false;
            this.levaTabla.Opacity = 0.2;
            this.levaTabla.Background = Brushes.OrangeRed;
            this.SelektovaniPin = null;
            this.drzavaTextBlock.Text = "";
            this.gradTextBlock.Text = "";
            this.brojDogadjajaTextBlock.Text = "";
            this.MainFrameCanvas.Visibility = Visibility.Hidden;
            this.dodajPin.Opacity = 0.2;
            this.filterAndSearchWrapPanel.IsEnabled = true;
            this.filterGrid.IsEnabled = true;
            this.filterAndSearchWrapPanel.Opacity = 1;
            this.filterGrid.Opacity = 1;
        }

        private void pomoc_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelektovaniPin != null)
                this._odabraniPin = this.SelektovaniPin;
            this.MainFrame.Content = stranicaPomoci;
            this.levaTabla.IsEnabled = false;
            this.levaTabla.Opacity = 0.2;
            this.levaTabla.Background = Brushes.OrangeRed;
            this.SelektovaniPin = null;
            this.drzavaTextBlock.Text = "";
            this.gradTextBlock.Text = "";
            this.brojDogadjajaTextBlock.Text = "";
            this.MainFrameCanvas.Visibility = Visibility.Hidden;
            this.dodajPin.Opacity = 0.2;
            this.filterAndSearchWrapPanel.IsEnabled = false;
            this.filterGrid.IsEnabled = false;
            this.filterAndSearchWrapPanel.Opacity = 0.2;
            this.filterGrid.Opacity = 0.2;
        }

        private void ulogujteSe_Click(object sender, RoutedEventArgs e)
        {
            if (this.SelektovaniPin != null)
                this._odabraniPin = this.SelektovaniPin;
            this.MainFrame.Content = stranicaLogovanja;
            this.levaTabla.IsEnabled = false;
            this.levaTabla.Opacity = 0.2;
            this.levaTabla.Background = Brushes.OrangeRed;
            this.SelektovaniPin = null;
            this.drzavaTextBlock.Text = "";
            this.gradTextBlock.Text = "";
            this.brojDogadjajaTextBlock.Text = "";
            this.MainFrameCanvas.Visibility = Visibility.Hidden;
            this.dodajPin.Opacity = 0.2;
            this.filterAndSearchWrapPanel.IsEnabled = false;
            this.filterGrid.IsEnabled = false;
            this.filterAndSearchWrapPanel.Opacity = 0.2;
            this.filterGrid.Opacity = 0.2;
        }

        //private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    if(!(sender is WrapPanel))
        //        DragMove();
        //}

        private void DrzaveIGradovi_Click(object sender, RoutedEventArgs e)
        {
            //this.searchTextBox.Text = "";
            PodesiDrzaveDialog d = new PodesiDrzaveDialog(SviPinovi);
            this.Effect = new BlurEffect();
            d.ShowDialog();
            this.Effect = null;
            d.Close();

           // bool tt;

            //foreach (KeyValuePair<string, List<Grad>> listeGradova in Dogadjaj.gradoviUDrzavi)
            //{
            //    foreach(Grad gr in listeGradova.Value)
            //    {
            //        tt = false;
            //        foreach(MapPin p in SviPinovi)
            //        {
            //            if (tt) break;
            //            if (p.Drzava.DrzavaText.Equals(gr.DrzavaGradova) && p.Grad.GradText.Equals(gr.GradText))
            //            {
            //                foreach (Dogadjaj dog in p.ListaDogadjaja)
            //                {
            //                    dog.Vidljiv = gr.Oznacen;
            //                }
            //                tt = true;
            //                break;
            //            }
            //        }
            //    }
            //}

            foreach (MapPin pin in SviPinovi)
            {
                foreach(Dogadjaj dogad in pin.ListaDogadjaja)
                {
                    dogad.Vidljiv = pin.Grad.Oznacen;
                }
            }

            if (this.levaTabla.IsEnabled && this.SelektovaniPin != null)
                this.brojDogadjajaTextBlock.Text = this.SelektovaniPin.ListaVidljivihDogadjaja.Count.ToString();
            if (this.MainFrame.Content == this.stranicaTabele)
            {
                this.stranicaTabele = new TablePage(SviPinovi);
                this.MainFrame.Content = this.stranicaTabele;
            }
        }

        private void azurirajPin_Click(object sender, RoutedEventArgs e)
        {
            if (Pin == null)
            {
                OdabirPinaUpozorenjeDialog d = new OdabirPinaUpozorenjeDialog();
                this.Effect = new BlurEffect();
                d.ShowDialog();
                this.Effect = null;
            }
            else
            {
                this._azurirajPinDialog = new AzurirajPinDialog(Pin);
                this.Effect = new BlurEffect();
                _azurirajPinDialog.ShowDialog();
                this.SelektovaniPin = Pin;
                this.Effect = null;
                this._azurirajPinDialog.Close();
                this.brojDogadjajaTextBlock.Text = this.SelektovaniPin.BrojDogadjaja.ToString();
            }

        }

        private void obrisiPin_Click(object sender, RoutedEventArgs e)
        {
            if (Pin == null)
            {
                OdabirPinaUpozorenjeDialog d = new OdabirPinaUpozorenjeDialog();
                this.Effect = new BlurEffect();
                d.ShowDialog();
                this.Effect = null;
            }
            else
            {
                this._obrisiPinDialog = new ObrisiPinDialog(SelektovaniPin, this.MainFrameCanvas, this.SviPinovi);
                this.Effect = new BlurEffect();
                _obrisiPinDialog.ShowDialog();
                this.Effect = null;
                this._obrisiPinDialog.Close();

                if (ObrisiPinDialog.obrisan)
                {
                    this.drzavaTextBlock.Text = "";
                    this.gradTextBlock.Text = "";
                    this.brojDogadjajaTextBlock.Text = "";
                    SelektovaniPin = null;
                }
                    
            }

        }

        private void detaljnijiPregledPina_Click(object sender, RoutedEventArgs e)
        {
            if (Pin == null)
            {
                OdabirPinaUpozorenjeDialog d = new OdabirPinaUpozorenjeDialog();
                this.Effect = new BlurEffect();
                d.ShowDialog();
                this.Effect = null;
            }
            else
            {
                this._detaljnijiPregledPinaDialog = new DetaljnijiPregledPinaDialog(Pin);
                this.Effect = new BlurEffect();
                _detaljnijiPregledPinaDialog.ShowDialog();
                this.Effect = null;
                this._detaljnijiPregledPinaDialog.Close();
            }
        }

        private void dodajPin_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this._startPoint = e.GetPosition(this.MainFrame);
        }


        private void dodajPin_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePoz = e.GetPosition(this.MainFrame);
            Vector diff = this._startPoint - mousePoz;

            if (e.LeftButton == MouseButtonState.Pressed &&
               (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                //Button this.dodajPin = sender as Button;
                MapPin pinDugme = new MapPin();
                DataObject dragData = new DataObject(typeof(MapPin), pinDugme);
                DragDrop.DoDragDrop(this.dodajPin, dragData, DragDropEffects.Move);

            }



        }

        private void MainFrameCanvas_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(MapPin)) || e.Source == sender)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void MainFrameCanvas_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(MapPin)))
            {
                MapPin pinNaMapi = new MapPin();
                MapPin sacuvajStari = null; 
                pinNaMapi.Style = (Style)this.Resources["PinoviNaMapi"]; ;
                if (SelektovaniPin != null)
                {
                    sacuvajStari = new MapPin(SelektovaniPin);
                }



                DodajPinInformacijeDialog dialog = new DodajPinInformacijeDialog(pinNaMapi);
                this.Effect = new BlurEffect();
                SelektovaniPin = pinNaMapi;
                dialog.ShowDialog();
                this.Effect = null;
                dialog.Close();

                if (DodajPinInformacijeDialog.Potvrdjeno)
                {
                    Canvas.SetLeft(pinNaMapi, e.GetPosition(MainFrameCanvas).X - 20);
                    Canvas.SetTop(pinNaMapi, e.GetPosition(MainFrameCanvas).Y - 40);
                    pinNaMapi.X = e.GetPosition(MainFrameCanvas).X - 20;
                    pinNaMapi.Y = e.GetPosition(MainFrameCanvas).Y - 40;
                    pinNaMapi.IsChecked = true;
                    foreach (MapPin p in MainFrameCanvas.Children)
                    {
                        p.IsChecked = false;
                    }

                    MainFrameCanvas.Children.Add(pinNaMapi);
                    SviPinovi.Add(pinNaMapi);
                }
                else
                    SelektovaniPin = sacuvajStari;
            }
            //this.Cursor = Cursors.Arrow;
        }

        private void MainFrameCanvas_PreviewDragEnter(object sender, DragEventArgs e)
        {

        }

        private void MainFrameCanvas_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void MainFrameCanvas_PreviewDragOver(object sender, DragEventArgs e)
        {

        }

        //private void probaPin_Checked(object sender, RoutedEventArgs e)
        //{
        //    _ = this.probaPin;
        //}

        private void pinoviNaMapi_Checked(object sender, RoutedEventArgs e)
        {
            this.SelektovaniPin = sender as MapPin;
            this.brojDogadjajaTextBlock.Text = this.SelektovaniPin.ListaVidljivihDogadjaja.Count.ToString();
        }

        private void filterisanje(object sender, RoutedEventArgs e)
        {
            if(this.IsLoaded)
            {
                this.nazivDogadjajaFilter.Text = "";
                //this.searchTextBox.Text = "";
                foreach (MapPin p in SviPinovi)
                {
                    foreach (Dogadjaj d in p.ListaDogadjaja)
                    {
                        switch (d.Tip)
                        {
                            case TIP.MUZICKI:
                                if ((bool)this.musicFilter.IsChecked) d.Vidljiv = true;
                                else
                                {
                                    d.Vidljiv = false;
                                    continue;
                                }
                                break;
                            case TIP.FILMSKI:
                                if ((bool)this.filmFilter.IsChecked) d.Vidljiv = true;
                                else
                                {
                                    d.Vidljiv = false;
                                    continue;
                                }
                                break;
                            case TIP.PIVSKI:
                                if ((bool)this.beerFilter.IsChecked) d.Vidljiv = true;
                                else
                                {
                                    d.Vidljiv = false;
                                    continue;
                                }
                                break;
                            case TIP.SAJAM:
                                if ((bool)this.fairFilter.IsChecked) d.Vidljiv = true;
                                else
                                {
                                    d.Vidljiv = false;
                                    continue;
                                }
                                break;
                            case TIP.SLIKARSKI:
                                if ((bool)this.artFilter.IsChecked) d.Vidljiv = true;
                                else
                                {
                                    d.Vidljiv = false;
                                    continue;
                                }
                                break;
                        }

                        switch (d.Posecenost)
                        {
                            case POSECENOST_DOGADJAJA.DO_1000:
                                if ((bool)this.do1000OsobaFilter.IsChecked) d.Vidljiv = true;
                                else
                                {
                                    d.Vidljiv = false;
                                    continue;
                                }
                                break;
                            case POSECENOST_DOGADJAJA.IZMEDJU_1000_I_5000:
                                if ((bool)this.izmedju1000I5000OsobaFilter.IsChecked) d.Vidljiv = true;
                                else
                                {
                                    d.Vidljiv = false;
                                    continue;
                                }
                                break;
                            case POSECENOST_DOGADJAJA.IZMEDJU_5000_I_10000:
                                if ((bool)this.izmedju5000I10000OsobaFilter.IsChecked) d.Vidljiv = true;
                                else
                                {
                                    d.Vidljiv = false;
                                    continue;
                                }
                                break;
                            case POSECENOST_DOGADJAJA.PREKO_10000:
                                if ((bool)this.preko10000OsobaFilter.IsChecked) d.Vidljiv = true;
                                else
                                {
                                    d.Vidljiv = false;
                                    continue;
                                }
                                break;
                        }

                        switch (d.HumanitarniKarakter)
                        {
                            case true:
                                if ((bool)this.sviDogadjajiFilter.IsChecked) d.Vidljiv = true;
                                else if ((bool)this.humanitarniDogjajiFilter.IsChecked) d.Vidljiv = true;
                                else
                                {
                                    d.Vidljiv = false;
                                    continue;
                                }
                                break;
                            case false:
                                if ((bool)this.sviDogadjajiFilter.IsChecked) d.Vidljiv = true;
                                else if ((bool)this.nehumanitarniDogadjajiFilter.IsChecked) d.Vidljiv = true;
                                else
                                {
                                    d.Vidljiv = false;
                                    continue;
                                }
                                break;
                        }

                        if (d.Troskovi < 10000)
                        {
                            if ((bool)this.do10000DolaraFilter.IsChecked) d.Vidljiv = true;
                            else
                            {
                                d.Vidljiv = false;
                                continue;
                            }
                        }
                        else if (d.Troskovi >= 10000 && d.Troskovi < 50000)
                        {
                            if ((bool)this.izmedju10000I50000DolaraFilter.IsChecked) d.Vidljiv = true;
                            else
                            {
                                d.Vidljiv = false;
                                continue;
                            }
                        }
                        else if (d.Troskovi <= 50000 && d.Troskovi < 100000)
                        {
                            if ((bool)this.izmedju50000I100000DolaraFilter.IsChecked) d.Vidljiv = true;
                            else
                            {
                                d.Vidljiv = false;
                                continue;
                            }
                        }
                        else
                        {
                            if ((bool)this.preko100000DolaraFilter.IsChecked) d.Vidljiv = true;
                            else
                            {
                                d.Vidljiv = false;
                                continue;
                            }
                        }



                    }
                }
                if (this.levaTabla.IsEnabled && this.SelektovaniPin != null)
                    this.brojDogadjajaTextBlock.Text = this.SelektovaniPin.ListaVidljivihDogadjaja.Count.ToString();
                if (this.MainFrame.Content == this.stranicaTabele)
                {
                    this.stranicaTabele = new TablePage(SviPinovi);
                    this.MainFrame.Content = this.stranicaTabele;
                }
            }
                
        }

        private void nazivDogadjajaFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                CultureInfo culture = new CultureInfo("sr-Latn-CS");
                foreach (MapPin p in SviPinovi)
                {
                    foreach (Dogadjaj d in p.ListaDogadjaja)
                    {
                        if (culture.CompareInfo.IndexOf(d.Naziv, this.nazivDogadjajaFilter.Text, CompareOptions.IgnoreCase) != -1) d.Vidljiv = true;
                        else
                        {
                            d.Vidljiv = false;
                            continue;
                        }
                    }
                }
                if (this.levaTabla.IsEnabled && this.SelektovaniPin != null)
                    this.brojDogadjajaTextBlock.Text = this.SelektovaniPin.ListaVidljivihDogadjaja.Count.ToString();
                if (this.MainFrame.Content == this.stranicaTabele)
                {
                    this.stranicaTabele = new TablePage(SviPinovi);
                    this.MainFrame.Content = this.stranicaTabele;
                }
            }
                         
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            PretragaDialog pd = new PretragaDialog(this.SviPinovi);
            this.Effect = new BlurEffect();
            pd.ShowDialog();
            this.Effect = null;
            pd.Close();

            ////////////////////////////////////////////////////////////////////////////
            //string ceoIzraz = this.searchTextBox.Text;
            /*if (ceoIzraz.Equals(""))
            {
                //SviPinovi = _sacuvanoStanjePrePretrage;
                vratiStanjePrePretrage();
                if (this.levaTabla.IsEnabled && this.SelektovaniPin != null)
                    this.brojDogadjajaTextBlock.Text = this.SelektovaniPin.ListaVidljivihDogadjaja.Count.ToString();
                if (this.MainFrame.Content == this.stranicaTabele)
                {
                    this.stranicaTabele = new TablePage(SviPinovi);
                    this.MainFrame.Content = this.stranicaTabele;
                }
                return;
            }

            CultureInfo culture = new CultureInfo("sr-Latn-CS");
            //_sacuvanoStanjePrePretrage = new List<MapPin>();
            _sviPromenjeni = new List<Dogadjaj>();
            //foreach(MapPin p in SviPinovi)
            //{
            //    MapPin pp = new MapPin(p);
            //    _sacuvanoStanjePrePretrage.Add(pp);
            //}
            string[] pravilneCelineNiz = ceoIzraz.Split('|');
            foreach (string pravilnaCelina in pravilneCelineNiz)
            {
                string pravilnaCelinaTrim = pravilnaCelina.Trim();
                string[] delovi = pravilnaCelinaTrim.Split('=');

                if (delovi.Length != 2) 
                {
                    this.searchTextBox.Background = Brushes.Red;
                    return; 
                }

                string key = delovi[0].Trim();
                string value = delovi[1].Trim();

                if (key.Equals("Jedinstvena oznaka") ||
                    key.Equals("jedinstvena oznaka") ||
                    key.Equals("Naziv") ||
                    key.Equals("naziv") ||
                    key.Equals("Opis") ||
                    key.Equals("opis") ||
                    key.Equals("Tip") ||
                    key.Equals("tip") ||
                    key.Equals("Humanitarni karakter") ||
                    key.Equals("humanitarni karakter") ||
                    key.Equals("Država") ||
                    key.Equals("država") || 
                    key.Equals("Drzava") ||
                    key.Equals("drzava") ||
                    key.Equals("Grad") ||
                    key.Equals("grad")
                    )
                {
                    this.searchTextBox.Background = Brushes.White;
                    foreach (MapPin pin in SviPinovi)
                    {
                        
                        foreach (Dogadjaj dog in pin.ListaDogadjaja)
                        {
                            //object property = dog.TryFindResource(key);
                            //dog.
                            if (key.Equals("Jedinstvena oznaka") || key.Equals("jedinstvena oznaka"))
                            {
                                string realnaVrednost = dog.JedCitOzn;
                                if (culture.CompareInfo.IndexOf(realnaVrednost, value, CompareOptions.IgnoreCase) == -1)
                                {
                                    dog.Vidljiv = false;
                                    _sviPromenjeni.Add(dog);
                                }
                                else
                                    dog.Vidljiv = true;
                            }
                            else if (key.Equals("Naziv") || key.Equals("naziv"))
                            {
                                string realnaVrednost = dog.Naziv;
                                if (culture.CompareInfo.IndexOf(realnaVrednost, value, CompareOptions.IgnoreCase) == -1)
                                {
                                    dog.Vidljiv = false;
                                    _sviPromenjeni.Add(dog);
                                }
                                else
                                    dog.Vidljiv = true;
                            }
                            else if (key.Equals("Opis") || key.Equals("opis"))
                            {
                                string realnaVrednost = dog.Opis;
                                if (culture.CompareInfo.IndexOf(realnaVrednost, value, CompareOptions.IgnoreCase) == -1)
                                {
                                    dog.Vidljiv = false;
                                    _sviPromenjeni.Add(dog);
                                }
                                else
                                    dog.Vidljiv = true;
                            }
                            else if (key.Equals("Država") ||
                                    key.Equals("država") ||
                                    key.Equals("Drzava") ||
                                    key.Equals("drzava"))
                            {
                                Drzava pom = dog.Drzava;
                                string realnaVrednost = pom.DrzavaText;
                                if (culture.CompareInfo.IndexOf(realnaVrednost, value, CompareOptions.IgnoreCase) == -1)
                                {
                                    dog.Vidljiv = false;
                                    _sviPromenjeni.Add(dog);
                                }
                                else
                                    dog.Vidljiv = true;
                            }
                            else if (key.Equals("Grad") || key.Equals("grad"))
                            {
                                Grad pom = dog.Grad;
                                string realnaVrednost = pom.GradText;
                                if (culture.CompareInfo.IndexOf(realnaVrednost, value, CompareOptions.IgnoreCase) == -1)
                                {
                                    dog.Vidljiv = false;
                                    _sviPromenjeni.Add(dog);
                                }
                                else
                                    dog.Vidljiv = true;
                            }
                            else if (key.Equals("Humanitarni karakter") || key.Equals("humanitarni karakter"))
                            {
                                bool pom = dog.HumanitarniKarakter;
                                string realnaVrednost;
                                if (pom) realnaVrednost = "true";
                                else realnaVrednost = "false";
                                if (value.Equals("true") ||
                                    value.Equals("True") ||
                                    value.Equals("False") ||
                                    value.Equals("false"))
                                {
                                    if (culture.CompareInfo.IndexOf(realnaVrednost, value, CompareOptions.IgnoreCase) == -1)
                                    {
                                        dog.Vidljiv = false;
                                        _sviPromenjeni.Add(dog);
                                    }
                                    else
                                        dog.Vidljiv = true;
                                }
                                else
                                {
                                    this.searchTextBox.Background = Brushes.Red;
                                    return;
                                }
                                
                            }
                            else if (key.Equals("Tip") || key.Equals("tip"))
                            {
                                TIP pom = dog.Tip;
                                // public enum TIP { MUZICKI = 0, FILMSKI, PIVSKI, SLIKARSKI, SAJAM}
                                int realnaVrednost;
                                if (value.Equals("Muzicki") ||
                                    value.Equals("Muzički"))
                                {
                                    realnaVrednost = 0;

                                }
                                else if (value.Equals("Filmski"))
                                {
                                    realnaVrednost = 1;
                                }
                                else if (value.Equals("Pivski"))
                                {
                                    realnaVrednost = 2;
                                }
                                else if (value.Equals("Slikarski"))
                                {
                                    realnaVrednost = 3;
                                }
                                else if (value.Equals("Sajamski") ||
                                         value.Equals("Sajam"))
                                {
                                    realnaVrednost = 4;
                                }
                                else
                                {
                                    realnaVrednost = -1;
                                    this.searchTextBox.Background = Brushes.Red;
                                    return;
                                }
                                    

                                
                                if (realnaVrednost != (int)pom)
                                {
                                    dog.Vidljiv = false;
                                    _sviPromenjeni.Add(dog);
                                }
                                else
                                    dog.Vidljiv = true;
                            }
                        }
                        

                 
                    }
                }
                else
                {
                    this.searchTextBox.Background = Brushes.Red;
                    return;
                }      
            }
            if (this.levaTabla.IsEnabled && this.SelektovaniPin != null)
                this.brojDogadjajaTextBlock.Text = this.SelektovaniPin.ListaVidljivihDogadjaja.Count.ToString();
            if (this.MainFrame.Content == this.stranicaTabele)
            {
                this.stranicaTabele = new TablePage(SviPinovi);
                this.MainFrame.Content = this.stranicaTabele;
            }*/
        }

        //private void searchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (this._sviPromenjeni.Count != 0)
        //    {
        //        //SviPinovi = _sacuvanoStanjePrePretrage;
        //        vratiStanjePrePretrage();
        //    }
        //}
        /*private void vratiStanjePrePretrage()
        {
            foreach (Dogadjaj d in this._sviPromenjeni)
                d.Vidljiv = true;
        }*/

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            /*IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);

            if (focusedControl is Control)
            {
                string str = HelpProvider.GetHelpKey((Control)focusedControl);
                HelpProvider.ShowHelp(str, this);
            } 
            else*/ 
            if (this.IsActive && this.IsEnabled)
            {
                string str = HelpProvider.GetHelpKey(this);
                HelpProvider.ShowHelp(str, this);
            }
            
        }
    }
}
