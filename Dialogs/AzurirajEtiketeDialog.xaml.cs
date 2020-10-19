using EventMap.Classes;
using System;
using System.Collections;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace EventMap.Dialogs
{
    /// <summary>
    /// Interaction logic for AzurirajEtiketeDialog.xaml
    /// </summary>
    public partial class AzurirajEtiketeDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private List<Etiketa> _listaEtiketaZaAzuriranje;
        private List<Etiketa> _predjasnjeStanjeListe;
        private Dogadjaj _dog;
        private Etiketa _selektovanaEtiketa;


        public AzurirajEtiketeDialog(Dogadjaj dog)
        {
            InitializeComponent();
            this.DataContext = this;

            this._dog = dog;
            this._predjasnjeStanjeListe = new List<Etiketa>();

            this._listaEtiketaZaAzuriranje = dog.ListaEtiketa;
            foreach(Etiketa p in this._listaEtiketaZaAzuriranje)
            {
                Etiketa et = new Etiketa(p);
                this._predjasnjeStanjeListe.Add(et);
            }
        }

        public List<Etiketa> ListaEtiketaZaAzuriranje
        {
            get
            {
                return _listaEtiketaZaAzuriranje;
            }
            set
            {
                if (_listaEtiketaZaAzuriranje != value)
                {
                    _listaEtiketaZaAzuriranje = value;
                    OnPropertyChanged("ListaEtiketaZaAzuriranje");
                }
            }
        }

        public Etiketa SelektovanaEtiketa
        {
            get
            {
                return _selektovanaEtiketa;
            }
            set
            {
                if (_selektovanaEtiketa != value)
                {
                    _selektovanaEtiketa = value;
                    OnPropertyChanged("SelektovanaEtiketa");
                }
            }
        }

        private void povratakNaPregledPinaButton_Click(object sender, RoutedEventArgs e)
        {
            this._dog.ListaEtiketa = this._predjasnjeStanjeListe;
            Storyboard sb = (Storyboard)this.TryFindResource("fadeOutStoryboard");
            sb.Begin();
        }

        private void azurirajEtiketeButton_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = (Storyboard)this.TryFindResource("fadeOutStoryboard");
            sb.Begin();
        }

        private void promeniBojuEtiketeColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if(this.SelektovanaEtiketa != null)
                this.SelektovanaEtiketa.Boja = (Color)promeniBojuEtiketeColorPicker.SelectedColor;
        }

        //private void listaSaEtiketama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    IList item = listaSaEtiketama.SelectedItems;
        //    if (item != null && item.Count != 0)
        //        this.SelektovanaEtiketa = (Etiketa)item[0];

        //}

        private void dodajNovuEtiketu_Click(object sender, RoutedEventArgs e)
        {
            
            DodajNovuEtiketuDialog p = new DodajNovuEtiketuDialog(this._listaEtiketaZaAzuriranje);
            this.Effect = new BlurEffect();
            p.ShowDialog();
            this.Effect = null;
            p.Close();

            listaSaEtiketama.Items.Refresh();
        }

        private void obrisiEtiketu_Click(object sender, RoutedEventArgs e)
        {
            if (this._selektovanaEtiketa != null)
            {
                this._listaEtiketaZaAzuriranje.Remove(this._selektovanaEtiketa);
                listaSaEtiketama.Items.Refresh();
            }
        }

        protected void SelectCurrentItem(object sender, EventArgs e)
        {
            ListViewItem item = (ListViewItem)sender;
            //  item.IsSelected = true;
            //listaSaEtiketama.Items.Refresh();
        }

        private void listaSaEtiketama_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IList item = listaSaEtiketama.SelectedItems;
            if (item != null && item.Count != 0)
            this.SelektovanaEtiketa = (Etiketa)item[0];
        }

        private void FocusOnTextBox(object sender, MouseEventArgs e)
        {
            MessageBox.Show("FocusOnTextBox fired on selection with list view");
            ListViewItem i = (ListViewItem)sender;
            i.IsSelected = true;       
        }

        private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            
            TextBox tb = (TextBox)sender;
            //this._selektovanaEtiketa = ;
            tb.Focus();
        }

        private void ListViewItem_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ListViewItem item = (ListViewItem)sender;
            this.SelektovanaEtiketa = (Etiketa)item.Content;
        }
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
