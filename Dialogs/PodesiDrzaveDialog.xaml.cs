using EventMap.Classes;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;

namespace EventMap.Dialogs
{
    /// <summary>
    /// Interaction logic for PodesiDrzaveDialog.xaml
    /// </summary>
    public partial class PodesiDrzaveDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public ObservableCollection<Drzava> SveDrzave 
        {
            get
            {
                return new ObservableCollection<Drzava>(Dogadjaj.sveDrzave);
            }
        }

        public List<Grad> GradoviUDrzavi
        {
            get
            {
                List<Grad> l = new List<Grad>();
                foreach (KeyValuePair<string, List<Grad>> kvp in Dogadjaj.gradoviUDrzavi)
                {
                    l.AddRange(kvp.Value);
                }
                _gradoviUDrzavi = l;
                return _gradoviUDrzavi;
            }
            set
            {
                if (value != _gradoviUDrzavi)
                {
                    _gradoviUDrzavi = value;
                    OnPropertyChanged("SviPinovi");
                }
            }

        }

        private List<Grad> _gradoviUDrzavi;
        private List<CheckBox> _gradoviCheckBox;
        private List<MapPin> _sviPinovi;


        public List<MapPin> SviPinovi
        {
            get { return _sviPinovi; }
            set
            {
                if (value != _sviPinovi)
                {
                    _sviPinovi = value;
                    OnPropertyChanged("SviPinovi");
                }
            }
        }

        public List<CheckBox> GradoviCheckBox
        {
            get { return _gradoviCheckBox; }
            set
            {
                if (value != _gradoviCheckBox)
                {
                    _gradoviCheckBox = value;
                    OnPropertyChanged("GradoviCheckBox");
                }
            }
        }

        public PodesiDrzaveDialog(List<MapPin> svi)
        {
            InitializeComponent();
            this.DataContext = this;

            this.SviPinovi = svi;

            this._gradoviCheckBox = new List<CheckBox>();

            // Find all elements
            

        }

        private void otkaziPrimenuFiltera_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        private void primeniFiltere_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, List<Grad>> mapaGradova = new Dictionary<string, List<Grad>>();
            StatickeFunkcije.FindChildGroup<CheckBox>(this.tabelaGradovi, "filtriraniGradovi", ref _gradoviCheckBox);
            int i = 0;
            foreach(Grad gr in this.GradoviUDrzavi)
            {
                gr.Oznacen = (bool)this._gradoviCheckBox[i].IsChecked;
                ++i;
                if (!mapaGradova.ContainsKey(gr.DrzavaGradova.DrzavaText))
                    mapaGradova[gr.DrzavaGradova.DrzavaText] = new List<Grad>();
                mapaGradova[gr.DrzavaGradova.DrzavaText].Add(gr);
            }

            Dogadjaj.gradoviUDrzavi = new Dictionary<string,List<Grad>>(mapaGradova);

            Storyboard sb = (Storyboard)this.TryFindResource("fadeOutStoryboard");
            sb.Begin();
        }

        private void sveDrzaveCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if(this.IsLoaded)
                foreach( Drzava dr in Dogadjaj.sveDrzave)
                {
                    dr.Oznacena = true;
                }
        }

        private void sveDrzaveCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded)
                foreach (Drzava dr in Dogadjaj.sveDrzave)
                {
                    dr.Oznacena = false;
                }

        }

        private void sviGradoviCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded)
                foreach (List<Grad> lista in Dogadjaj.gradoviUDrzavi.Values)
                {
                    foreach(Grad gr in lista)
                    {
                        gr.Oznacen = true;
                    }
                }
        }

        private void sviGradoviCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded)
                foreach (List<Grad> lista in Dogadjaj.gradoviUDrzavi.Values)
                {
                    foreach (Grad gr in lista)
                    {
                        gr.Oznacen = false;
                    }
                }

            
        }

        //private void filtriraniGradovi_Checked(object sender, RoutedEventArgs e)
        //{
        //    foreach (MapPin p in SviPinovi)
        //    {
        //        foreach (Dogadjaj d in p.ListaDogadjaja)
        //        {
        //            foreach (CheckBox cbg in _gradoviCheckBox)
        //            {
        //                IList item = tabelaGradovi.SelectedItems;
        //                Grad gr = (Grad)item[0];

        //            }
        //        }
        //    }
        //}

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            this.Visibility = Visibility.Hidden;
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

